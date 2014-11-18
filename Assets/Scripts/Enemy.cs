using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject sprite; // reference to animated sprite wrapped in player parent
	private Animator animator; // animation controller

	private Transform frontCheck; // reference to front check transform

	public int hp = 2; // hit points
	public float maxSpeed = 2.0f; // max speed
	public float moveForce = 50.0f; // movement force
	public int damage = 1; // damage dealt on hit

	public float attackCooldown = 3;
	public float attackTimer = 0;

	// Use this for initialization
	void Start () {
		frontCheck = transform.Find ("frontCheck"); // access the frontCheck point
		animator = sprite.GetComponent<Animator> (); // reference to sprite animator
	}
	
	// FixedUpdate is called at fixed time intervals
	void FixedUpdate () {
		if (rigidbody2D.velocity.x < maxSpeed) {
			rigidbody2D.AddForce (Vector2.right * moveForce * transform.localScale.x);
		} 

		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
	}

	// Update is called once per frame
	void Update() {

		attackTimer -= Time.deltaTime;

		// check if colliding with obstacle
		if (Physics2D.Linecast (transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Ground"))) {
			Flip();
		}

		else if (Physics2D.Linecast (transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Player")))
		{
			if (attackTimer <= 0)
			{
				Collider2D c = Physics2D.Linecast (transform.position, frontCheck.position, 1 << LayerMask.NameToLayer("Player")).collider;
				PlayerController player = c.gameObject.GetComponent<PlayerController>();
				player.Hurt(); // hurt the player
				Vector2 forceDirection = new Vector2(Mathf.Sign (transform.localScale.x) * 0.5f, 0.5f); // diagonal directional vector
				player.rigidbody2D.velocity = Vector2.zero;
				player.rigidbody2D.AddForce (forceDirection * 1000.0f); // apply force diagonally
				attackTimer = attackCooldown;
			}
		}
	}

	/*
		// Called when this collides with another 2D collider
		void OnTriggerEnter2D(Collider2D c)
		{
		// If collision is with the player...
		if (c.tag == "Player") {
		}
	*/

	// Called when the enemy is damaged.
	public void Hurt()
	{
		hp--; // decrement hit points
		if (hp == 0) {
			Destroy(gameObject); // check for death condition
		}

		animator.Play ("enemy_hurt");
	}

	// Called to flip the enemy's direction.
	public void Flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
