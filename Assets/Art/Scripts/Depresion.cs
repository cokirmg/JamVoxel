using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Depresion : MonoBehaviour
{
    public float maxSpeed = 40.0f;
    public float timeInSofa = 5.0f;
    private float runSpeed = 0f;
    private bool depresion = false;
    private int x = 0;
        [SerializeField]
    private GameObject FeedbackInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    transform.Translate(Vector3.right * runSpeed * Time.deltaTime);

    if (Input.GetKeyDown(KeyCode.D))
    {
        if (depresion) return; // No hacer nada si está en depresión

        if(x==0){
        this.gameObject.GetComponent<Animator>().SetTrigger("sleeping");
        x++;
        }

        if (transform.position.x > -24.0f && transform.position.x < -14.0f)
        {
            addSpeed(30);
        }
        else if (transform.position.x >= -14.0f && transform.position.x < -4.0f)
        {
            addSpeed(28);
        }
        else if (transform.position.x >= 6.0f && transform.position.x < 16.0f)
        {
            addSpeed(26);
        }
        else
        {
            addSpeed(23);
        }
        this.gameObject.GetComponent<Animator>().SetBool("Andar", true);
    }else{
        this.gameObject.GetComponent<Animator>().SetBool("Andar", false);
    }

    lessSpeed();
}

    void addSpeed(int Speed){
        if(runSpeed < maxSpeed){
            runSpeed = runSpeed + Speed;
        }
    }

    void lessSpeed(){
        if(runSpeed>0){
            runSpeed--;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Interact"))
        {
            depresion = true;
            this.gameObject.GetComponent<Animator>().SetBool("depresion", true);
            FeedbackInteract.SetActive(false);
            StartCoroutine(WaitTime());

        }


    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(timeInSofa);
        GameObject sceneManager = GameObject.Find("SceneManager");
        if (sceneManager) sceneManager.GetComponent<SceneController2>().NextLevel();
    }
}
