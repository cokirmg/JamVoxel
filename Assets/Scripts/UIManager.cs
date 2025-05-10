using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image titleImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color color = titleImage.color;
        color.a = 0f;
        titleImage.color = color;

        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / 2);
            titleImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color color = titleImage.color;
        color.a = 1f;
        titleImage.color = color;

        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / 2);
            titleImage.color = color;
            yield return null;
        }
    }
}
