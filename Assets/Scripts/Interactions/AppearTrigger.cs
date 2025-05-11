using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class AppearTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToAppera;
    [SerializeField]
    private bool increaseSpeed = false;
    [SerializeField]
    private float increaseAmount = 3;

    [SerializeField]
    private AudioClip sonido;

    private AudioSource audioSource;
    private bool used = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used && collision.CompareTag("Player"))
        {
            if (objectToAppera != null)
            {
                used = true;
                audioSource.clip = sonido;
                audioSource.Play();
                objectToAppera.SetActive(true);

                //AUMENTAMOS LA VELOCIDAD
                if (increaseSpeed)
                {
                    GameObject player = GameObject.FindWithTag("Player");
                    if (player) player.GetComponent<PlayerController>().sumarSpeed(increaseAmount);
                }

                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
