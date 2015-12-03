using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    public float speed = 4.0f;
    public float maxSpeed = 4.0f;
    public float angularSpeed = 3.2f;
    public float maxAngularSpeed = 3.2f;
    Rigidbody rigBody;

    public DeathSystem death;

    // Use this for initialization
    void Start()
    {
        rigBody = (Rigidbody)GetComponent(typeof(Rigidbody));
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

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Time.timeSinceLevelLoad <= 7.0f)
            v = h = 0.0f;

        float angular = angularSpeed * h;
        Vector3 yVelocity = new Vector3(0,rigBody.velocity.y,0);
        Vector3 speedVec = this.transform.forward * speed * v;

        rigBody.velocity = speedVec + yVelocity;
        //this.transform.position += Time.deltaTime * speed; // translation based on horizontal axis
        this.transform.Rotate(Vector3.up, angular); // rotation based ob vertical axis

        if (rigBody.transform.position.y <= -1.0f && !death.dead)
            death.Kill("Runtergefallen",rigBody.transform.position);

    }


}
