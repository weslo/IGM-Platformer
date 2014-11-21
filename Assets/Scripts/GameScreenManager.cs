using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScreenManager : MonoBehaviour {

	public GameScreen[] screens;

	// Use this for initialization
	void Start () {
		LoadScreen (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Loads the screen at the given index
	public void LoadScreen (int index) {
		if (index < 0 || index >= screens.Length) return;

		DestroyChildren ();
		GameObject go = (GameObject)Instantiate (screens[index].gameObject, transform.position, transform.rotation);
		GameScreen screen = go.GetComponent<GameScreen> ();
		screen.transform.parent = transform;
		screen.controller = this;
	}

	// Helper method for clearing children
	private void DestroyChildren () {
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}
}
