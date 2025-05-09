using UnityEngine;

public class DropAndSpawnNewObjArea : MonoBehaviour, IObjDropArea
{
    [SerializeField] private GameObject objectToSpawn;
    public void OnObjDrop(DragableObj obj)
    {
        Destroy(obj.gameObject);
        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
