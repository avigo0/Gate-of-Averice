using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDash : MonoBehaviour
{
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
    bool isInAIr;

    // public string horizAXIS = "Horizontal"; 

    // Use this for initialization
    void Start()
    {

        isDashing = false;
        rb = GetComponent<Rigidbody2D>();
        // dashTime = startDashTime;

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        RaycastHit2D resultDOWN = Physics2D.Raycast(rb.position, Vector2.down);
        RaycastHit2D resultRIGHTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.right));
        RaycastHit2D resultLEFTDiag = Physics2D.Raycast(rb.position, (Vector2.down + Vector2.left));
        direction = Input.GetAxisRaw("Horizontal");
        if (!isDashing)
        { // if not dashig then you can dash
            if (Input.GetButtonDown("Dash") && (direction != 0)) // if dash button down and player moving
            {

                if ((resultDOWN.distance > distance) || ((resultRIGHTDiag.distance > sideDistance)) || ((resultLEFTDiag.distance > sideDistance)))
                {
                    //rb.gravityScale = 2f;
                    rb.AddForce(Vector2.right * dashSpeedA * direction, ForceMode2D.Impulse);
                }
                else
                {
                    //rb.gravityScale = 1f;
                    rb.AddForce(Vector2.right * dashSpeedG * direction, ForceMode2D.Impulse);

                    //rb.transform.Translate(Vector2.right * dashSpeed * direction);// Teleport LOL


                    Debug.Log("DASHING BITCH"); // Dash
                }
            }
        }
        else if (dashTime <= 0)
        { // we must be done dashing
            direction = 0; // we want to stop dashing here
            isDashing = false;
            dashTime = startDashTime;
            rb.gravityScale = 1f;
            Debug.Log("WE need to stop dashing");
        }
        else
        {
            isDashing = true;
            //while (isDashing) {

            //    rb.gravityScale /= 100;
            //}
            Debug.Log("we must be dashing");
                
            dashTime -= Time.fixedDeltaTime; //MAYBe

        }



        //    direction = Input.GetAxisRaw("Horizontal");
        //    if (!isDashing)
        //    { // if not dashig then you can dash
        //        if (Input.GetButtonDown("Dash") && (direction != 0)) // if dash button down and player moving
        //        {
        //            Debug.Log("DASHING BITCH"); // Dash
        //        }
        //    }
        //    else if (dashTime <= 0)
        //    { // we must be done dashing
        //        direction = 0; // we want to stop dashing here
        //        isDashing = false;
        //        dashTime = startDashTime;
        //    }
        //    else
        //    {
        //        isDashing = true;
        //        dashTime -= Time.fixedDeltaTime; //MAYBe
        //        rb.AddForce(Vector2.right * dashSpeed * direction, ForceMode2D.Impulse);
        //    }



        //}
    }
 
       
 }



