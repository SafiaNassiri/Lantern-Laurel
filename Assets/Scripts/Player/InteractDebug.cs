using UnityEngine;

namespace GraveyardShift.Player
{
    /// <summary>
    /// Temporary debug script to confirm the Interact action from PlayerControls is firing correctly. Delete or replace this once real chore/tool interaction logic exists.
    /// </summary>
    public class InteractDebug : MonoBehaviour
    {
        private PlayerControls _controls;

        private void Awake()
        {
            _controls = new PlayerControls();
        }

        private void OnEnable()
        {
            _controls.Player.Enable();
            _controls.Player.Interact.performed += OnInteractPerformed;
        }

        private void OnDisable()
        {
            _controls.Player.Interact.performed -= OnInteractPerformed;
            _controls.Player.Disable();
        }

        private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log($"[InteractDebug] Interact fired at {Time.time:F2}s — input is wired correctly.");
        }
    }
}