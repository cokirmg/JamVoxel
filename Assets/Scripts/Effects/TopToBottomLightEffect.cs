using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TopToBottomLightEffect : MonoBehaviour
{
    [SerializeField] private Light2D volumetricLight;
    [SerializeField] private float fullHeight = 10f; // Height of the complete light
    [SerializeField] private float appearDuration = 3f; // How long it takes to fully appear
    [SerializeField] private float initialIntensity = 0.8f;

    private Material lightMaterial;
    private float appearProgress = 0f;
    private bool isAppearing = false;

    void Start()
    {
        // Ensure we have the light component
        if (volumetricLight == null)
            volumetricLight = GetComponent<Light2D>();

        // Store the initial intensity
        initialIntensity = volumetricLight.intensity;

        // Make the light invisible initially
        SetLightVisibility(0f);
    }

    void Update()
    {
        if (isAppearing)
        {
            // Increase progress over time
            appearProgress += Time.deltaTime / appearDuration;

            // Clamp progress to 0-1 range
            appearProgress = Mathf.Clamp01(appearProgress);

            // Update the light's appearance
            UpdateLightEffect();

            // Check if effect is complete
            if (appearProgress >= 1f)
                isAppearing = false;
        }
    }

    // Called to trigger the light appearance
    public void TriggerLightAppearance()
    {
        appearProgress = 0f;
        isAppearing = true;

        // Make sure the light object is active
        volumetricLight.gameObject.SetActive(true);
    }

    private void UpdateLightEffect()
    {
        // Create a mask using a shader property
        Shader.SetGlobalFloat("_LightRevealProgress", appearProgress);

        // Set the light intensity proportional to the progress
        SetLightVisibility(appearProgress);
    }

    private void SetLightVisibility(float visibility)
    {
        // Set light intensity
        volumetricLight.intensity = initialIntensity * visibility;
    }
}