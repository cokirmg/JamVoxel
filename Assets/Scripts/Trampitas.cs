using UnityEngine;

public class Trampitas : MonoBehaviour
{
    private KeyCode keyToDetect = KeyCode.P;

    void Update()
    {
        if (Input.GetKeyDown(keyToDetect))
        {
            GameObject sceneManager = GameObject.Find("SceneManager");
            if (sceneManager) sceneManager.GetComponent<SceneController2>().TrampitaNextLevel();
        }
    }
}
