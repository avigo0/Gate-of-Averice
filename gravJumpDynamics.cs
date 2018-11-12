/*
 * this should mimic the jumping style of mega man  by adding falling mechanics
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravJumpDynamics : MonoBehaviour {
    public float fallMutiplier = 3f; // fall scaler
    public float lowJmpMultiplier = 2f; // low jump scaler
    [Range(-10f, 0f)]
    public float velCheck;

    Rigidbody2D rb;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>(); // refrence to objects body
	}

	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb.velocity.y < velCheck) // if the velocity of the object is decreasing apply fall multiplier to gravity scale (ONLY WORKS FOR 2d)
        {
            rb.gravityScale = fallMutiplier; // basicaly makes the object fall faster -> looks more realistic
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // this will check if the button is not being held down seperating the jump into two types
        {
            rb.gravityScale = lowJmpMultiplier; // until the velocity is <=0 gravity scale is now set to low multiplier 
                                               // this will apply the low jump scaler 
        }
        else rb.gravityScale = 1f;
	}
}
