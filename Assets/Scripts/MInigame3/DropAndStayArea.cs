using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DropAndStayArea : MonoBehaviour, IObjDropArea
{
    public void OnObjDrop(DragableObj obj)
    {
        obj.transform.position = transform.position;
        Debug.Log("Object dropped here");
    }
}
