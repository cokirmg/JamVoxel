using System.Collections;
using UnityEngine;

public class MiniGame1 : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject spawnGameObject;

    private int pointsClicked;
    private bool minigameEnded = false;
    private int objectsSpawned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E)){
            GameObject circle = GameObject.Find("Circle(Clone)");
            if (circle != null)
            {
                Destroy(circle);
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
        }
    }
    IEnumerator spawnObjects()
    {
        if (!minigameEnded)
        {
            if (objectsSpawned <= 30)
            {
                Transform spawnTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(spawnGameObject, spawnTransform.position, Quaternion.identity);
                objectsSpawned++;
            }
            
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(spawnObjects());
        }
        else
        {
            Debug.Log("Minijuego pasado");
        }
        

    }
}
