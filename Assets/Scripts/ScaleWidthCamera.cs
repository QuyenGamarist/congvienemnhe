using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWidthCamera : MonoBehaviour {
	public int targetWidth = 1920;
	public float pixelsToUnits = 100;
	// Use this for initialization
	void Awake () {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
        Camera.main.orthographicSize = height / pixelsToUnits / 2;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
