using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GroundCheckerScript : MonoBehaviour {

    public bool onTrack;
    public bool canRay;
  
    public TimeBehaviour tBehav;

    private void Start()
    {
        canRay = true;
    }

    // Update is called once per frame
    void Update ()
    {
        PlayerRaycast();
        bool canCall = true;   //used to make the script call "death" only once without it crashing

        if (!onTrack && canCall)
        {
            tBehav.DeathCall();
            canCall = false;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine (SuspendRay());
            Debug.Log("suspend");
        }
	}

    public IEnumerator SuspendRay()     //raycast will be shut down for a moment to prevent it from returning false positives that would end the game
    {
        canRay = false;
        yield return new WaitForSeconds(0.05f);
        canRay = true;
    }

    private void PlayerRaycast()
    {
        {
            // downward raycast that checks if the player is touching ground
            RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
            if (canRay && rayDown.collider != null)
            {
                onTrack = true;
            }
            else if (canRay && rayDown.collider == null)
            {
                onTrack = false;
            }
        }
    }
}
