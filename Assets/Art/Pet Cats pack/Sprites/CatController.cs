using System.Collections;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [Header("Punto al que se teletransporta el gato")]
    [SerializeField] private Transform firstPoint;

    private Animator animator;
    private bool segundaTransi = false;

    [SerializeField]
    private GameObject Player;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (segundaTransi)
        {
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            // Muestra la distancia por consola
            Debug.Log($"Distancia al Player: {distance:F2} unidades");

            // Comprueba si está dentro del umbral
            if (distance <= 2f)
            {
                animator.SetTrigger("secondTransition");
                // Aquí tu lógica cuando esté cerca
                Debug.Log("¡Estás lo suficientemente cerca!");
                // Por ejemplo, activar interacción:
                // EnableInteractionUI();
            }
        }
    }
    /// <summary>
    /// Llamado por Animation Event en el último frame
    /// de la animación de feedback del gato.
    /// </summary>
    public void firstTransition()
    {
        Debug.Log("firstTransition invocado");
        StartCoroutine(TeleportAndPlayNext());
    }

    private IEnumerator TeleportAndPlayNext()
    {
        // Espera un frame para que termine de aplicar la animación actual
        yield return null;

        // 1) Desactiva el Animator para que no reescriba la posición
        animator.enabled = false;

        // 2) Teletransporta al gato
        transform.position = firstPoint.position;
        Debug.Log("Teleport realizado");

        // 3) Espera otro frame para asegurarte de que el cambio de posición se asienta
        yield return null;

        // 4) Reactiva el Animator
        animator.enabled = true;

        // 5) Dispara la animación siguiente
        animator.SetTrigger("firstTransition");

        segundaTransi = true;
    }
    public void lastTransition()
    {
        animator.SetTrigger("lastTransition");
    }
}


