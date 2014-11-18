using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float forceOnHit = 1000.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2);
	}

	// Called when this collides with another 2D collider
	void OnTriggerEnter2D(Collider2D col)
	{
		// If collision is with an enemy...
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt (); // damage the enemy
			Vector2 projectileDirection = rigidbody2D.velocity;
			projectileDirection.Normalize();
			Vector2 forceDirection = new Vector2(projectileDirection.x, 0.1f);
			forceDirection.Normalize();
			col.gameObject.rigidbody2D.velocity = Vector2.zero;
			col.gameObject.rigidbody2D.angularVelocity = 0.0f;
			col.gameObject.rigidbody2D.AddForce (forceDirection * forceOnHit); // apply force diagonally
			Destroy(gameObject); // destroy the bullet
		}
		else if (col.tag == "Obstacle") {
			Destroy(gameObject);
		}
	}
}
