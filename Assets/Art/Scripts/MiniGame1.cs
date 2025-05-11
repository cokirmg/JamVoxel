using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class MiniGame1 : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    public GameObject player;
    public GameObject camera;
    public GameObject VFX;

   // [SerializeField]
    //private GameObject spawnGameObject;

    private int pointsClicked;
    private bool minigameEnded = false;
    private int objectsSpawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnObjects());
        player.GetComponent<Depresion>().enabled = false;
        camera.GetComponent<CameraShake>().enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject[] clouds = GameObject.FindGameObjectsWithTag("cloud");
            foreach (GameObject cloud in clouds)
            {
                CloudSpawner scriptSpawner = cloud.GetComponent<CloudSpawner>();
                if (scriptSpawner != null && scriptSpawner.active)
                {
                    scriptSpawner.popCloud();
                    break; // si solo quieres "poppear" una nube por pulsación
                }
            }
        }
    }



    public void pushingObject()
    {
        pointsClicked++;
        objectsSpawned--;
        if(pointsClicked >= 10)
        {
            minigameEnded = true;
            Debug.Log("JUEGO TERMINADO");
            player.GetComponent<Depresion>().enabled = true;
        }
    }


    IEnumerator spawnObjects()
    {
        if (!minigameEnded)
        {
            if (objectsSpawned < 30)
            {
                GameObject[] clouds = GameObject.FindGameObjectsWithTag("cloud");

                // Crear una lista con solo los inactivos
                List<GameObject> inactiveClouds = new List<GameObject>();
                foreach (GameObject cloud in clouds)
                {
                    CloudSpawner scriptSpawner = cloud.GetComponent<CloudSpawner>();
                    if (scriptSpawner != null && !scriptSpawner.active)
                    {
                        inactiveClouds.Add(cloud);
                    }
                }

                if (inactiveClouds.Count > 0)
                {
                    // Elegir uno al azar de los inactivos
                    GameObject chosenCloud = inactiveClouds[Random.Range(0, inactiveClouds.Count)];
                    CloudSpawner scriptSpawner = chosenCloud.GetComponent<CloudSpawner>();
                    scriptSpawner.createCloud();
                    objectsSpawned++;
                }

            }else{
                player.GetComponent<Depresion>().enabled = true;
                camera.GetComponent<CameraShake>().enabled = true;
                VFX.SetActive(false); 

            }

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(spawnObjects());
        }
        else
        {
            Debug.Log("Minijuego pasado");
        }
    }

}
