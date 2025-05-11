using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightningController : MonoBehaviour
{
    [Header("Lightning Settings")]
    [SerializeField] private Light2D globalLight;
    [SerializeField] private List<Light2D> windowLights = new List<Light2D>();
    [SerializeField] private AudioSource thunderAudioSource;

    [Header("Lightning Timing")]
    [SerializeField, Range(1f, 20f)] private float minTimeBetweenLightning = 5f;
    [SerializeField, Range(5f, 30f)] private float maxTimeBetweenLightning = 10f;
    [SerializeField, Range(0f, 3f)] private float minThunderDelay = 0.3f;
    [SerializeField, Range(0.1f, 6f)] private float maxThunderDelay = 2.5f;

    [Header("Lightning Visuals")]
    [SerializeField] private AnimationCurve lightningIntensityCurve;
    [SerializeField, Range(0.1f, 3f)] private float lightningDuration = 0.7f;
    [SerializeField, Range(1f, 10f)] private float globalLightningIntensity = 3f;
    [SerializeField, Range(1f, 10f)] private float windowLightningIntensity = 5f;
    [SerializeField] private Color lightningColor = new Color(0.9f, 0.9f, 1f);

    [Header("Thunder Sounds")]
    [SerializeField] private List<AudioClip> thunderSounds = new List<AudioClip>();
    [SerializeField, Range(0.5f, 1.5f)] private float minPitch = 0.8f;
    [SerializeField, Range(0.5f, 1.5f)] private float maxPitch = 1.2f;
    [SerializeField, Range(0f, 1f)] private float thunderVolume = 0.7f;

    // Cached original values
    private float originalGlobalIntensity;
    private List<float> originalWindowIntensities = new List<float>();
    private List<Color> originalWindowColors = new List<Color>();

    private void Start()
    {
        // Store original light values
        if (globalLight != null)
        {
            originalGlobalIntensity = globalLight.intensity;
        }

        foreach (Light2D light in windowLights)
        {
            if (light != null)
            {
                originalWindowIntensities.Add(light.intensity);
                originalWindowColors.Add(light.color);
            }
            else
            {
                originalWindowIntensities.Add(1f);
                originalWindowColors.Add(Color.white);
            }
        }

        // Start the lightning coroutine
        StartCoroutine(LightningStormCoroutine());
    }

    private IEnumerator LightningStormCoroutine()
    {
        while (true)
        {
            // Random wait time between lightning strikes
            float waitTime = Random.Range(minTimeBetweenLightning, maxTimeBetweenLightning);
            yield return new WaitForSeconds(waitTime);

            // Determine if we should do a single flash or multiple flashes
            bool multipleFlashes = Random.value > 0.7f;
            int numFlashes = multipleFlashes ? Random.Range(2, 5) : 1;

            for (int i = 0; i < numFlashes; i++)
            {
                // Trigger the lightning effect
                yield return StartCoroutine(LightningFlashCoroutine());

                if (multipleFlashes && i < numFlashes - 1)
                {
                    // Small pause between multiple flashes
                    yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
                }
            }

            // Calculate thunder delay based on "distance"
            float thunderDelay = Random.Range(minThunderDelay, maxThunderDelay);
            yield return new WaitForSeconds(thunderDelay);

            // Play thunder sound
            PlayThunderSound();
        }
    }

    private IEnumerator LightningFlashCoroutine()
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < lightningDuration)
        {
            elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / lightningDuration;

            // Get lightning intensity from animation curve
            float lightningFactor = lightningIntensityCurve.Evaluate(normalizedTime);

            // Apply to global light
            if (globalLight != null)
            {
                globalLight.intensity = originalGlobalIntensity + (globalLightningIntensity * lightningFactor);
            }

            // Apply to all window lights
            for (int i = 0; i < windowLights.Count; i++)
            {
                if (windowLights[i] != null)
                {
                    windowLights[i].intensity = originalWindowIntensities[i] + (windowLightningIntensity * lightningFactor);

                    // Blend color toward lightning color
                    windowLights[i].color = Color.Lerp(originalWindowColors[i], lightningColor, lightningFactor * 0.7f);
                }
            }

            yield return null;
        }

        // Restore original light values
        if (globalLight != null)
        {
            globalLight.intensity = originalGlobalIntensity;
        }

        for (int i = 0; i < windowLights.Count; i++)
        {
            if (windowLights[i] != null)
            {
                windowLights[i].intensity = originalWindowIntensities[i];
                windowLights[i].color = originalWindowColors[i];
            }
        }
    }

    private void PlayThunderSound()
    {
        if (thunderAudioSource == null || thunderSounds.Count == 0)
            return;

        // Select random thunder sound
        AudioClip thunderClip = thunderSounds[Random.Range(0, thunderSounds.Count)];

        // Set parameters and play
        thunderAudioSource.clip = thunderClip;
        thunderAudioSource.pitch = Random.Range(minPitch, maxPitch);
        thunderAudioSource.volume = thunderVolume;
        thunderAudioSource.Play();
    }

    // For manual testing or triggering lightning via other game events
    public void TriggerLightning()
    {
        StartCoroutine(LightningFlashCoroutine());

        // Play thunder after delay
        StartCoroutine(DelayedThunder());
    }

    private IEnumerator DelayedThunder()
    {
        float delay = Random.Range(minThunderDelay, maxThunderDelay);
        yield return new WaitForSeconds(delay);
        PlayThunderSound();
    }
}