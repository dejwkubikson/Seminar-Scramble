using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    public float heavyColl;
    public float midColl;
    public float softColl;

    public Sprite fallenTable;
    public Sprite flashingMop;
    public Sprite flashingSign;
    public Sprite norMop;
    public Sprite norSign;
    private bool alive = true;

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //THINGS THAT HAPPEN TO THE OBJECT THAT GOT HIT
            gameObject.GetComponent<CircleCollider2D>().enabled = false;    //prevents triggers from happening more than once         
            gameObject.GetComponent<SpriteRenderer>().sprite = fallenTable;
            alive = false;

            if(gameObject.name == "ObstacleSign(Clone)")
            {
                gameObject.transform.position += new Vector3 (0, 0.2f, 0);
            }
            else
            {
                gameObject.transform.position -= new Vector3(0, 0.12f, 0);
            }

            //KEEPING TRACK OF HOW MANY OBJECTS HAVE BEEN HIT
            GameObject levelControlObject = GameObject.Find("LevelControlObject");
            LevelControl levelControlScript = levelControlObject.GetComponent<LevelControl>();
           	levelControlScript.objectsHit++;    //finds the player's score control script to add the number of enemies hit by one

            //CAMERA FX
            GameObject mainCameraObj = GameObject.Find("Main Camera");
            mainCameraObj.GetComponent<CameraRig>().StartCoroutine("Tilt");     //finds the camera object and makes it tit
            
            //PLAYER BEHAVIOUR ACCORDING TO THE COLLISION
            StartCoroutine(MakeBlink(other.gameObject));
            other.gameObject.GetComponent<Animator>().SetBool("Tripping", true);

            //SOUND
            GetComponent<AudioSource>().Play();

            if (other.gameObject.GetComponent<PlayerControll>().currentVelocity > 9.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 4.0f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(heavyColl, 0);
            }

            else if (other.gameObject.GetComponent<PlayerControll>().currentVelocity < 9.5f && other.gameObject.GetComponent<PlayerControll>().currentVelocity > 4.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 2.00f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(midColl, 0);
            }

            else if (other.gameObject.GetComponent<PlayerControll>().currentVelocity < 4.5f && other.gameObject.GetComponent<PlayerControll>().currentVelocity > 0.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 17f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(softColl, 0);
            }

            yield return new WaitForSeconds(0.12f);
            other.gameObject.GetComponent<Animator>().SetBool("Tripping", false);

            yield return new WaitForSeconds(3.1f);
            Destroy(gameObject);
        }
    }

    public IEnumerator MakeBlink(GameObject player)     //makes the player blink quickly to indicate that they have been hit
    {
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        for (int i = 0; i < 3; i++)
        {
            player.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.08f);
            player.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.08f);
        }
        player.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private IEnumerator Flashing()
    {
        if (gameObject.name == "ObstacleSign(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = flashingSign;
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().sprite = norSign;
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = flashingMop;
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<SpriteRenderer>().sprite = norMop;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForEndOfFrame();
        if (alive)
        {
            StartCoroutine("Flashing");
        }
    }

    private void Start()
    {
        StartCoroutine("Flashing");
    }
}
