using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float smoothSpeed;

    private Vector3 targetPos, newPos;

    public Vector3 minPos, maxPos;
    
    void Update()
    {
        if(transform.position != player.position)
        {
            //targetPos = player.position;

            Vector3 camBoundaryPos = new Vector3(
                Mathf.Clamp(transform.position.x, minPos.x, maxPos.x),
                Mathf.Clamp(transform.position.y, minPos.y, maxPos.y),
                Mathf.Clamp(transform.position.z, minPos.z, maxPos.z));

            newPos = Vector3.Lerp(transform.position, camBoundaryPos, smoothSpeed);
            transform.position = newPos;
        }
    }
}
