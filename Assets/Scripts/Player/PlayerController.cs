using UnityEngine;

namespace GraveyardShift.Player
{
    /// <summary>
    /// Drives a CharacterController at a single fixed jog pace. Reads input from the generated PlayerControls class, not the legacy Input API.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float jogSpeed = 3.5f;
        [SerializeField] private float turnSpeedDegrees = 540f;
        [SerializeField] private float gravity = -9.81f;

        [Header("Camera Reference")]
        [Tooltip("Drag your Cinemachine follow camera's transform here. Movement direction is relative to this.")]
        [SerializeField] private Transform cameraTransform;

        private CharacterController _controller;
        private PlayerControls _controls;
        private float _verticalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _controls = new PlayerControls();

            if (cameraTransform == null && Camera.main != null)
                cameraTransform = Camera.main.transform;
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                Debug.LogError("[PlayerController] _controls is NULL in OnEnable!");
                return;
            }

            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            if (_controls != null)
                _controls.Player.Disable();
        }

        private void Update()
        {
            Vector2 moveInput = _controls.Player.Move.ReadValue<Vector2>();
            MoveCharacter(moveInput);
        }

        private void MoveCharacter(Vector2 input)
        {
            Vector3 inputDir = new Vector3(input.x, 0f, input.y);

            // Apply gravity so the controller stays grounded on slopes.
            if (_controller.isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = -2f;
            _verticalVelocity += gravity * Time.deltaTime;

            if (inputDir.sqrMagnitude < 0.0001f)
            {
                _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
                return;
            }

            // Convert input from local (camera-relative) space to world space so "forward" always means "away from the camera".
            Vector3 camForward = cameraTransform != null ? cameraTransform.forward : Vector3.forward;
            Vector3 camRight = cameraTransform != null ? cameraTransform.right : Vector3.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = (camForward * inputDir.z + camRight * inputDir.x).normalized;

            // Rotate the player to face the movement direction.
            Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, turnSpeedDegrees * Time.deltaTime);

            Vector3 motion = moveDir * jogSpeed;
            motion.y = _verticalVelocity;
            _controller.Move(motion * Time.deltaTime);
        }
    }
}