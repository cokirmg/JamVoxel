using UnityEngine;

public class Depresion : MonoBehaviour
{
    public float maxSpeed = 40.0f;
    private float runSpeed = 0f;
    private bool depresion = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    transform.Translate(Vector3.right * runSpeed * Time.deltaTime);

    if (Input.GetKeyDown(KeyCode.E))
    {
        if (depresion) return; // No hacer nada si está en depresión

        this.gameObject.GetComponent<Animator>().SetTrigger("sleeping");

        if (transform.position.x > -10.0f && transform.position.x < 10.0f)
        {
            addSpeed(25);
        }
        else if (transform.position.x >= 10.0f && transform.position.x < 20.0f)
        {
            addSpeed(23);
        }
        else if (transform.position.x >= 20.0f && transform.position.x < 26.0f)
        {
            addSpeed(20);
        }
        else
        {
            addSpeed(17);
        }

        this.gameObject.GetComponent<Animator>().SetBool("Andar", true);
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
        }
    }
}
