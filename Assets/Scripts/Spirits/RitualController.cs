using UnityEngine;

namespace GraveyardShift.Rituals
{
    /// <summary>
    /// Reads the DrawRitual action and drives whichever RitualVerb is currently assigned as active.
    /// For now, automatically uses the SaltLineRitual on the same Player.
    /// </summary>
    public class RitualController : MonoBehaviour
    {
        private PlayerControls _controls;
        private RitualVerb _activeVerb;

        private void Awake()
        {
            _controls = new PlayerControls();

            // Temporary: automatically use the SaltLineRitual attached to this Player.
            _activeVerb = GetComponent<SaltLineRitual>();

            if (_activeVerb != null)
            {
                _activeVerb.Unlock();
                Debug.Log($"[RitualController] Auto-assigned ritual: {_activeVerb.VerbId}");
            }
            else
            {
                Debug.LogWarning("[RitualController] No SaltLineRitual found on Player!");
            }
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                Debug.LogError("[RitualController] _controls is NULL in OnEnable!");
                return;
            }

            _controls.Player.Enable();
            _controls.Player.DrawRitual.started += OnDrawStarted;
            _controls.Player.DrawRitual.canceled += OnDrawCanceled;
        }

        private void OnDisable()
        {
            if (_controls != null)
            {
                _controls.Player.DrawRitual.started -= OnDrawStarted;
                _controls.Player.DrawRitual.canceled -= OnDrawCanceled;
                _controls.Player.Disable();
            }
        }

        private void Update()
        {
            if (_activeVerb != null && _activeVerb.IsUnlocked)
                _activeVerb.UpdateRitual();
        }

        /// <summary>
        /// Called by GrimoirePickup (or similar) once a verb is unlocked.
        /// </summary>
        public void SetActiveVerb(RitualVerb verb)
        {
            verb.Unlock();
            _activeVerb = verb;

            Debug.Log($"[RitualController] Active verb set to: {verb.VerbId}");
        }

        private void OnDrawStarted(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("[RitualController] DrawRitual started!");

            if (_activeVerb == null)
            {
                Debug.LogWarning("[RitualController] DrawRitual pressed, but NO active ritual is assigned!");
                return;
            }

            if (!_activeVerb.IsUnlocked)
            {
                Debug.LogWarning($"[RitualController] Active ritual '{_activeVerb.VerbId}' is NOT unlocked!");
                return;
            }

            Debug.Log($"[RitualController] Starting ritual: {_activeVerb.VerbId}");
            _activeVerb.BeginRitual();
        }

        private void OnDrawCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            Debug.Log("[RitualController] DrawRitual canceled!");

            if (_activeVerb == null)
            {
                Debug.LogWarning("[RitualController] DrawRitual released, but NO active ritual is assigned!");
                return;
            }

            if (!_activeVerb.IsUnlocked)
            {
                Debug.LogWarning($"[RitualController] Active ritual '{_activeVerb.VerbId}' is NOT unlocked!");
                return;
            }

            Debug.Log($"[RitualController] Completing ritual: {_activeVerb.VerbId}");
            _activeVerb.CompleteRitual();
        }
    }
}