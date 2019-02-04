using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour : MonoBehaviour {

    public bool shut = false;
    public Sprite flashColour;
    public Sprite baseColour;
    public Sprite shutWindow;

    private void Start()
    {
        StartCoroutine("Flash");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            shut = true;
        }

        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().sprite = shutWindow;

        collision.gameObject.GetComponent<TimeBehaviour>().AddTime();
        collision.gameObject.GetComponent<TimeBehaviour>().AddTime();
    }

    private IEnumerator Flash()
    {
        while(!shut)
        {
            GetComponent<SpriteRenderer>().sprite = flashColour;
            yield return new WaitForSeconds(0.17f);
            GetComponent<SpriteRenderer>().sprite = baseColour;
            yield return new WaitForSeconds(0.17f);
        }
    }
}
