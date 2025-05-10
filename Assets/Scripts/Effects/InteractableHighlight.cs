using UnityEngine;

public class InteractableHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    public Material defaultMaterial;

    private SpriteRenderer spriteRenderer;
    private bool canInteract = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Make sure we start with default material
        spriteRenderer.material = defaultMaterial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Activate highlight
            spriteRenderer.material = highlightMaterial;
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the exiting object is the player
        if (collision.CompareTag("Player"))
        {
            // Deactivate highlight
            spriteRenderer.material = defaultMaterial;
            canInteract = false;
        }
    }

    private void Update()
    {
        // Handle interaction if player presses the interact key
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            // Interaction code here
            Debug.Log("Player interacted with " + gameObject.name);
        }
    }
}