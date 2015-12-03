using UnityEngine;
using System.Collections;

public class FinishFlag : MonoBehaviour {

    public DeathSystem death;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !death.dead)
            death.Kill("Geschafft",other.transform.position);
    }
}
