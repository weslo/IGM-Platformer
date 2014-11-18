using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {
	private Vector3 prevCameraPos; // camera position last frame

	// Use this for initialization
	void Start () {
		prevCameraPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main != null)
		{
			Vector3 deltaCameraPos = Camera.main.transform.position - prevCameraPos;
			transform.position = new Vector3(transform.position.x - deltaCameraPos.x / transform.position.z,
			                                 transform.position.y - deltaCameraPos.y / transform.position.z,
			                                 transform.position.z);
			prevCameraPos = Camera.main.transform.position;
		}
	}
}
