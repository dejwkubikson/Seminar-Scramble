using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour {

    private Vector3 startPos;
    public GameObject player;

    public bool tiltingLeft = false;
    public bool tiltingRight = false;

    // Use this for initialization
    void Start()
    {
        startPos = (gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= 0.51f)
        {
            gameObject.transform.position = new Vector3(startPos.x + player.transform.position.x, startPos.y, startPos.z);

            if (tiltingLeft)
            {
                gameObject.transform.Translate(Vector3.down * Time.deltaTime * 3.2f);
            }

            if (tiltingRight)
            {
                gameObject.transform.Translate(Vector3.up * Time.deltaTime * 3.2f);
            }
        }
    }

    public IEnumerator Tilt()
    {
        for (int i = 0; i < 3; i++)
        {
            tiltingLeft = true;
            yield return new WaitForSeconds(0.11f);
            tiltingLeft = false;
            tiltingRight = true;
            yield return new WaitForSeconds(0.11f);
            tiltingRight = false;
        }
    }
}
