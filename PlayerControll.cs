using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    // changeable values in the editor.
    public float acceleration = 0.005f;
    public float maxSpeed;
    public float startSpeed;
	public float maxLevelSpeed = 7f;

    // To see if everything is working fine.
    public bool bTopFloor = false;
    public bool bMiddleFloor = false;
    public bool bDownFloor = false;
    public bool jumpUp = false;
    public bool jumpDown = false;

    Vector2 downFloorPos;
    Vector2 middleFloorPos;
    Vector2 topFloorPos;
    Vector2 playerPos;
    //Vector2 placeToJump;

    GameObject downFloor;
    GameObject middleFloor;
    GameObject topFloor;

    private Rigidbody2D playerBody;

    // Just to see the current speed and velocity in the editor.
    public float currentSpeed = 1.0f;
    public float currentVelocity;

    // Use this for initialization
    void Start()
    {
        currentSpeed = startSpeed;

        playerBody = this.GetComponent<Rigidbody2D>();

        // Tags are attached to the colliders
        downFloor = GameObject.FindGameObjectWithTag("DownFloor");
        middleFloor = GameObject.FindGameObjectWithTag("MiddleFloor");
        topFloor = GameObject.FindGameObjectWithTag("TopFloor");

        downFloorPos = downFloor.transform.position;
        middleFloorPos = middleFloor.transform.position;
        topFloorPos = topFloor.transform.position;
    }

    //CORE AND MOST IMPORTANT PART OF THE CODE, REST IS LARGELY UNUSED
    private void FixedUpdate()
    {
        playerBody.AddForce(new Vector2(currentSpeed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // Assuming that this script will be attached to the player
        playerPos = this.transform.position;

        // Speeding up the current player's speed
        if (currentSpeed < maxSpeed)
        {
            currentSpeed = currentSpeed + acceleration * Time.deltaTime;
        }

        else
        {
            currentSpeed = maxSpeed;
        }

        if (currentSpeed < 1f)
        {
            currentSpeed = 1f;
        }

        // Checking what floor the player is on and switching off colliders of the higher floor
        if (playerPos.y >= downFloorPos.y && playerPos.y < middleFloorPos.y)
            bDownFloor = true;
        else
            bDownFloor = false;

        if (playerPos.y >= middleFloorPos.y && playerPos.y < topFloorPos.y)
            bMiddleFloor = true;
        else
            bMiddleFloor = false;

        if (playerPos.y >= topFloorPos.y)
            bTopFloor = true;
        else bTopFloor = false;

        //these will keep player's velocity between minimum and maximum constraints
        currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity.x;

        //VELOCITY CONSTRAINTS
        if (currentVelocity > maxLevelSpeed)
        {
            currentVelocity = 6.75f;
        }

        if (currentVelocity < 0.15f)
        {
            currentVelocity = 0.15f;
        }

        //IF STATEMENTS ABOUT ANIMATION SPEED
        if (currentVelocity > 9)
        {
            gameObject.GetComponent<Animator>().speed = 1.45f;
        }

        else if (currentVelocity < 5.5f)
        {
            gameObject.GetComponent<Animator>().speed = 0.75f;
        }

        else if (currentVelocity < 3.5f)
        {
            gameObject.GetComponent<Animator>().speed = 0.20f;
        }

        else
        {
            gameObject.GetComponent<Animator>().speed = 1f;
        }
    }
}
