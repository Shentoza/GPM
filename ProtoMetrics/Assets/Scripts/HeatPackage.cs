using UnityEngine;
using System.Collections;

public class HeatPackage : MonoBehaviour {

	// Use this for initialization
    public GameObject player;
    public float heatPerSecond;
    HeatSystem playerHeat;
    float currentDistance;

    private SphereCollider sphere;
    private AudioSource source;
    private float range;

	void Start () {
        playerHeat = (HeatSystem)player.GetComponent(typeof(HeatSystem));
        sphere = (SphereCollider)GetComponent(typeof(SphereCollider));
        source = (AudioSource)GetComponent(typeof(AudioSource));
        range = sphere.radius;
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHeat.warming = true;
            source.mute = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHeat.warming = false;
            source.mute = true;
        }
    }
}
