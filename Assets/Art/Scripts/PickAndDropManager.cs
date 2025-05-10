using UnityEngine;

public class PickAndDropManager : MonoBehaviour
{
    private int conteo;
    [SerializeField]
    private int conteoDeseado;
    
    public void avanceNivel()
    {
        conteo++;
        if (conteo >= conteoDeseado)
        {
            //TODO cambio de escena
            Debug.Log("TODO cambio de escena");
        }
    }
}
