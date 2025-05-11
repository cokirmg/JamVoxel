using UnityEngine;
using UnityEngine.SceneManagement;

public class catFood : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        GameObject sceneManager = GameObject.Find("SceneManager");
        if (sceneManager) sceneManager.GetComponent<SceneController2>().NextLevel();
    }
}
