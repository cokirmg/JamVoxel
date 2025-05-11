using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2 : MonoBehaviour
{
    public static SceneController2 instance;
    [SerializeField] Animator animator;
    [SerializeField] int[] tasksPerScene = {1,1,1,1,1,1};
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    //Se que essto no se hace asi pero son las 7am 
    private bool fade1 = false;
    private bool fade2 = false;
    private bool fade3 = false;
    private int currentTasksDone = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void NextLevel()
    {
        currentTasksDone++;

        if (tasksPerScene[SceneManager.GetActiveScene().buildIndex - 1] == currentTasksDone) 
        {
            currentTasksDone = 0;
            StartCoroutine(LoadLevel());
        }

            
    }

    public void TrampitaNextLevel() 
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && !fade1)
        {
            audioSource.volume = 0;
            audioSource.clip = audioClip;
            audioSource.Play();
            audioSource.volume = Mathf.Lerp(0, 0.3f, 0.1f);
            fade1 = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5 && !fade2)
        {
            audioSource.volume = Mathf.Lerp(0.3f, 0.6f, 0.1f);
            fade2 = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6 && !fade3)
        {
            audioSource.volume = Mathf.Lerp(0.6f, 0.9f, 0.1f);

            fade3 = true;
        }
    }

}
