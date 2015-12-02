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
        death.Kill("Du hast das Ziel erreicht!");
    }
}
