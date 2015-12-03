using UnityEngine;
using System.Collections;
using System;

public class UISystem : MonoBehaviour {

    public UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Text heatText;

    public GameObject deathUI;
    public UnityEngine.UI.Text deathMessage;

    // Use this for initialization
    void Start () {
        deathUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(!deathUI.active)
            timeText.text = SecondsToTime();
	}

    string SecondsToTime()
    {

        //Hacky Methode, da ich -Zeit habe, um die Steuerung am anfang zu freezen



        float rawTime = Time.timeSinceLevelLoad - 7.0f;

        int secs = (int)Mathf.Abs(rawTime);

        if (rawTime < 0)
            rawTime += secs;
        else
            rawTime -= secs;

        rawTime *= 100.0f;
        int milliSecs = (int)Mathf.Abs(rawTime);

        int minutes = secs / 60;
        secs -= minutes * 60;

        int hours = minutes / 60;
        minutes -= hours * 60;

        string result ="Time: ";

        if (hours > 0)
            result += hours.ToString() + ":";
        else
            result += "   ";

        if (minutes > 0)
        {
            if (minutes < 10)
                result += "0";
            result += minutes.ToString()+":";
        }
        else
            result += "   ";

        if (secs < 10)
            result += "0";
        result += secs+".";


        if(milliSecs < 10)
        {
            result += "0";
        }
        result += milliSecs.ToString();

        if(rawTime < 0.0f)
        {
            result = "Time:       -"+secs.ToString()+"."+milliSecs.ToString();
        }

        return result;
    }

    public void Respawn()
    {
        deathUI.SetActive(false);
    }

    public void Kill(string cause)
    {
        deathUI.SetActive(true);
        deathMessage.text = cause;
    }
}
