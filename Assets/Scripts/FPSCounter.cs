﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
	Text fpsText;
	float deltaTime = 0.0f;

	void Start() {
		fpsText = GetComponent<Text> ();
	}

	void Update ()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
		
	void OnGUI ()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		fpsText.text =  string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
	}

}
