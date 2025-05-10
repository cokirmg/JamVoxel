using UnityEngine;

public class PressKey : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyToDetect = KeyCode.E;
    [SerializeField]
    private Shower shower;

    void Update()
    {
        if (Input.GetKeyDown(keyToDetect))
        {
            if (shower != null) shower.OnKeyPressed();
        }

        if (Input.GetKeyUp(keyToDetect))
        {
            if (shower != null) shower.OnKeyReleased();
        }
    }
}
