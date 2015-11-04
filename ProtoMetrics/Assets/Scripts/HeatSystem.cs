using UnityEngine;
using System.Collections;

public class HeatSystem : MonoBehaviour {

	// Use this for initialization

    Movement move;

    public float maxHeat;
    public float heat;
    public float decayPerSecond;
    public bool warming;
    Renderer rend;

    public Material colorHot;
    public Material colorCold;

    float oldQuote;

	void Start () {
        move = (Movement)GetComponent(typeof(Movement));
        rend = (Renderer)GetComponent(typeof(Renderer));
        warming = false;
        oldQuote = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float quote = heat / maxHeat;
        if (Mathf.Abs(oldQuote - quote) > .1 || warming)
        {
            Debug.Log("UPDATE: " + quote);
            move.speed = move.maxSpeed * quote;
            move.angularSpeed = move.maxAngularSpeed * quote;
            rend.material.Lerp(colorCold, colorHot, quote);
            oldQuote = quote;
        }
        if(!warming)
            heat = Mathf.Clamp(heat - decayPerSecond * Time.deltaTime, 0, maxHeat);
	}
}
