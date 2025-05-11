using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RainWindowEffect : MonoBehaviour
{
    [Header("Rain Effect Settings")]
    [SerializeField] private Light2D windowLight;
    [SerializeField] private SpriteRenderer windowLightSprite;
    [SerializeField] private Material rainMaterial;

    [Header("Animation Settings")]
    [SerializeField, Range(0.1f, 2f)] private float rainIntensity = 1f;
    [SerializeField, Range(0.1f, 5f)] private float animationSpeed = 1f;
    [SerializeField, Range(0f, 1f)] private float intensityVariation = 0.1f;

    [Header("Rain Pattern")]
    [SerializeField] private Texture2D rainNormalMap;
    [SerializeField] private Color rainTint = new Color(0.8f, 0.8f, 1f, 1f);

    // Cached material instance
    private Material instancedRainMaterial;
    private float baseIntensity;
    private float timeSinceStart;

    private void Start()
    {
        // Create a unique instance of the material
        instancedRainMaterial = new Material(rainMaterial);

        // Apply the material to the window light sprite
        if (windowLightSprite != null)
        {
            windowLightSprite.material = instancedRainMaterial;
        }

        // Store the base light intensity
        if (windowLight != null)
        {
            baseIntensity = windowLight.intensity;
        }

        // Set initial material properties
        if (instancedRainMaterial != null)
        {
            instancedRainMaterial.SetTexture("_NormalMap", rainNormalMap);
            instancedRainMaterial.SetColor("_RainTint", rainTint);
            instancedRainMaterial.SetFloat("_RainIntensity", rainIntensity);
        }
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime * animationSpeed;

        // Animate the rain material
        if (instancedRainMaterial != null)
        {
            // Offset the normal map UV to create a flowing effect
            instancedRainMaterial.SetFloat("_TimeOffset", timeSinceStart);

            // Vary the distortion amount slightly over time
            float noiseValue = Mathf.PerlinNoise(timeSinceStart * 0.3f, timeSinceStart * 0.2f);
            float currentIntensity = rainIntensity * (1f + (noiseValue - 0.5f) * 0.5f);
            instancedRainMaterial.SetFloat("_RainIntensity", currentIntensity);
        }

        // Vary the light intensity to simulate rain affecting the light
        if (windowLight != null)
        {
            // Use different noise pattern for light intensity
            float lightNoise = Mathf.PerlinNoise(timeSinceStart * 0.4f, timeSinceStart * 0.6f);
            // The light gets slightly dimmer in a natural pattern
            windowLight.intensity = baseIntensity * (1f - intensityVariation * lightNoise);
        }
    }

    // Set rain intensity at runtime if needed
    public void SetRainIntensity(float intensity)
    {
        rainIntensity = Mathf.Clamp(intensity, 0.1f, 2f);
        if (instancedRainMaterial != null)
        {
            instancedRainMaterial.SetFloat("_RainIntensity", rainIntensity);
        }
    }
}