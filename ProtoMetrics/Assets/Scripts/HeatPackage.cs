using UnityEngine;
using System.Collections.Generic;

public class HeatPackage : MonoBehaviour {

	// Use this for initialization
    public GameObject player;
    public float heatPerSecond;
    HeatSystem playerHeat;
    float currentDistance;

    private SphereCollider sphere;
    private AudioSource source;
    private float range;

    float incomingHeat;
    float incomingTime;

	void Start () {
        playerHeat = (HeatSystem)player.GetComponent(typeof(HeatSystem));
        sphere = (SphereCollider)GetComponent(typeof(SphereCollider));
        source = (AudioSource)GetComponent(typeof(AudioSource));
        range = sphere.radius;

        incomingHeat = incomingTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 spherePos = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        currentDistance = Vector2.Distance(spherePos,playerPos);
	}

    void OnTriggerStay(Collider other)
    {
        float quoteInRange = Mathf.Clamp01(1 - (currentDistance / range));
        if (other.gameObject == player)
        {
            playerHeat.heat = Mathf.Clamp(playerHeat.heat + heatPerSecond * Time.deltaTime, 0, playerHeat.maxHeat);
            if (playerHeat.heat == playerHeat.maxHeat)
                source.mute = true;
            else
                source.mute = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHeat.warming = true;
            incomingHeat = playerHeat.heat;
            source.mute = false;
            incomingTime = Time.timeSinceLevelLoad-7.0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHeat.warming = false;
            source.mute = true;


            float stayedTime = (Time.timeSinceLevelLoad - 7.0f) - incomingTime;
             UnityEngine.Analytics.Analytics.CustomEvent(
             "FeuerEvent",
     new Dictionary<string, object>
     {
         { "Hitze_Start", incomingHeat },
         { "Hitze_Ende", playerHeat.heat },
         { "Aufgehaltene Zeit", stayedTime },
         {"X", other.transform.position.x },
         {"Y", other.transform.position.y },
         {"Z", other.transform.position.z }
     });
        }
        

    }   
}
