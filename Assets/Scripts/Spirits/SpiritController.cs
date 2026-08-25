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
    /// </summary>
    public class SpiritController : MonoBehaviour
    {
        [Header("Identity")]
        public string spiritId;
        [TextArea] public string wantDescription; 

        [Header("Detection Tells (diegetic — GDD 2.4)")]
        public float coldZoneRadius = 4f;
        [Tooltip("Optional — leave empty to auto-find the player's CaretakerLantern via CaretakerLantern.Instance.")]
        public LanternLaurel.Player.CaretakerLantern nearbyLantern;

        private LanternLaurel.Player.CaretakerLantern ResolveLantern()
            => nearbyLantern != null ? nearbyLantern : LanternLaurel.Player.CaretakerLantern.Instance;

        public SpiritResolutionState State { get; private set; } = SpiritResolutionState.Unresolved;

        public UnityEvent<SpiritResolutionState> onResolved;

        public void Resolve(SpiritResolutionState newState)
        {
            if (State != SpiritResolutionState.Unresolved) return; 
            State = newState;
            onResolved?.Invoke(newState);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            ResolveLantern()?.SetSpiritProximity(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            ResolveLantern()?.SetSpiritProximity(false);
        }
    }
}