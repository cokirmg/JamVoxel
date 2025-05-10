using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] 
    private GameObject player;
    [SerializeField] 
    private float cameraShakeDuration = 0.1f;
    [SerializeField]
    private float shakeAmount = 0.2f;
    private bool isShaking = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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

            float x = 0f;
            float y = 0f;

            if (player.transform.position.x > -10.0f && transform.position.x < 10.0f){
                x = Random.Range(-0.1f, 0.1f) * shakeAmount;
                y = Random.Range(-0.1f, 0.1f) * shakeAmount;
            }else if (transform.position.x >= 10.0f && transform.position.x < 20.0f){
                x = Random.Range(-0.3f, 0.3f) * shakeAmount;
                y = Random.Range(-0.3f, 0.3f) * shakeAmount;
            }else if(transform.position.x >= 20.0f && transform.position.x < 26.0f){
                x = Random.Range(-0.6f, 0.6f) * shakeAmount;
                y = Random.Range(-0.6f, 0.6f) * shakeAmount;
            }
            else{
                x = Random.Range(-1f, 1f) * shakeAmount;
                y = Random.Range(-1f, 1f) * shakeAmount;
            }





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