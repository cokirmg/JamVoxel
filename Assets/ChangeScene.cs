using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneController2.instance.NextLevel();
        }
    }
}
