using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class showerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject cover;

    private GameObject player;

    public void Interact()
    {
        cover.SetActive(true);
        player = GameObject.FindWithTag("Player");
        if (player != null) 
        {
            player.GetComponent<PlayerController>().blockControls = true;
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<PressKey>().enabled = true;
        }

    }
}
