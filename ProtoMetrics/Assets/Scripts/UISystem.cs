using UnityEngine;
using System.Collections;
using System;

public class UISystem : MonoBehaviour {

    public UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Text heatText;

    public GameObject deathUI;
    public UnityEngine.UI.Text deathMessage;

    public int seconds;
    public int minutes;
    public int hours;
    public float milliSecs;
    public float rawTime;

    public Tracker tracker;
    // Use this for initialization
    void Start () {
        deathUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!deathUI.active)
            makeTime();

        timeText.text = TimeToString();
	}

    public string TimeToString()
    {


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

        if (seconds < 10)
            result += "0";
        result += seconds + ".";


        if(milliSecs < 10)
        {
            result += "0";
        }
        result += milliSecs.ToString();

        if(rawTime < 0.0f)
        {
            result = "Time:       -"+ seconds.ToString()+"."+milliSecs.ToString();
        }

        return result;
    }

    void makeTime()
    {

        //Hacky Methode, da ich -Zeit habe, um die Steuerung am anfang zu freezen
        rawTime = Time.timeSinceLevelLoad - 7.0f;

        seconds = (int)Mathf.Abs(rawTime);

        if (rawTime < 0)
            rawTime += seconds;
        else
            rawTime -= seconds;

        rawTime *= 100.0f;
        milliSecs = (int)Mathf.Abs(rawTime);

        minutes = seconds / 60;
        seconds -= minutes * 60;

        hours = minutes / 60;
        minutes -= hours * 60;
    }

    public void Respawn()
    {
        deathUI.SetActive(false);
    }

    public void Kill(string cause, Vector3 position)
    {
        tracker.LogPosition(position);
        deathUI.SetActive(true);
        deathMessage.text = cause;
    }
}
