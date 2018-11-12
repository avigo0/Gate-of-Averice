using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterDash : MonoBehaviour {
    private Rigidbody2D rb;
    public float dashSpeedG; // ground
    public float dashSpeedA; // air
    [SerializeField]
    [Range(-1f, 0f)]
    private float dashTime;
    [Range(0f, 20f)]
    public float distance;
    [Range(0f, 20f)]
    public float sideDistance;
    public float startDashTime;
    private float direction;
    bool isDashing;
    float tempGrav; 
    //bool isInAIr;
    // Use this for initialization
    void Start () {
        isDashing = false;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkDashRequest();
	}

    bool isInAir()
    {
        Debug.Log("checking if in air");
        RaycastHit2D resultDOWN = Physics2D.Raycast(rb.position, Vector2.down);
        RaycastHit2D resultRIGHTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.right));
        RaycastHit2D resultLEFTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.left));

        Debug.Log((resultDOWN.distance > distance) || ((resultRIGHTDiag.distance > sideDistance)) || ((resultLEFTDiag.distance > sideDistance)));
        return ((resultDOWN.distance > distance) || ((resultRIGHTDiag.distance > sideDistance)) || ((resultLEFTDiag.distance > sideDistance)));

        //if ((resultDOWN.distance > distance) || ((resultRIGHTDiag.distance > sideDistance)) || ((resultLEFTDiag.distance > sideDistance)))
        //    inAir = true;
        //else
        //    inAir = false;
        // return inAir;

    }

    void Dash() //Direction?  if button left then direction is negative one ect.
    {
        Debug.Log("Are we dashing: " + isDashing);
        if (!isDashing) //&&& prepared to dash
        {
            tempGrav = rb.gravityScale;
            if (isInAir()) //and dashing? 
            {
                //rb.gravityScale = 2f;
                rb.AddForce(Vector2.right * dashSpeedA * direction + new Vector2(1f, 5f), ForceMode2D.Impulse);
                Debug.Log("AIR DASHING HOE!!!!!!!");
            }
            else
            {
                //rb.gravityScale = 1f;
                rb.AddForce(Vector2.right * dashSpeedG * direction , ForceMode2D.Impulse);

                //rb.transform.Translate(Vector2.right * dashSpeed * direction);// Teleport LOL


                Debug.Log("DASHING BITCH"); // Dash
            }
        }else if (dashTime <= 0) { // we must be done dashing
            direction = 0; // we want to stop dashing here
            isDashing = false;
            dashTime = startDashTime;
            //rb.gravityScale = 1f;
            Debug.Log("WE need to stop dashing");
        }
        else {
            isDashing = true;
            while (isInAir()) {
                Debug.Log("I AM IN THE AIRRRRRR");
                if (isDashing)
                    rb.gravityScale /= 250;
                else
                    rb.gravityScale = (tempGrav/4);
            }
            Debug.Log("we must be dashing");
            dashTime -= Time.fixedDeltaTime; //MAYBe

        }

    }
    //+++++=============DASH MECH=============++\\\ (MINORBUGS DOWN HERE)
    public float timeInterval;
    private float timer;
    private bool dashPrepared = false;
    private bool dashingRight;
    private bool dashingLeft;
    void checkDashRequest()
    {
        if (dashPrepared)
        {
            timer += Time.fixedDeltaTime;
            if (timer > timeInterval) { 
                dashPrepared = false;
                timer = 0;
            }
        }

        if(dashPrepared && dashingRight && rightButtonDown()) // might need to make a button for right
        {
            direction = 1;
            Dash();
            dashingRight = false;
            direction = 0;
            dashPrepared = false;
            timer = 0;
        } else if(dashPrepared && dashingLeft && leftButtonDown()){
            direction = -1;
            Dash();
            direction = 0;
            dashPrepared = false;
            dashingLeft = false;
            timer = 0;

        }else if (rightButtonDown()) // if one button is pressed then prepare for dash
        {
            Debug.Log(1);
            dashPrepared = true;
            dashingRight = true;
            dashingLeft = false;
            timer = 0;
        }else if (leftButtonDown())
        {
            Debug.Log(2);
            dashPrepared = true;
            dashingLeft = true;
            dashingRight = false;
            timer = 0;
        }

    }

    bool rightButtonDown()
    {
        return (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0);
    }

    bool leftButtonDown()
    {
        return (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0);
    }
}
