using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    public float speed = 4.0f;
    public float maxSpeed = 4.0f;
    public float angularSpeed = 5.4f;
    public float maxAngularSpeed = 5.4f;
    Rigidbody rigBody;
    Camera cam;

    private Vector3 input;

    public DeathSystem death;

    // Use this for initialization
    void Start()
    {
        rigBody = (Rigidbody)GetComponent(typeof(Rigidbody));
        input = new Vector3();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        /*{
            // Debug draw for local coordinate system
            Vector3 origin = this.transform.position + 3.0f * this.transform.up;
            Debug.DrawLine(origin, origin + 2.0f * this.transform.right, Color.red);
            Debug.DrawLine(origin, origin + 2.0f * this.transform.up, Color.green);
            Debug.DrawLine(origin, origin + 2.0f * this.transform.forward, Color.blue);
        }*/
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Time.timeSinceLevelLoad <= 7.0f)
            input.Set(0, 0, 0);

        float angular = angularSpeed * input.x;
        Vector3 yVelocity = new Vector3(0,rigBody.velocity.y,0);
        Vector3 speedVec = this.transform.forward * speed * input.y;


        rigBody.velocity = speedVec + yVelocity;
        //this.transform.position += Time.deltaTime * speed; // translation based on horizontal axis
        this.transform.Rotate(Vector3.up, angular); // rotation based ob vertical axis

        if (rigBody.transform.position.y <= -1.0f && !death.dead)
            death.Kill("Runtergefallen",rigBody.transform.position);

    }


}
