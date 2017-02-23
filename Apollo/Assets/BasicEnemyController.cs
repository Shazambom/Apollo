using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {


    private Rigidbody2D body;
    private CircleCollider2D collider;
    public Rigidbody2D player;
    public float force;


    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        Vector2 heading = player.position - body.position;
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        distance *= 0.75f;
        Vector2 pos = new Vector2(body.position.x + (direction.x * (collider.radius + 0.1f)), body.position.y + +(direction.y * (collider.radius + 0.1f)));
        if(!Physics2D.Raycast(pos, direction, distance))
        {
            //body.AddForce(new Vector2(force * direction.x, force * direction.y));
            body.velocity = new Vector2(force * direction.x, force * direction.y);
        } 
    }
}
