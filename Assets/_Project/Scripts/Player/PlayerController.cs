using UnityEngine;
using UnityEngine.InputSystem;

namespace GraveyardShift.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Speeds")]
        [SerializeField] private float walkSpeed = 2.5f;
        [SerializeField] private float sprintSpeed = 5.0f;
        [SerializeField] private float rotationSmoothTime = 0.1f;
        [SerializeField] private float gravity = -9.81f;

        [Header("References")]
        [Tooltip("Assign your Cinemachine or Main Camera transform here.")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private float animDampTime = 0.1f;

        private CharacterController _controller;
        private PlayerControls _controls;
        private float _turnSmoothVelocity;
        private float _verticalVelocity;
        private bool _isSprinting;
        private int _speedHash;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _controls = new PlayerControls();

            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;

            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            _speedHash = Animator.StringToHash("Speed");
        }

        private void OnEnable()
        {
            if (_controls == null) return;
            _controls.Player.Enable();

            // Sprint is active strictly while Left Shift is held down
            _controls.Player.Sprint.performed += OnSprintPerformed;
            _controls.Player.Sprint.canceled += OnSprintCanceled;
        }

        private void OnDisable()
        {
            if (_controls != null)
            {
                _controls.Player.Sprint.performed -= OnSprintPerformed;
                _controls.Player.Sprint.canceled -= OnSprintCanceled;
                _controls.Player.Disable();
            }
        }

        private void OnSprintPerformed(InputAction.CallbackContext context) => _isSprinting = true;
        private void OnSprintCanceled(InputAction.CallbackContext context) => _isSprinting = false;

        private void Update()
        {
            Vector2 input = _controls.Player.Move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

            // Gravity Calculation
            if (_controller.isGrounded && _verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }
            else
            {
                _verticalVelocity += gravity * Time.deltaTime;
            }

            if (direction.magnitude >= 0.1f)
            {
                // Calculate target angle based on input direction + camera angle
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

                // Smoothly rotate the character towards the travel heading
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move forward in the target direction
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float currentSpeed = _isSprinting ? sprintSpeed : walkSpeed;

                Vector3 motion = moveDir.normalized * currentSpeed;
                motion.y = _verticalVelocity;
                _controller.Move(motion * Time.deltaTime);

                // 1.0 (Run) when Shift is held, 0.5 (Walk) otherwise
                if (animator != null)
                {
                    float targetAnimSpeed = _isSprinting ? 1.0f : 0.5f;
                    animator.SetFloat(_speedHash, targetAnimSpeed, animDampTime, Time.deltaTime);
                }
            }
            else
            {
                // Idle / Standing Still
                _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);

                if (animator != null)
                {
                    animator.SetFloat(_speedHash, 0f, animDampTime, Time.deltaTime);
                }
            }
        }
    }
}