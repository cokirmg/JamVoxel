using UnityEngine;
using System.Collections;

public class grow : MonoBehaviour, IInteractable
{

    public GameObject obj;
    public GameObject player;
    private PlayerController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(){
        if(controller.objectPicked){
            StartCoroutine(grower());
        }
    }

   IEnumerator grower()
    {
        for (int i = 0; i < 3; i++)
        {
            // Escalado progresivo
            gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(FadeInAlpha());
    }

    IEnumerator FadeInAlpha()
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0f;
            sr.color = c;

            float duration = 1f; // Duración total de la transición
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                c.a = Mathf.Clamp01(elapsed / duration); // Valor de 0 a 1
                sr.color = c;
                yield return null;
            }
        }
    }


}
