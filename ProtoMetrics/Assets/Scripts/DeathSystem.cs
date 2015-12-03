using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour {

    public bool dead;
    public UISystem uiSys;
    public HeatSystem heat;

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

    public void Kill(string cause,Vector3 position)
    {
        dead = true;
        uiSys.Kill(cause, position);

        UnityEngine.Analytics.Analytics.CustomEvent(
            cause,
    new Dictionary < string, object >
    {
        { "Heat", heat.heat },
        {"Minutes",uiSys.minutes },
        {"Seconds",uiSys.seconds },
        {"MS", uiSys.milliSecs },
        {"X", position.x },
        {"Y", position.y },
        {"Z", position.z }
    });
        Debug.Log(cause);

    }

}
