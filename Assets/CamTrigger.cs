using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;

    CameraController camControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camControl = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            camControl.minPos = newCamPos;
            camControl.maxPos = newCamPos;

            //other.transform.position = newPlayerPos;

        }
    }
}
