using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public int pickupsRequired = 1;
	public bool open = false;
	public Sprite closed;
	public Sprite opened;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = closed;
	}
	
	// Update is called once per frame
	void Update () {
		switch (open) {
		case false:
			GetComponent<SpriteRenderer> ().sprite = closed;
			break;
		case true:
			GetComponent<SpriteRenderer> ().sprite = opened;
			break;
				}
	}

	// Called when this collides with another 2D collider
	void OnTriggerEnter2D(Collider2D c)
	{
		// If collision is with the player...
		if (c.tag == "Player") {
			PlayerController player = c.gameObject.GetComponent<PlayerController>();
			if (player.pickups >= pickupsRequired)
			{
				open = true;
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
