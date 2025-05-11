using System.Collections;
using UnityEngine;

public class AppearDisappearInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject objectToAppera;
    [SerializeField]
    private float timeToAppear = 0.5f, timeToDesappear = 0.5f;
    [SerializeField]
    private bool isLevelTask = false;

    private GameObject sceneManager;
    private bool isPlaying = false;


    [SerializeField]
    private AudioClip sonido, sonido2;

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
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.clip = sonido;
            audioSource.Play();
            StartCoroutine(StopAt(timeToAppear));
            StartCoroutine(WaitTime());
        }

    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(timeToAppear);
        objectToAppera.SetActive(true);
        audioSource.clip = sonido2;
        audioSource.Play();
        StartCoroutine(StopAt(timeToDesappear));

        yield return new WaitForSeconds(timeToDesappear);
        objectToAppera.SetActive(false);



        if (isLevelTask)
        {
            sceneManager = GameObject.Find("SceneManager");
            if (sceneManager) sceneManager.GetComponent<SceneController2>().NextLevel();
        }
        this.enabled = false;
        gameObject.tag = "Untagged";
    }

    private IEnumerator StopAt(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        audioSource.Stop();
    }
}
