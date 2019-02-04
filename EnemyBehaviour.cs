using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private float speed;    //how quick the enemies move
    private bool gotHit = false;

    public float heavyColl;
    public float midColl;
    public float softColl;



    private void Start()
    {
        speed = Random.Range(3.5f, 7.1f);
        StartCoroutine("AutoCancel");
    }
        

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        if (gotHit)
        {
            transform.Rotate(Vector3.forward * 270 * Time.deltaTime);
        }
	}

    private IEnumerator AutoCancel()
    {
        yield return new WaitForSeconds(7.5f);
        Destroy(gameObject);
    }

    private IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        yield return new WaitForEndOfFrame();
        
        if (other.gameObject.name == "Player")
        {
            gotHit = true;      //the bird will start spinning
            gameObject.GetComponent<CircleCollider2D>().enabled = false;    //prevents triggers from happening more than once
            GetComponent<AudioSource>().Play();     //Sound fx

            GameObject mainCameraObj = GameObject.Find("Main Camera");
            mainCameraObj.GetComponent<CameraRig>().StartCoroutine("Tilt");

            GameObject levelControlObject = GameObject.Find("LevelControlObject");
           	LevelControl levelControlScript = levelControlObject.GetComponent<LevelControl>();
            levelControlScript.objectsHit++;

            StartCoroutine(MakeBlink(other.gameObject));

            other.gameObject.GetComponent<Animator>().SetBool("Tripping", true);   //enables tripping animation


            if (other.gameObject.GetComponent<PlayerControll>().currentVelocity > 9.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 4.0f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(heavyColl, 0);
            }

            else if (other.gameObject.GetComponent<PlayerControll>().currentVelocity < 9.5f && other.gameObject.GetComponent<PlayerControll>().currentVelocity > 4.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 1.5f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(midColl, 0);
            }

            else if (other.gameObject.GetComponent<PlayerControll>().currentVelocity < 4.5f && other.gameObject.GetComponent<PlayerControll>().currentVelocity > 0.5f)
            {
                other.gameObject.GetComponent<PlayerControll>().currentSpeed -= 1f;
                other.gameObject.GetComponent<Rigidbody2D>().velocity -= new Vector2(softColl, 0);
            }

            yield return new WaitForSeconds(0.12f);
            other.gameObject.GetComponent<Animator>().SetBool("Tripping", false);

            yield return new WaitForSeconds (1.5f);
        }
    }

    public IEnumerator MakeBlink(GameObject player)
    {

        for (int i = 0; i < 3; i++)
        {
            player.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.08f);
            player.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.08f);

        }
    }
}
