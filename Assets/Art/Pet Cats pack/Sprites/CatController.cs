using System.Collections;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [Header("Punto al que se teletransporta el gato")]
    [SerializeField]
    private Transform firstPoint;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Llamado por un Animation Event en el �ltimo frame
    /// de la animaci�n de feedback del gato.
    /// </summary>
    public void firstTransition()
    {
        Debug.Log("firstTransition invocado");
        StartCoroutine(DoTeleportNextFrame());
    }

    private IEnumerator DoTeleportNextFrame()
    {
        // 1 frame de espera para que Unity aplique el keyframe final
        yield return null;

        

        // 2) Desactiva root motion por si queda alguna curva
        animator.applyRootMotion = false;

        // 3) Lanza la animaci�n siguiente (ahora sin mover la posici�n)
        animator.SetTrigger("firstTransition");
        transform.position = firstPoint.position;



    }
}


