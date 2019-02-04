using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    //this code is totally unused

    private Rigidbody2D rigidBody;
    float JumpSpeed = 8.5f;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    public PlayerControll pControl;

    // Use this for initialization
    void Start () {
		rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && (pControl.jumpUp == false) && isTouchingGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpSpeed);
        }
    }

    // Update is called once per frame
    void Update () {
        //check to see if player is touching the ground
        isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);
       
	}
}
