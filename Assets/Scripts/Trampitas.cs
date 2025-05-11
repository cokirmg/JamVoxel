using UnityEngine;
using UnityEngine.SceneManagement;

public class Trampitas : MonoBehaviour
{
    private KeyCode keyToDetect = KeyCode.P;
    private KeyCode keyToDetect2 = KeyCode.O;

    void Update()
    {
        if (Input.GetKeyDown(keyToDetect))
        {
            GameObject sceneManager = GameObject.Find("SceneManager");
            if (sceneManager) sceneManager.GetComponent<SceneController2>().TrampitaNextLevel();
        }

        if (Input.GetKeyDown(keyToDetect2))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
