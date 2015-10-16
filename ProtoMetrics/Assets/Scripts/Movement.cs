using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    public float speed = 3.0f;
    public float maxSpeed = 3.0f;
    public float angularSpeed = 1.8f;
    public float maxAngularSpeed = 1.8f;
    Rigidbody rigBody;

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

        float angular = angularSpeed * h;
        Vector3 speedVec = this.transform.forward * speed * v;

        rigBody.velocity = speedVec;
        //this.transform.position += Time.deltaTime * speed; // translation based on horizontal axis
        this.transform.Rotate(Vector3.up, angular); // rotation based ob vertical axis

    }


}
