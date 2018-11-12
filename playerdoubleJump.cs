using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdoubleJump : MonoBehaviour {
    private Rigidbody2D rb;
    [Range(0, 50)] // makes a slider in the inspector
    public float jumpVelocity, jumpMultiplier; // will be used as a vector multiple
    private bool jumpRequest;
    bool isGrounded, isinAir;
    [Range(-1f, 1f)]
    public float distance;
    [Range(-1f, 1f)]
    public float sideDistance;
    public float jumpCounter;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>(); // used to grab refrence to object physical body
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround())  {

            jumpCounter = 0; // used for double jump
        }
        //
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCounter < 2)) { // this is to check that the jump button was pressed and that player is grounded. (also allows one extra jump)

            jumpRequest = true; //sets flag to true
        }
    }

    void FixedUpdate() {

        if (jumpRequest) // if the button is down then the character is allowed to jumped
        {
            Jump(); // calls jump function
            jumpRequest = false; // subsequently sets jump to false
            //isGrounded = false; //resets grounded bool so the player cannot jump forever
        }
    }
    void Jump() {

        if (jumpCounter == 1) // if the player has an extra jump
        {
            rb.AddForce(Vector2.up * jumpVelocity * jumpMultiplier, ForceMode2D.Impulse); // apply a force multiplier
        }
        else
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse); // simply adds impulse force to y cord of vector (regular jump)

        jumpCounter++;
    }

    bool onGround() {
        RaycastHit2D resultDOWN = Physics2D.Raycast(rb.position, Vector2.down);
        RaycastHit2D resultRIGHTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.right));
        RaycastHit2D resultLEFTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.left));
        if (resultDOWN.collider.tag == "Floor" && ((resultDOWN.distance < distance) ||
           ((resultRIGHTDiag.distance > 0) && (resultRIGHTDiag.distance < sideDistance)) ||
            ((resultLEFTDiag.distance > 0) && (resultLEFTDiag.distance < sideDistance))))  {

            isGrounded = true;

        } else
             isGrounded = false;
         /* //###########IF THIS IS EASIER TO READ PLEASE USE THIS:###########//
        //if (resultDOWN.collider.tag == "Floor" && (resultDOWN.distance < distance))
        //{
        //    Debug.Log("down ~ DISTANCE IS!: " + resultDOWN.distance);
        //    isGrounded = true;
        //}
        //else if (resultDOWN.collider.tag == "Floor" && ((resultRIGHTDiag.distance> 0) && (resultRIGHTDiag.distance < sideDistance)))
        //{
        //    Debug.Log("RIGHT (***) ~ DISTANCE IS!: " + resultRIGHTDiag.distance);
        //    isGrounded = true;
        //}else if (resultDOWN.collider.tag == "Floor" && ((resultLEFTDiag.distance > 0) && (resultLEFTDiag.distance < sideDistance)))
        //{
        //    Debug.Log("LEFT [$$$$] ~ DISTANCE IS!: " + resultLEFTDiag.distance);
        //    isGrounded = true;
        }*/
        return isGrounded;
    }
}
