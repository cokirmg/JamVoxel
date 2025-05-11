using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AppearInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject objectToAppera;
    [SerializeField]
    private float timeToAppear = 0.5f, timeSonido2 = 0.5f;
    [SerializeField]
    private bool isLevelTask = false;
    [SerializeField]
    private bool increaseSpeed = false;
    [SerializeField]
    private float increaseAmount = 7;

    private GameObject sceneManager;
    private bool isPlaying = false;
    private GameObject daylight;


    [SerializeField] 
    private AudioClip sonido,sonido2;

    private AudioSource audioSource;

    private void Awake()
    {
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
            audioSource.PlayOneShot(sonido);
            this.gameObject.GetComponent<DeactivateZoneToDrop>().DeactivateCollider();
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
        StartCoroutine(StopAt(timeSonido2));

        //AUMENTAMOS LA VELOCIDAD Y EL CICLO DE DIA
        if (increaseSpeed)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player) player.GetComponent<PlayerController>().sumarSpeed(increaseAmount);
            daylight = GameObject.Find("GlobalLight");
            daylight.GetComponent<DayNightCycle>()._dayDuration -= 1.25f;

        }

        if (isLevelTask) 
        {
            sceneManager = GameObject.Find("SceneManager");
            if(sceneManager) sceneManager.GetComponent<SceneController2>().NextLevel();
        }

        this.enabled = false;
      //  gameObject.tag = "Untagged";
    }

    private IEnumerator StopAt(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        audioSource.Stop();
    }

}
