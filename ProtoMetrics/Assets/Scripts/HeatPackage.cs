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

    public float maxHeat;
    public float heat;

    float incomingHeat;
    float incomingTime;

    public bool active;

    public GameObject wholeSpot;
    public UI_FireBar barUI;

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
        if (heat == 0)
        {
            playerHeat.warming = false;
            Destroy(wholeSpot);
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            active = true;
            float calculated = heatPerSecond * Time.deltaTime;

            //Hitze die abgezogen werden kann geclamped auf 0
            float transferredHeat;
            if (heat - calculated <= 0)
            {
                transferredHeat = heat;
                heat = 0.0f;
            }
            else
            {
                transferredHeat = calculated;
                heat -= transferredHeat;
            }

            float receivedHeat;
            if(playerHeat.heat + transferredHeat >= playerHeat.maxHeat)
            {
                receivedHeat = playerHeat.maxHeat - playerHeat.heat;
                heat += transferredHeat - receivedHeat;
                playerHeat.heat = playerHeat.maxHeat;
            }
            else
            {
                receivedHeat = transferredHeat;
                playerHeat.heat += transferredHeat;
            }

            source.mute = heat == 0.0f || playerHeat.heat == playerHeat.maxHeat;
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
            active = false;
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
