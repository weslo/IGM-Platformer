using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target; // reference to the game object the camera will follow
	public float speed = 1.0f; // speed constant

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z); // apply depth
	
		}
	}
}
