/*
 * Wesley Rockholz
 * PlayerController Script
 * January - March 2014
*/

// A lot of this code is rewritten based on the Unity2D platformer example project.

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject sprite; // reference to animated sprite wrapped in player parent
	private Animator animator; // animation controller

	public int hp = 3; // hit points

	public float jumpForce = 500.0f; // force applied vertically when the player jumps.
	public float moveForce = 350.0f; // movement speed
	public float maxSpeed = 5.0f; // maximum movement speed
	public Rigidbody2D projectile; // reference to projectile prefab
	public float projectileSpeed = 25.0f; // projectile speed

	public int pickups = 0; // number of pickups the player is holding

	private bool facingRight = true; // flagged true if the player is facing right
	private Transform groundCheck; // point that checks for ground collisions
	private Transform meleeCheck; // point that checks for melee range
	public bool grounded; // flagged true if the player is on the ground (able to jump)
	private bool jump; // flagged true if the player will jump next fixed fram
	
	// Use this for initialization
	void Start () {
		groundCheck = transform.Find ("groundCheck"); // access the groundCheck point
		meleeCheck = transform.Find ("meleeCheck"); // access the meleeCheck point
		animator = sprite.GetComponent<Animator> (); // reference to sprite animator
	}
	
	// Update is called once per frame
	void Update () {
		bool prevGrounded = grounded; // cache grounded flag from last frame
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); // check if the player is grounded
		if (!prevGrounded && grounded) {
			animator.Play ("player_jump_end"); // end jumping animation
		}

		// if the player is grounded and presses the jump button...
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true; // flag them to jump next frame
			animator.SetBool ("Walking", false); // end walking animation
			animator.Play("player_jump_start"); // begin jumping animation
		}

		// if the player presses the fire key
		if (Input.GetButtonDown ("Fire1"))
		{
			// create a new instance of a projectile and fire it
			Rigidbody2D projectileInstance = (Rigidbody2D) Instantiate (projectile, transform.position, Quaternion.identity);
			if (!facingRight) {
				Vector3 ls = projectileInstance.transform.localScale;
				ls.x *= -1;
				projectileInstance.transform.localScale = ls;
			}
			projectileInstance.velocity = new Vector2(facingRight ? projectileSpeed : projectileSpeed * -1, 0);

			animator.Play ("player_ranged"); // play ranged attack animation
		}

		// if the player presses the melee attack key
		if (Input.GetButtonDown ("Fire2"))
		{
			animator.Play ("player_melee"); // play ranged attack animation

			// Create an array of all the colliders in front of the player after the swing.
			Collider2D[] hits = Physics2D.OverlapPointAll(meleeCheck.position);
			
			// Check each of the colliders.
			foreach(Collider2D c in hits)
			{
				// If any of the colliders is an Enemy...
				if(c.tag == "Enemy")
				{
					Enemy enemy = c.gameObject.GetComponent<Enemy>();
					enemy.Hurt(); // hurt the enemy
					Vector2 forceDirection = new Vector2(facingRight ? 0.5f : -0.5f, 0.5f); // diagonal directional vector
					enemy.rigidbody2D.velocity = Vector2.zero;
					enemy.rigidbody2D.AddForce (forceDirection * 500.0f); // apply force diagonally
				}
			}
		}
	}

	// FixedUpdate is called at fixed time intervals
	void FixedUpdate() {

		float h = Input.GetAxis ("Horizontal"); // cache horizontal input

		//if (grounded) {

			// idle if not moving
			if (h == 0) {
				animator.SetBool ("Walking", false);
			}
			// walk if moving
			else {
				animator.SetBool ("Walking", true);
			}

			// if player is changing direction or hasn't reached speed cap yet...
			if (h * rigidbody2D.velocity.x < maxSpeed) {
					rigidbody2D.AddForce (Vector2.right * h * moveForce); // apply horizontal force
			}

			// if player's horizontal speed is greater than the speed cap...
			if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
				// enforce the speed cap
				rigidbody2D.velocity = new Vector2(Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
			}

			// check if player is switching directions...
			if (h > 0 && !facingRight) {
				Flip ();
			}
			else if (h < 0 && facingRight) {
				Flip ();
			}
		//}

		// if the player is flagged to jump this frame...
		if (jump) {
			rigidbody2D.AddForce(Vector2.up * jumpForce); // apply upwards force
			jump = false; // reset jump flag
		}
	}

	// Flip the direction the player is facing
	void Flip () {
		facingRight = !facingRight; // flip flag

		Vector3 ls = transform.localScale; // cache current scale
		ls.x *= -1; // flip horizontally
		transform.localScale = ls; // maintain scale
	}

	// Hurt the player.
	public void Hurt () {
		animator.Play("player_hurt");
		hp--; // decrement health

		if (hp == 0) {
			Destroy(gameObject); // check for death condition
			GameScreen screen = transform.parent.GetComponent<GameScreen>();
			screen.LoadOtherScreen(2);
		}
		
		//animator.Play ("player_hurt");
	}
}
