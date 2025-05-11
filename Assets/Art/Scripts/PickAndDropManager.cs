using UnityEngine;
using UnityEngine.SceneManagement;

public class PickAndDropManager : MonoBehaviour
{
    private int conteo;
    [SerializeField]
    private int conteoDeseado;
    private GameObject player;
    private GameObject sceneManager;
    private GameObject camera;
    private AudioSource audioSource;
    [SerializeField] private AudioClip negationEffect;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void avanceNivel()
    {
        conteo++;
        //Si estamos en la fase de negación, invertimos los controles again
        if(conteo >= conteoDeseado)
        {

            sceneManager = GameObject.Find("SceneManager");
            sceneManager.GetComponent<SceneController2>().NextLevel();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            player = GameObject.FindWithTag("Player");
            camera = GameObject.Find("Main Camera");
            camera.GetComponent<CameraShake>().ShakeItUp();
            player.GetComponent<PlayerController>().invertControls = !player.GetComponent<PlayerController>().invertControls;
            audioSource.PlayOneShot(negationEffect);
        }
    }
}
