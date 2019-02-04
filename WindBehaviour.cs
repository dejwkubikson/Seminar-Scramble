using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehaviour : MonoBehaviour
{

    public GameObject player;
    public GameObject windSource;
    public float windStrength;
    private PlayerControll pControl;

    private void Start()
    {
        pControl = player.GetComponent<PlayerControll>();
    }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (player.transform.position.x < windSource.transform.position.x)
            {
                Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();

                if (pControl.currentVelocity > 1.5f)
                {
                    playerRB.AddForce (new Vector2(-windStrength, 0));
                    pControl.currentSpeed -= windStrength;
                }
            }

            yield return new WaitForSeconds(0.17f);
        }
    }
}