using UnityEngine;
using UnityEngine.InputSystem;

namespace GraveyardShift.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
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
        private int _inputXHash;
        private int _inputYHash;
        private int _isSprintingHash;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _controls = new PlayerControls();

            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;

            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            _speedHash = Animator.StringToHash("Speed");
            _inputXHash = Animator.StringToHash("InputX");
            _inputYHash = Animator.StringToHash("InputY");
            _isSprintingHash = Animator.StringToHash("IsSprinting");
        }

        private void OnEnable()
        {
            if (_controls == null) return;
            _controls.Player.Enable();
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
                // Calculate target angle based on input + camera yaw
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

                // Smoothly rotate character toward movement heading
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move forward along the target angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float currentSpeed = _isSprinting ? sprintSpeed : walkSpeed;

                Vector3 motion = moveDir.normalized * currentSpeed;
                motion.y = _verticalVelocity;
                _controller.Move(motion * Time.deltaTime);

                // Update Animator Parameters
                if (animator != null)
                {
                    animator.SetBool(_isSprintingHash, _isSprinting);

                    if (_isSprinting)
                    {
                        // Set Speed to 1.0 (Run) and send directional axes
                        animator.SetFloat(_speedHash, 1.0f, animDampTime, Time.deltaTime);
                        animator.SetFloat(_inputXHash, input.x, animDampTime, Time.deltaTime);
                        animator.SetFloat(_inputYHash, input.y, animDampTime, Time.deltaTime);
                    }
                    else
                    {
                        // Set Speed to 0.5 (Walk)
                        animator.SetFloat(_speedHash, 0.5f, animDampTime, Time.deltaTime);
                        animator.SetFloat(_inputXHash, 0f, animDampTime, Time.deltaTime);
                        animator.SetFloat(_inputYHash, 1f, animDampTime, Time.deltaTime);
                    }
                }
            }
            else
            {
                // Standing Still / Idle
                _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);

                if (animator != null)
                {
                    animator.SetBool(_isSprintingHash, false);
                    animator.SetFloat(_speedHash, 0f, animDampTime, Time.deltaTime);
                    animator.SetFloat(_inputXHash, 0f, animDampTime, Time.deltaTime);
                    animator.SetFloat(_inputYHash, 0f, animDampTime, Time.deltaTime);
                }
            }
        }
    }
}