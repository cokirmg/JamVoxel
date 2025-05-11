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

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        if(SceneManager.GetActiveScene().buildIndex + 1 == 4)
        {
            audioSource.volume = 0;
            audioSource.clip = audioClip;
            audioSource.Play();
            audioSource.volume = Mathf.Lerp(0, 20, 0.002f);
        }
        else if (SceneManager.GetActiveScene().buildIndex + 1 == 5)
        {
            audioSource.volume = Mathf.Lerp(20, 40, 0.002f);
        }
        else if (SceneManager.GetActiveScene().buildIndex + 1 == 6)
        {
            audioSource.volume = Mathf.Lerp(40, 60, 0.002f);
        }
        animator.SetTrigger("Start");        
    }

}
