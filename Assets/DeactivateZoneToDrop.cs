using UnityEngine;

public class DeactivateZoneToDrop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DeactivateCollider()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

}
