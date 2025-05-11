using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Chaito : MonoBehaviour
{
    public float tiempo = 4.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Ahhhh(tiempo));
    }

    private IEnumerator Ahhhh(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        gameObject.SetActive(false);
    }


}
