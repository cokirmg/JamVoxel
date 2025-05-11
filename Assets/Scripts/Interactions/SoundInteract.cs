using System.Collections;
using UnityEngine;

public class SoundInteract : MonoBehaviour, IInteractable
{

    [SerializeField] 
    private AudioClip sonido;    
    [SerializeField] 
    private float maxDuration = 1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        // Buscar o agregar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Interact()
    {
        audioSource.clip = sonido;
        audioSource.Play();
        StartCoroutine(StopAt(maxDuration));

    }

    private IEnumerator StopAt(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        audioSource.Stop();
    }

}
