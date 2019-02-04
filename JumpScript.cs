using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{

    public Vector2 TopFloor;
    public Vector2 MiddleFloor;
    public Vector2 DownFloor;

    private Vector2 lastPos;

    public PlayerControll pControl;
    public float seconds;
    public bool canJump;

    public AudioClip fallSound;
    public AudioClip jumpSound;

    //mobile controls
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;

    private void Start()
    {
        canJump = true;
        lastPos = gameObject.transform.position;

        dragDistance = Screen.height * 10 / 100;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canJump)
            {
                Fall();
            }
        }

        //MOBILE CONTROLS
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
                if (touch.position.y > Screen.height / 2)
                {
                    Jump();
                }
                else
                {
                    Fall();
                }
                /*
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
            }
            
            //Check if drag distance is greater than 10% of the screen height
            if (Mathf.Abs(lp.y - fp.y) > dragDistance)
            {
                //the vertical movement is greater than the horizontal movement
                if (lp.y > fp.y)  //If the movement was up
                {   
                    //Up swipe
                    if (canJump)
                    {
                        Jump();
                    }
                }

                else
                {   //Down swipe
                    if (canJump)
                    {
                        Fall();
                    }
                }*/
            }
        }
    }
    

    //There's a variety of ways that can be used to move the player from one platform to the others
    private void Jump()
    {

        if (pControl.bDownFloor == true)
        {
            StartCoroutine(Jump(MiddleFloor, seconds));

            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = jumpSound;
            audio.Play();

            //Here is a list of all the previous iteration of this code; they didn't work
            //TELEPORT
            //gameObject.transform.Translate(0, 4.001f, 0);

            //ADD FORCE METHOD
            // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower));

            //MOVE TOWARDS
            //transform.position = Vector3.MoveTowards(transform.position, MiddleFloor + new Vector2(0, 0.75f), 100.5f*Time.deltaTime);

            //LERP
            //transform.position = Vector3.Lerp(transform.position, MiddleFloor, smoothness*Time.deltaTime);
        }

        if (pControl.bMiddleFloor == true)
        {
            StartCoroutine(Jump(TopFloor, seconds));

            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = jumpSound;
            audio.Play();
        }

        if (pControl.bTopFloor == true)
        {

        }
    }

    private void Fall()
    {

        if (pControl.bTopFloor == true)
        {
            StartCoroutine(Jump(MiddleFloor, seconds));

            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = fallSound;
            audio.Play();
        }

        if (pControl.bMiddleFloor == true)
        {
            StartCoroutine(Jump(DownFloor, seconds));

            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.clip = fallSound;
            audio.Play();
        }
    }

    public IEnumerator Jump(Vector2 end, float seconds)
    {
        canJump = false;

        float elapsedTime = 0;
        Vector2 startingPos = lastPos;

        while (elapsedTime < seconds)
        {
            gameObject.transform.position = Vector2.Lerp(new Vector2(gameObject.transform.position.x, startingPos.y), new Vector2 (gameObject.transform.position.x, end.y), (elapsedTime / seconds));
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        lastPos = end;

        canJump = true;
    }
}