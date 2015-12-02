using UnityEngine;
using System.Collections;

public class DeathSystem : MonoBehaviour {

    bool dead;
    public UISystem uiSys;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Respawn()
    {
        Debug.Log("CALLED");
        dead = false;
        Application.LoadLevel(Application.loadedLevel);
        uiSys.Respawn();
    }

    public void Kill(string cause)
    {
        dead = true;
        uiSys.Kill(cause);
    }

}
