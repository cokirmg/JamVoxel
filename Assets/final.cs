using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class final : MonoBehaviour, IInteractable
{
    public TopToBottomLightEffect topToBottomLightEffect;
    public GameObject[] objs;
    public GameObject player;
    private PlayerController controller;

    void Start()
    {
        controller = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    public void Interact(){
        if(controller.objectPicked){
            StartCoroutine(show());
            //topToBottomLightEffect.TriggerLightAppearance();
            StartCoroutine(WaitEnd());
        }
    }

IEnumerator show()
{
    float duration = 1f;

    foreach (GameObject obj in objs)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            StartCoroutine(FadeIn(sr, duration));
        }
    }

    yield return null;
}

IEnumerator FadeIn(SpriteRenderer sr, float duration)
{
    Color c = sr.color;
    c.a = 0f;
    sr.color = c;

    float elapsed = 0f;

    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        c.a = Mathf.Clamp01(elapsed / duration);
        sr.color = c;
        yield return null;
    }
}

    private IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(7);
    }

}
