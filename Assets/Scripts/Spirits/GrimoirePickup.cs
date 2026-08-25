using UnityEngine;
using GraveyardShift.Rituals;

namespace GraveyardShift.Grimoire
{
    /// <summary>
    /// Place in the scene where the player finds the grimoire. On Interact while in range, unlocks the linked ritual verb on the player's RitualController and removes itself. 
    /// </summary>
    public class GrimoirePickup : MonoBehaviour
    {
        [SerializeField] private GrimoireEntry grimoirePage;
        [SerializeField] private RitualController ritualController;
        [SerializeField] private RitualVerb verbToUnlock;

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
            if (other.CompareTag("Player")) _playerInRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) _playerInRange = false;
        }

        private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (!_playerInRange) return;

            if (grimoirePage != null)
                Debug.Log($"[Grimoire] Picked up page: {grimoirePage.entryId} — {grimoirePage.pageText}");

            if (ritualController != null && verbToUnlock != null)
                ritualController.SetActiveVerb(verbToUnlock);

            gameObject.SetActive(false); // simple "picked up" feedback until real UI exists
        }
    }
}