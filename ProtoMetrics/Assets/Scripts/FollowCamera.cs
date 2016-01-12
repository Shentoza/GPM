using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject viewTarget;
    public float elevation = Mathf.PI / 8.0f;
    public float azimuth = 0.0f;

    public float distance = 20.0f;
    public float minDistance = 15.0f;
    public float maxDistance = 45.0f;

    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 xDest = new Vector3();
    public Vector3 xAbovePos = new Vector3();
    Vector3 finalCamPos = new Vector3();

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

        Vector3[] checkPoints = new Vector3[5];
        checkPoints[0] = xWanted;
        checkPoints[1] = Vector3.Lerp(xWanted, xAbovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(xWanted, xAbovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(xWanted, xAbovePos, 0.75f);

        checkPoints[4] = xAbovePos = viewTarget.transform.position + viewTarget.transform.up * distance;

        for (int i = 0; i < checkPoints.Length; ++i)
        { 
            if (checkRayCast(checkPoints[i]))
                break;
        }
        Vector3 acceleration = new Vector3(0, 0, 0);
        acceleration += -velocity * 2.0f * Mathf.Sqrt(distSpringConstant) / mass;
        acceleration += -(this.transform.position - finalCamPos) * distSpringConstant / mass;

        this.transform.position += Time.deltaTime * velocity;
        this.transform.LookAt(viewTarget.transform.position);

        velocity = velocity + Time.deltaTime * acceleration;

        velocity = Vector3.ClampMagnitude(velocity, 20);
    }

    bool checkRayCast (Vector3 checkPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkPos, viewTarget.transform.position - checkPos, out hit))
        {
            Debug.DrawRay(checkPos, viewTarget.transform.position - checkPos, Color.red);
            if (hit.transform != viewTarget.transform)
                return false;
        }

        finalCamPos = checkPos;
        return true;
    }
}