using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private GameObject FadeOut;
    [SerializeField] Animator animator;
    public void Jugar()
    {
        FadeOut = GameObject.Find("FadeOut");
        if(FadeOut)
        {
            FadeOut.SetActive(true);

            //yield return new WaitForSeconds(2);
            audioSource.clip = audioClip;
            audioSource.Play();
            //yield return new WaitForSeconds(2);
            StartCoroutine(StopAt());
        }
    }

    private IEnumerator StopAt()
    {
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(1);
    }
    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
