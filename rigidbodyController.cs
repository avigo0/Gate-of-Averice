using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodyController : MonoBehaviour {
    Rigidbody2D rb; // physics pointer
    //public string vertAxis = "Vertical";
    public string horizAxis = "Horizontal";
    //private float time = Time.fixedDeltaTime;
    public float movementSpeed = 100f;

    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>(); // now holds a refrence to the physics component of object

	}
	
	// Update is called once per frame
	//void Update () {
        
	//}

    void FixedUpdate() {
        float moveHoriz = Input.GetAxisRaw(horizAxis); // grabs the horizontal axis .. (either -1 or 1)
        Move(moveHoriz); //calls my move function
    }

    void Move(float input)
    {
        rb.AddForce(Vector2.right * movementSpeed * input, ForceMode2D.Force); // move function simply adds physics force of size movementspeed to vector<1,0> 
    }
}
