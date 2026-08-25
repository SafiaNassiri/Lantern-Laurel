using UnityEngine;
using UnityEngine.Events;

namespace GraveyardShift.Spirits
{
    public enum SpiritResolutionState
    {
        Unresolved,
        Released,
        Bound,
        IgnoredUntilMorning
    }

    /// <summary>
    /// Every spirit micro-quest resolves into one of three standardized states so the whole roster shares one resolution system instead of one-off scripted endings per spirit. 
    /// Also listens for the Interact action while the player is inside its trigger zone, so interacting with a spirit resolves it as Released. 
    /// </summary>
    public class SpiritController : MonoBehaviour
    {
        [Header("Identity")]
        public string spiritId;
        [TextArea] public string wantDescription; // one-line "want" from the GDD roster table

        [Header("Detection Tells (diegetic — GDD 2.4)")]
        public float coldZoneRadius = 4f;
        [Tooltip("Optional — leave empty to auto-find the player's CaretakerLantern via CaretakerLantern.Instance.")]
        public LanternLaurel.Player.CaretakerLantern nearbyLantern;

        public SpiritResolutionState State { get; private set; } = SpiritResolutionState.Unresolved;

        public UnityEvent<SpiritResolutionState> onResolved;

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

        private LanternLaurel.Player.CaretakerLantern ResolveLantern()
            => nearbyLantern != null ? nearbyLantern : LanternLaurel.Player.CaretakerLantern.Instance;

        public void Resolve(SpiritResolutionState newState)
        {
            if (State != SpiritResolutionState.Unresolved) return; // resolve once
            State = newState;
            Debug.Log($"[SpiritController] {spiritId} resolved: {newState}");
            onResolved?.Invoke(newState);
        }

        private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (!_playerInRange || State != SpiritResolutionState.Unresolved) return;
            ResolveLantern()?.Pulse(1.5f);
            Resolve(SpiritResolutionState.Released);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = true;
            ResolveLantern()?.SetSpiritProximity(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInRange = false;
            ResolveLantern()?.SetSpiritProximity(false);
        }
    }
}