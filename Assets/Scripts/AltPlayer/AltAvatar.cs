using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltAvatar : AltActor
{
    public AltPuck puck;

    float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public override void ApplyVelocity()
    {
        Vector2 velocity = Vector2.zero;
        velocity.y += (Input.GetKey(KeyCode.W) ? 1f : 0f) * speed * Time.deltaTime;
        velocity.y -= (Input.GetKey(KeyCode.S) ? 1f : 0f) * speed * Time.deltaTime;
        velocity.x += (Input.GetKey(KeyCode.D) ? 1f : 0f) * speed * Time.deltaTime;
        velocity.x -= (Input.GetKey(KeyCode.A) ? 1f : 0f) * speed * Time.deltaTime;
        transform.Translate(velocity);

        puck.playerMoving = (velocity != Vector2.zero);
    }
}
