using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] 
    private float cameraShakeDuration = 0.1f;
    [SerializeField]
    private float shakeAmount = 0.2f;
    private bool isShaking = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShakeItUp();
        }
    }

    private IEnumerator Shake()
    {
        if (isShaking)
        {
            yield return null;
        }
        isShaking = true;
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < cameraShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            transform.localPosition = new Vector3(originalPos.x + x,
                originalPos.y + y, originalPos.z);
            
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
        isShaking=false;
    }
    public void ShakeItUp()
    {
        StartCoroutine(Shake());
    }

}