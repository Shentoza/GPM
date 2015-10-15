using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject viewTarget;
    public float elevation = Mathf.PI / 4.0f;
    public float azimuth = 0.0f;

    public float distance = 15.0f;
    public float minDistance = 5.0f;
    public float maxDistance = 30.0f;

    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 xDest = new Vector3();

    public float distSpringConstant = 10.0f;
    public float mass = 1.0f;
    public bool dragged = false;

    // Use this for initialization
    void Start()
    {
        if (!viewTarget)
            Debug.LogError("viewTarget not set! SpringFollowCamera will not work!");

        xDest.x = distance * Mathf.Sin(azimuth) * Mathf.Sin(elevation);
        xDest.y = distance * Mathf.Cos(elevation);
        xDest.z = distance * -Mathf.Cos(azimuth) * Mathf.Sin(elevation);
    }

    // Update is called once per frame
    void Update()
    {
        //SetupCamera ();
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 localX, localY, localZ;
        localZ = viewTarget.transform.forward;
        localY = viewTarget.transform.up;
        localX = viewTarget.transform.right;

        Vector3 xWanted = xDest.x * localX + xDest.y * localY + xDest.z * localZ;
        xWanted += viewTarget.transform.position;

        Vector3 acceleration = new Vector3(0, 0, 0);
        acceleration += -velocity * 2.0f * Mathf.Sqrt(distSpringConstant) / mass;
        acceleration += -(this.transform.position - xWanted) * distSpringConstant / mass;

        this.transform.position += Time.deltaTime * velocity;
        this.transform.LookAt(viewTarget.transform.position);

        velocity = velocity + Time.deltaTime * acceleration;
    }
}