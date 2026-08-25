using UnityEngine;

namespace LanternLaurel.Player
{
    /// <summary>
    /// Handles practical illumination, fuel drain, and supernatural flicker response.
    /// </summary>
    public class CaretakerLantern : MonoBehaviour
    {
        /// <summary>
        /// Simple auto-lookup so scene objects (like SpiritController zones) don't need a manual Inspector drag-and-drop for every instance.
        /// Assumes a single player lantern per scene, which holds for this game's scope (one playable character).
        /// </summary>
        public static CaretakerLantern Instance { get; private set; }

        [Header("Light Reference")]
        [Tooltip("Assign your independent Point Light child GameObject here.")]
        [SerializeField] private Light lanternLight;

        [Header("Light Settings")]
        [SerializeField] private float baseIntensity = 5.0f;
        [SerializeField] private float maxRange = 15f;
        [SerializeField] private Color normalColor = new Color(1.0f, 0.78f, 0.45f); // Warm amber
        [SerializeField] private Color spiritProximityColor = new Color(0.4f, 0.85f, 0.85f); // Cool teal

        [Header("Fuel Mechanics")]
        [SerializeField] private float maxFuel = 100f;
        [SerializeField] private float currentFuel = 100f;
        [SerializeField] private float burnRatePerSecond = 0.5f; // Drains over shift

        [Header("Supernatural Flicker")]
        [SerializeField] private bool isSpiritNearby = false;
        [SerializeField] private float flickerSpeed = 20f;
        [SerializeField] private float flickerIntensityVariance = 0.8f;

        private float _noiseOffset;

        private void Awake()
        {
            Instance = this;

            // Fallback: If not assigned in Inspector, check children first, then this GameObject
            if (lanternLight == null)
            {
                lanternLight = GetComponentInChildren<Light>();
            }

            if (lanternLight != null)
            {
                lanternLight.type = LightType.Point;
                lanternLight.color = normalColor;
                lanternLight.range = maxRange;
            }
            else
            {
                Debug.LogWarning("[CaretakerLantern] No Light component assigned or found in children!", this);
            }

            _noiseOffset = Random.Range(0f, 100f);
        }

        private void Update()
        {
            HandleFuelDrain();
            UpdateIllumination();
        }

        private void HandleFuelDrain()
        {
            if (currentFuel > 0f)
            {
                currentFuel -= burnRatePerSecond * Time.deltaTime;
                currentFuel = Mathf.Max(0f, currentFuel);
            }
        }

        private void UpdateIllumination()
        {
            if (lanternLight == null) return;

            float fuelPercent = currentFuel / maxFuel;
            float targetIntensity = baseIntensity * fuelPercent;

            if (isSpiritNearby && currentFuel > 0f)
            {
                // Procedural Perlin flicker
                float noise = Mathf.PerlinNoise((Time.time * flickerSpeed) + _noiseOffset, 0f);
                float flicker = (noise * 2f - 1f) * flickerIntensityVariance;
                lanternLight.intensity = Mathf.Max(0.2f, targetIntensity + flicker);
                lanternLight.color = Color.Lerp(normalColor, spiritProximityColor, 0.5f);
            }
            else
            {
                lanternLight.intensity = targetIntensity;
                lanternLight.color = normalColor;
            }
        }

        /// <summary>
        /// Called by spirit triggers or proximity zones.
        /// </summary>
        public void SetSpiritProximity(bool nearby)
        {
            isSpiritNearby = nearby;
        }

        /// <summary>
        /// One-shot flicker for a fixed duration, then automatically returns to normal.
        /// </summary>
        public void Pulse(float durationSeconds = 2f)
        {
            StopAllCoroutines(); // avoid overlapping pulses stacking oddly
            StartCoroutine(PulseRoutine(durationSeconds));
        }

        private System.Collections.IEnumerator PulseRoutine(float durationSeconds)
        {
            isSpiritNearby = true;
            yield return new WaitForSeconds(durationSeconds);
            isSpiritNearby = false;
        }

        /// <summary>
        /// Refuel the lantern.
        /// </summary>
        public void Refuel(float amount)
        {
            currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
        }
    }
}