using UnityEngine;

public class ALaCama : MonoBehaviour, IInteractable
{
    private GameObject sceneManager;

    public void Interact()
    {
        sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<SceneController2>().NextLevel();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
