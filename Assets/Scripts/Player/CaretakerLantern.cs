using UnityEngine;

namespace LanternLaurel.Player
{
    /// <summary>
    /// Handles practical illumination, fuel drain, and supernatural flicker response.
    /// </summary>
    [RequireComponent(typeof(Light))]
    public class CaretakerLantern : MonoBehaviour
    {
        [Header("Light Settings")]
        [SerializeField] private float baseIntensity = 2.5f;
        [SerializeField] private float maxRange = 12f;
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

        private Light _lanternLight;
        private float _noiseOffset;

        private void Awake()
        {
            _lanternLight = GetComponent<Light>();
            _lanternLight.type = LightType.Point;
            _lanternLight.color = normalColor;
            _lanternLight.range = maxRange;
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
            float fuelPercent = currentFuel / maxFuel;
            float targetIntensity = baseIntensity * fuelPercent;

            if (isSpiritNearby && currentFuel > 0f)
            {
                // Procedural Perlin flicker
                float noise = Mathf.PerlinNoise((Time.time * flickerSpeed) + _noiseOffset, 0f);
                float flicker = (noise * 2f - 1f) * flickerIntensityVariance;
                _lanternLight.intensity = Mathf.Max(0.2f, targetIntensity + flicker);
                _lanternLight.color = Color.Lerp(normalColor, spiritProximityColor, 0.5f);
            }
            else
            {
                _lanternLight.intensity = targetIntensity;
                _lanternLight.color = normalColor;
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
        /// Refuel the lantern.
        /// </summary>
        public void Refuel(float amount)
        {
            currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
        }
    }
}