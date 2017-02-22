﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementControler : MonoBehaviour {

	public float MAX_SPEED;

	private Rigidbody2D body;

	private Animator animator;

	private bool facingRight;

	//Jump variables
	private bool grounded = false;

	private float groundRadius = 0.2f;

    private bool doubleJump = true;


	//Attack variables

	private bool attacking = false;

	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

    //Wall sliding variables
    public LayerMask wallLayer;
    public Transform frontWallCheck;
    public Transform behindWallCheck;
    public float wallFallRate;
    private bool onWallFront;
    private bool onWallBehind;
    private float wallRadius = 0.01f;

    // Use this for initialization
    void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		
	}

	void Update() {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                grounded = false;
                animator.SetBool("onGround", grounded);
                body.AddForce(new Vector2(0, jumpHeight));
                doubleJump = true;
            }
            else if (doubleJump)
            {
                doubleJump = false;
                animator.SetBool("onGround", false);
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(new Vector2(0, jumpHeight));
            }
        }


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //check if we are grounded

        

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        animator.SetBool ("onGround", grounded);

        onWallFront = Physics2D.OverlapCircle(frontWallCheck.position, wallRadius, wallLayer);
        onWallBehind = Physics2D.OverlapCircle(behindWallCheck.position, wallRadius, wallLayer);
        bool onWall = (onWallFront || onWallBehind) && !grounded;
        animator.SetBool("onWall", onWall);


        if (grounded)
        {
            doubleJump = true;
        } 
        if (onWall)
        {
            if (onWall && body.velocity.y < 0)
            {
                body.velocity = new Vector2(body.velocity.x, -wallFallRate);
            }

            if (onWallFront)
            {
                doubleJump = true;
                flip();

            }
        }



        animator.SetFloat ("verticalSpeed", body.velocity.y);



		float move = Input.GetAxis("Horizontal");
		animator.SetFloat ("speed", Mathf.Abs (move));
        if ((move < 0 && !facingRight && onWall) || (move > 0 && facingRight && onWall))
        {
            move = 0;
        } 

		body.velocity = new Vector2 (move * MAX_SPEED, body.velocity.y);

		if (move < 0 && !facingRight) {
			flip ();

		} else if (move > 0 && facingRight) {
			flip ();
		}

		if(Input.GetKey(KeyCode.E)) {
			if (!attacking) {
				attacking = true;
				animator.SetBool ("attack", attacking);
			}

		} 
		else {
			attacking = false;
			animator.SetBool("attack", attacking);
		}


		
	}

	void flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
    }
}
