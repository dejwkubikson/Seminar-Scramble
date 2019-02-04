using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ExtraTimeScript : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        TimeBehaviour timeBehaviour = collision.GetComponent<TimeBehaviour>();
        gameObject.GetComponent<AudioSource>().Play();

        if (collision.gameObject.tag == "Player" && timeBehaviour != null)
        {
            timeBehaviour.AddTime();    //adds five seconds to the time
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
