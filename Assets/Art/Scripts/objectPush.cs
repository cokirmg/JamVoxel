using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class objectPush : MonoBehaviour
{

    private GameObject miniGame;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniGame = GameObject.Find("Minigame");
    }

    // Update is called once per frame
    /*void Update()
    {
       
            if (Input.GetKeyDown(KeyCode.E)) // clic izquierdo
            {
                Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(posicionMouse, Vector2.zero);

                if (hit.collider != null && hit.collider.tag == "objectToDestroy")
                {
                    miniGame.GetComponent<MiniGame1>().pushingObject();
                    Destroy(hit.collider.gameObject); // Elimina el objeto tocado
                                                      // Alternativa: hit.collider.gameObject.SetActive(false); // Solo lo oculta
                                                      //Guayaba electrica
                }
            }
        
    }*/
}
