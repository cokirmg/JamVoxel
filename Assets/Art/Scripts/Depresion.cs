using UnityEngine;

public class Depresion : MonoBehaviour
{
    public float maxSpeed = 40.0f;
    private float runSpeed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E)){
            if (transform.position.x > -10.0f && transform.position.x < 10.0f){
                addSpeed(25);
            }else if (transform.position.x >= 10.0f && transform.position.x < 20.0f){
                addSpeed(20);
            }else if(transform.position.x >= 20.0f && transform.position.x < 26.0f){
                addSpeed(15);
            }
            else{
                addSpeed(10);
            }
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
}
