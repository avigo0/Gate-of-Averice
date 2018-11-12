/*
 * This is a jumping script for 2d
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour {
    private Rigidbody2D rb;
    [Range(0, 50)] // makes a slider in the inspector
    public float jumpVelocity; // will be used as a vector multiple
    private bool jumpRequest;
    bool isGrounded;
    [Range(-1f,1f)]
    public float distance;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>(); // used to grab refrence to object physical body
	}
	
	// Update is called once per frame
	void Update () {
       
        RaycastHit2D result = Physics2D.Raycast(rb.position, Vector2.down);
        
        if (result.collider.tag == "Floor" && (result.distance < .28)) //.28
        {
            isGrounded = true;
           

        }
        //
        if (Input.GetButtonDown("Jump") && (isGrounded)) // this is to check that the jump button was pressed
        {
            jumpRequest = true; //sets flag to true
            Debug.Log("############## im jumping ##############"); //debug
        }
	}

    void FixedUpdate()
    {
        if (jumpRequest) // if the button is down then the character is allowed to jumped
        {
            Jump(); // calls jump function
            jumpRequest = false; // subsequently sets jump to false
            isGrounded = false;
        }
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse); // simply adds impulse force to y cord of vector
    }
}
