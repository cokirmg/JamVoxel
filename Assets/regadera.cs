using UnityEngine;
using System.Collections;

public class regadera : MonoBehaviour, IInteractable
{
    private Quaternion originalRotation;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(){
        //if(controller.objectPicked){
            StartCoroutine(grower());
        //}
    }

    IEnumerator grower()
    {
        Debug.Log("fgbgfbgfb");
        // Girar 90 grados (en z)
        transform.rotation = Quaternion.Euler(0, 0, 90);

        // Esperar 1 segundo (puedes cambiarlo según tu animación)
        yield return new WaitForSeconds(1f);

        // Volver a la rotación original
        transform.rotation = originalRotation;
    }
}



