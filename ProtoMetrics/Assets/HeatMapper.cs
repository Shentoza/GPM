using UnityEngine;
using System.Collections;

public class HeatMapper : MonoBehaviour {


    public TextAsset movement;
    public TextAsset deaths;
    bool done = false;
    public Camera usedCam;
    Texture2D movementTex, deathTex;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!done)
        {
            done = true;
            Vector3[] deathVec = StringUtility.Vector3ArrayWithFile(deaths);


            Vector3[] movementVec = StringUtility.Vector3ArrayWithFile(movement);
            movementTex = Heatmap.CreateHeatmap(movementVec, usedCam, 7);
            deathTex = Heatmap.CreateHeatmap(deathVec, usedCam, 13);
        }

        if(done && Input.GetKeyDown("1"))
        {
            Heatmap.CreateRenderPlane(movementTex, usedCam);
            GameObject plane = GameObject.Find("Heatmap Render Plane");
            MeshRenderer meshr = (MeshRenderer)plane.GetComponent(typeof(MeshRenderer));

            meshr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            Heatmap.Screenshot("post_movement.png", usedCam);
        }
        if(done && Input.GetKeyDown("2"))
        {
            Heatmap.CreateRenderPlane(deathTex, usedCam);
            GameObject plane = GameObject.Find("Heatmap Render Plane");
            MeshRenderer meshr = (MeshRenderer)plane.GetComponent(typeof(MeshRenderer));

            meshr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            Heatmap.Screenshot("post_death.png", usedCam);
        }
    }
}
