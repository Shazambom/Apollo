using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementControler : MonoBehaviour {

	public float MAX_SPEED;

	private Rigidbody2D body;

	private Animator animator;

	private bool facingRight;

	//Jump variables
	private bool grounded = false;

	private float groundRadius = 0.6f;

	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		
	}

	void Update() {
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			animator.SetBool ("onGround", grounded);
			body.AddForce(new Vector2(0, jumpHeight));

		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//check if we are grounded
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
		animator.SetBool ("onGround", grounded);

		animator.SetFloat ("verticalSpeed", body.velocity.y);


		float move = Input.GetAxis("Horizontal");
		animator.SetFloat ("speed", Mathf.Abs (move));

		body.velocity = new Vector2 (move * MAX_SPEED, body.velocity.y);

		if (move < 0 && !facingRight) {
			flip ();

		} else if (move > 0 && facingRight) {
			flip ();
		}
		
	}

	void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

	}
}
