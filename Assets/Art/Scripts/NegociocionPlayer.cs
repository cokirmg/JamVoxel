using UnityEngine;


public class NegociocionPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject Cat;
    private bool isSleeping;
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerController.sleeping)
        {
            Debug.Log("Entre sabes en la chingadera");
            Cat.GetComponent<Animator>().SetTrigger("start");
        }
    }

}
