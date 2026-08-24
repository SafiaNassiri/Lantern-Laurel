using UnityEngine;

namespace GraveyardShift.Player
{
    /// <summary>
    /// Drives a CharacterController at a single fixed jog pace.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float jogSpeed = 3.5f;
        [SerializeField] private float gravity = -9.81f;

        [Header("Body-Follows-Camera Look")]
        [Tooltip("Lower = snappier turn, higher = lazier/smoother turn. In seconds, roughly how long the body takes to catch up to the camera's facing.")]
        [SerializeField] private float turnSmoothTime = 0.12f;
        private float _turnSmoothVelocity;

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

        private void OnEnable() => _controls.Player.Enable();
        private void OnDisable() => _controls.Player.Disable();

        private void Update()
        {
            Vector2 moveInput = _controls.Player.Move.ReadValue<Vector2>();
            RotateBodyToCamera();
            MoveCharacter(moveInput);
        }

        /// <summary>
        /// Smoothly rotates the player's yaw to match the camera's current facing direction, every frame — independent of movement input.
        /// This is what gives the "turn to look" feel: moving the mouse turns the body even while standing still.
        /// </summary>
        private void RotateBodyToCamera()
        {
            if (cameraTransform == null) return;

            float targetYaw = cameraTransform.eulerAngles.y;
            float currentYaw = transform.eulerAngles.y;
            float smoothedYaw = Mathf.SmoothDampAngle(
                currentYaw, targetYaw, ref _turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothedYaw, 0f);
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

            // Movement stays camera-relative regardless of the body's current facing, so movement always feels immediate even mid-turn.
            Vector3 camForward = cameraTransform != null ? cameraTransform.forward : Vector3.forward;
            Vector3 camRight = cameraTransform != null ? cameraTransform.right : Vector3.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = (camForward * inputDir.z + camRight * inputDir.x).normalized;

            Vector3 motion = moveDir * jogSpeed;
            motion.y = _verticalVelocity;
            _controller.Move(motion * Time.deltaTime);
        }
    }
}