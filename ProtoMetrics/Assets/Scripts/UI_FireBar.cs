using UnityEngine;
using System.Collections;

public class UI_FireBar : MonoBehaviour {

    RectTransform canvas;
    Camera cam;
    float maxWidth;

    public HeatPackage hotSpot;

	// Use this for initialization
	void Start () {
        canvas = (RectTransform)this.GetComponent(typeof(RectTransform));
        cam = Camera.main;
        maxWidth = canvas.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
        canvas.localRotation = cam.transform.rotation;
        canvas.sizeDelta = new Vector2((hotSpot.heat / hotSpot.maxHeat) * maxWidth, 0.25f);
    }
}
