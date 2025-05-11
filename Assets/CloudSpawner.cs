using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour
{
    public Sprite[] sprites;
    public bool active = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float cooldownAfterPop = 1f; // Tiempo de espera antes de permitir nueva nube

    private bool onCooldown = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void createCloud()
    {
        if (onCooldown) return;

        int index = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[index];
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        active = true;
    }

    public void popCloud()
    {
        if (active)
        {
            StartCoroutine(FadeOutAndDisable());
        }
    }

    IEnumerator FadeOutAndDisable()
    {
        float duration = 0.25f;
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spriteRenderer.sprite = null;
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        active = false;

        // Iniciar cooldown
        onCooldown = true;
        yield return new WaitForSeconds(cooldownAfterPop);
        onCooldown = false;
    }
}
