using UnityEngine;

namespace GraveyardShift.Chores
{
    /// <summary>
    /// Attach to a chore object in the scene (e.g. a path that needs sweeping). Requires a trigger Collider to detect the player, and  listens for the Interact action while the player is inside it. 
    /// </summary>
    public class ChoreInteractable : MonoBehaviour
    {
        [Tooltip("Must match a ChoreTask.choreId in ChoreManager's Tonights Chores list.")]
        [SerializeField] private string choreId;

        [Tooltip("Optional prompt text you can hook up to UI later (e.g. 'Press E to sweep').")]
        [SerializeField] private string interactPrompt = "Press E to interact";

        private PlayerControls _controls;
        private bool _playerInRange;

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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = true;
            // TODO: show interactPrompt in UI once you have one.
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = false;
            // TODO: hide interactPrompt in UI once you have one.
        }

        private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (!_playerInRange) return;

            if (ChoreManager.Instance == null)
            {
                Debug.LogWarning("ChoreInteractable: no ChoreManager found in scene.");
                return;
            }

            ChoreManager.Instance.CompleteChore(choreId);
        }
    }
}