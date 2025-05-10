using UnityEngine;
using UnityEngine.Rendering.Universal;

// Attach this to each window object in your scene
public class WindowLight : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Light2D _light;
    [SerializeField] private SpriteRenderer _windowSprite;

    [Header("Window Settings")]
    [SerializeField] private bool _isExteriorWindow = true;
    [SerializeField] private float _baseIntensityMultiplier = 1f;
    [SerializeField] private Vector2 _flickerRange = new Vector2(0.9f, 1.1f);
    [SerializeField] private float _flickerSpeed = 0.5f;
    [SerializeField] private bool _useFlicker = false;

    [Header("Window Light Shape")]
    [SerializeField] private float _angleSpread = 45f; // For directional windows
    [SerializeField] private bool _castShadows = true;

    private DayNightCycle _dayNightCycle;
    private float _flickerTime;
    private float _originalIntensity;

    private void Start()
    {
        _dayNightCycle = FindObjectOfType<DayNightCycle>();

        if (_dayNightCycle != null)
        {
            // Register this window with the day/night cycle system
            if (_light != null && _isExteriorWindow)
            {
                _originalIntensity = _light.intensity;

                // Subscribe to time changes to update lighting
                _dayNightCycle.OnTimeChanged += UpdateWindowLight;
            }
        }

        SetupShadows();
    }

    private void SetupShadows()
    {
        if (_light != null)
        {
            _light.shadowIntensity = _castShadows ? 0.8f : 0f;

            // Configure light shape if it's directional
            if (_light.lightType == Light2D.LightType.Freeform || _light.lightType == Light2D.LightType.Sprite)
            {
                // For directional windows, you might need additional setup here
                // This depends on how you've configured your lights
            }
        }
    }

    private void Update()
    {
        if (_useFlicker && _light != null)
        {
            ApplyFlicker();
        }
    }

    private void ApplyFlicker()
    {
        _flickerTime += Time.deltaTime * _flickerSpeed;

        // Create a natural flickering effect using Perlin noise
        float flickerFactor = Mathf.Lerp(_flickerRange.x, _flickerRange.y,
            Mathf.PerlinNoise(_flickerTime, 0f));

        // Apply the flicker to the light's current intensity
        _light.intensity = _originalIntensity * flickerFactor * _baseIntensityMultiplier;
    }

    private void UpdateWindowLight(float timeOfDay)
    {
        if (_light == null || _windowSprite == null) return;

        // The main DayNightCycle script will handle the actual color and intensity changes
        // This is just a hook for any window-specific behavior you might want

        // Update the original intensity reference (the DayNightCycle will modify the actual light)
        _originalIntensity = _light.intensity;

        // You could add window-specific behavior here
        // For example, special effects when sunlight directly hits this window
        bool isSunlightDirect = IsSunlightDirect(timeOfDay);

        if (isSunlightDirect)
        {
            // Maybe create a sunbeam effect or brightened window frame
            _windowSprite.material.SetFloat("_Brightness", 1.2f);

            // You could also spawn a temporary particle effect here
        }
        else
        {
            _windowSprite.material.SetFloat("_Brightness", 1.0f);
        }
    }

    private bool IsSunlightDirect(float timeOfDay)
    {
        // This is a placeholder for your game's logic
        // Determine if this window is currently receiving direct sunlight based on
        // the time of day and the window's orientation/position

        // Example: East-facing windows get morning light, west-facing get evening light
        // This is just a placeholder, replace with your actual logic

        // Assuming the window has a tag or property indicating its orientation:
        string orientation = gameObject.tag;

        switch (orientation)
        {
            case "EastWindow":
                // Morning light (sunrise)
                return timeOfDay >= 0.2f && timeOfDay <= 0.4f;
            case "SouthWindow":
                // Midday light
                return timeOfDay >= 0.4f && timeOfDay <= 0.6f;
            case "WestWindow":
                // Evening light (sunset)
                return timeOfDay >= 0.6f && timeOfDay <= 0.8f;
            default:
                return false;
        }
    }

    private void OnDestroy()
    {
        if (_dayNightCycle != null)
        {
            _dayNightCycle.OnTimeChanged -= UpdateWindowLight;
        }
    }
}