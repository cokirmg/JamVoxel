using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float maxRunSpeed = 200f;

    float horizontalMove = 0f;
    private Animator anim;

    public bool invertControls = false;

    private GameObject objectToPickUp = null;
    public bool objectPicked = false;
    [SerializeField]
    private Transform pickUpPoint;
    private GameObject objectAtached;
    private GameObject ZonedWhereDropped;
    private GameObject gameManager;
    private bool zoneToDrop = false;
    public bool blockControls = true;
    private bool sleeping = true;
    // A�adimos la variable para almacenar el objeto interactuable
    private IInteractable currentInteractable = null;

    [SerializeField]
    private GameObject FeedbackInteract;

    void Start()
    {
        FeedbackInteract.GetComponent<Animation>().Play("Animation feedback");
        FeedbackInteract.SetActive(true);
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        
        if (!blockControls )
        {
            // Movimiento
            float direction = invertControls ? -1f : 1f;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * direction;
            
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            anim.SetTrigger("sleeping");
            sleeping = false;
            blockControls = false;
            this.gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            FeedbackInteract.SetActive(false);
        }

        anim.SetBool("Andar", Input.GetAxisRaw("Horizontal") != 0);

        // Si se presiona la tecla E, interactuamos

        if (Input.GetKeyDown(KeyCode.E) && !sleeping)
        {
            anim.SetTrigger("Interact");

            // Si tenemos un objeto para recoger, lo recogemos
            if (objectToPickUp != null && !objectPicked)
            {
                objectToPickUp.transform.SetParent(pickUpPoint);
                objectToPickUp.transform.localPosition = Vector3.zero;
                objectToPickUp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                objectPicked = true;
                objectAtached = objectToPickUp;
                objectToPickUp = null;
            }
            // Si estamos en una zona para dejar el objeto, lo dejamos
            if (zoneToDrop && objectPicked)
            {
                Destroy(objectAtached);
                gameManager.GetComponent<PickAndDropManager>().avanceNivel();
                ZonedWhereDropped.GetComponent<DeactivateZoneToDrop>().DeactivateCollider();
                objectPicked = false;
            }
            // Si tenemos un objeto interactuable, lo interactuamos
            if (currentInteractable != null)
            {
                Debug.Log("11111111111111111111");
                currentInteractable.Interact();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && sleeping)
        {
            anim.SetTrigger("sleeping");
            sleeping = false;
            blockControls = false;
            this.gameObject.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            FeedbackInteract.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }

    // Al entrar en el trigger de un objeto que se puede recoger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            objectToPickUp = other.gameObject;
        }

        // Al entrar en el trigger de un objeto interactuable
        if (other.CompareTag("Interact"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable; // Guardamos el objeto interactuable
            }
            FeedbackInteract.SetActive(true);
        }
    }

    // Al salir del trigger de un objeto que se puede recoger
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Object") && other.gameObject == objectToPickUp)
        {
            objectToPickUp = null;
        }

        // Al salir de un objeto interactuable
        if (other.CompareTag("Interact"))
        {
            if (other.GetComponent<IInteractable>() == currentInteractable)
            {
                currentInteractable = null; // Limpiamos la referencia
            }

            FeedbackInteract.SetActive(false);
        }
    }

    // Para las zonas de drop de objetos
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("zoneToDrop") && objectPicked)
        {
            zoneToDrop = true;
            ZonedWhereDropped = collision.gameObject;
        }
        else
        {
            zoneToDrop = false;
            
        }
    }

    public void sumarSpeed(float suma){
        if(runSpeed < maxRunSpeed){
            runSpeed += suma;
        }
    }

    
}

