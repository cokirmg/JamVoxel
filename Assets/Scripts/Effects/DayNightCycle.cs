using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float _dayDuration = 120f; // Full day/night cycle in seconds (for real-time mode)
    [SerializeField] private bool _useRealTimeProgression = false;
    [Range(0f, 1f)]
    [SerializeField] private float _timeOfDay = 0.25f; // 0 = midnight, 0.25 = sunrise, 0.5 = noon, 0.75 = sunset, 1 = midnight

    [Header("Light Settings")]
    [SerializeField] private Gradient _lightColor;
    [SerializeField] private AnimationCurve _lightIntensity;
    [SerializeField] private List<Light2D> _windowLights = new List<Light2D>();
    [SerializeField] private List<SpriteRenderer> _windowSprites = new List<SpriteRenderer>();

    [Header("Interior Lights")]
    [SerializeField] private List<Light2D> _interiorLights = new List<Light2D>();
    [SerializeField] private float _interiorLightActivationTime = 0.7f; // When interior lights turn on (evening)
    [SerializeField] private float _interiorLightDeactivationTime = 0.3f; // When interior lights turn off (morning)

    [Header("Global Lighting")]
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D _globalLight;
    [SerializeField] private Gradient _ambientLightColor;
    [SerializeField] private AnimationCurve _ambientLightIntensity;

    // Events that can trigger time changes
    public delegate void TimeChangedHandler(float newTime);
    public event TimeChangedHandler OnTimeChanged;

    private void Update()
    {
        if (_useRealTimeProgression)
        {
            ProgressTimeRealtime();
        }

        UpdateLighting();
    }

    private void ProgressTimeRealtime()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;
        if (_timeOfDay >= 1f)
        {
            _timeOfDay -= 1f;
        }

        if (OnTimeChanged != null)
        {
            OnTimeChanged(_timeOfDay);
        }
    }

    public void AdvanceTime(float amount)
    {
        _timeOfDay += amount;
        if (_timeOfDay >= 1f)
        {
            _timeOfDay -= 1f;
        }

        if (OnTimeChanged != null)
        {
            OnTimeChanged(_timeOfDay);
        }

        UpdateLighting();
    }

    public void SetTime(float newTime)
    {
        _timeOfDay = Mathf.Clamp01(newTime);

        if (OnTimeChanged != null)
        {
            OnTimeChanged(_timeOfDay);
        }

        UpdateLighting();
    }

    private void UpdateLighting()
    {
        // Update window lights
        Color currentColor = _lightColor.Evaluate(_timeOfDay);
        float currentIntensity = _lightIntensity.Evaluate(_timeOfDay);

        foreach (Light2D light in _windowLights)
        {
            light.color = currentColor;
            light.intensity = currentIntensity;
        }

        // Update window sprites if needed (optional visual effect)
        foreach (SpriteRenderer window in _windowSprites)
        {
            // You could change the window sprite based on time of day
            // Or apply a color overlay
            window.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        }

        // Update interior lights
        bool shouldInteriorLightsBeOn = ShouldInteriorLightsBeOn();
        foreach (Light2D light in _interiorLights)
        {
            light.gameObject.SetActive(shouldInteriorLightsBeOn);
        }

        // Update global lighting
        if (_globalLight != null)
        {
            _globalLight.color = _ambientLightColor.Evaluate(_timeOfDay);
            _globalLight.intensity = _ambientLightIntensity.Evaluate(_timeOfDay);
        }
    }

    private bool ShouldInteriorLightsBeOn()
    {
        // Logic for when interior lights should be on
        // Lights turn on in the evening and turn off in the morning
        if (_timeOfDay > _interiorLightActivationTime || _timeOfDay < _interiorLightDeactivationTime)
        {
            return true;
        }
        return false;
    }

    // Public methods to progress time based on game events

    public void AdvanceToNextDayPhase()
    {
        // Advance to next major day phase (dawn, noon, dusk, night)
        float[] phases = { 0f, 0.25f, 0.5f, 0.75f };

        // Find the next phase
        for (int i = 0; i < phases.Length; i++)
        {
            if (_timeOfDay < phases[i])
            {
                SetTime(phases[i]);
                return;
            }
        }

        // If we're past the last phase, go to the first phase of next day
        SetTime(0f);
    }

    public void ProgressTimeByEvent(string eventName)
    {
        // You can implement custom logic here to advance time
        // based on specific in-game events
        switch (eventName)
        {
            case "minigame_completed":
                AdvanceTime(0.05f); // Advance time by 5% of a day
                break;
            case "major_story_event":
                AdvanceTime(0.15f); // Advance time by 15% of a day
                break;
            // Add more events as needed
            default:
                break;
        }
    }

    // For debugging
    public string GetTimeAsString()
    {
        int hours = Mathf.FloorToInt(_timeOfDay * 24);
        int minutes = Mathf.FloorToInt((_timeOfDay * 24 - hours) * 60);
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }
}