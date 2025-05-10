using UnityEngine;
using UnityEngine.SceneManagement;

public class PickAndDropManager : MonoBehaviour
{
    private int conteo;
    [SerializeField]
    private int conteoDeseado;
    private GameObject player;
    private GameObject sceneManager;
    public void avanceNivel()
    {
        conteo++;
        //Si estamos en la fase de negación, invertimos los controles again
        if(conteo >= conteoDeseado)
        {
            //TODO cambio de escena
            Debug.Log("TODO cambio de escena");
            sceneManager = GameObject.Find("SceneManager");
            sceneManager.GetComponent<SceneController2>().NextLevel();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            player = GameObject.FindWithTag("Player");

            player.GetComponent<PlayerController>().invertControls = !player.GetComponent<PlayerController>().invertControls;
        }
    }
}
