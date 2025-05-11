using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    public Sprite[] sprites;
    public bool active = false;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createCloud(){
        int index = Random.Range(0, sprites.Length); // Elegir índice aleatorio
        spriteRenderer.sprite = sprites[index];      // Asignar sprite
        active = true;

        //transform.localScale = new Vector3(2f, 2f, 1f); // Escalar al doble
    }

    public void popCloud(){
        spriteRenderer.sprite = null; // Quitar el sprite
        active = false;
    }
}
