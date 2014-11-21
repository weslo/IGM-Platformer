using UnityEngine;
using System.Collections;

public class GameScreen : MonoBehaviour {

	[HideInInspector]
	public GameScreenManager controller;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Loads another screen via the controller
	public void LoadOtherScreen (int index) {
		controller.LoadScreen (index);
	}
}
