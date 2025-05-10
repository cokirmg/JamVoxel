using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private Animator anim;

    public bool invertControls = false;

    private GameObject objectToPickUp = null;
    private bool objectPicked = false;
    [SerializeField]
    private Transform pickUpPoint;
    private GameObject objectAtached;
    private GameObject gameManager;
    private bool zoneToDrop = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        float direction = invertControls ? -1f : 1f;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * direction;

        anim.SetBool("Andar", Input.GetAxisRaw("Horizontal") != 0);

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Interact");

            if (objectToPickUp != null)
            {
                objectToPickUp.transform.SetParent(pickUpPoint);
                objectToPickUp.transform.localPosition = Vector3.zero;
                objectToPickUp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                objectPicked = true;
                objectAtached = objectToPickUp;
                objectToPickUp = null;
            }
            if (zoneToDrop)
            {
                gameManager.GetComponent<PickAndDropManager>().avanceNivel();
                Destroy(objectAtached);
            }
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            objectToPickUp = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Object") && other.gameObject == objectToPickUp)
        {
            objectToPickUp = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("zoneToDrop") && objectPicked)
        {
            zoneToDrop = true;
        }

        else
        {
            zoneToDrop = false;
        }
        }
    }
