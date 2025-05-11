using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame1 : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    public GameObject player;
    public GameObject camera;
    public GameObject VFX;

    private int pointsClicked;
    private bool minigameEnded = false;
    private int objectsSpawned;
    private bool finalStateTriggered = false;

    void Start()
    {
        StartCoroutine(spawnObjects());
        player.GetComponent<Depresion>().enabled = false;
        camera.GetComponent<CameraShake>().enabled = false;
    }

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
                    break; // Solo desactiva una nube por pulsación
                }
            }
        }

        CheckForEnd(); // Verifica si se deben activar los scripts finales
    }

    public void pushingObject()
    {
        pointsClicked++;
        objectsSpawned--;

        if (pointsClicked >= 10)
        {
            minigameEnded = true;
            Debug.Log("Minijuego terminado, esperando que se eliminen todas las nubes...");
        }
    }

    IEnumerator spawnObjects()
    {
        while (!minigameEnded)
        {
            if (objectsSpawned < 25)
            {
                GameObject[] clouds = GameObject.FindGameObjectsWithTag("cloud");

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
                    GameObject chosenCloud = inactiveClouds[Random.Range(0, inactiveClouds.Count)];
                    CloudSpawner scriptSpawner = chosenCloud.GetComponent<CloudSpawner>();
                    scriptSpawner.createCloud();
                    objectsSpawned++;
                }
            }else{
                minigameEnded = true;
            }

            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("Minijuego pasado.");
    }

    void CheckForEnd()
    {
        if (minigameEnded && !finalStateTriggered)
        {
            GameObject[] clouds = GameObject.FindGameObjectsWithTag("cloud");
            bool allPopped = true;

            foreach (GameObject cloud in clouds)
            {
                CloudSpawner scriptSpawner = cloud.GetComponent<CloudSpawner>();
                if (scriptSpawner != null && scriptSpawner.active)
                {
                    allPopped = false;
                    break;
                }
            }

            if (allPopped)
            {
                finalStateTriggered = true;

                player.GetComponent<Depresion>().enabled = true;
                camera.GetComponent<CameraShake>().enabled = true;
                VFX.SetActive(false);

                Debug.Log("Todas las nubes fueron eliminadas. Activando scripts.");
            }
        }
    }
}
