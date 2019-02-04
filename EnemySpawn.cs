using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    

    public GameObject enemy1;
    public GameObject enemy2;

    public GameObject player;
    private Vector3 startPos;

    private int enemyNum = 0;
    private bool working;

	// Use this for initialization
	void Start () {
        working = true;
        startPos = (gameObject.transform.position);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (working)
        {
            StartCoroutine(Spawn(enemyNum = Random.Range(1, 3)));
        }

        //these objects will be moving along with the player but will not follow it around when jumping or falling, so they don't have to be childed to player or camera
        gameObject.transform.position = new Vector3(startPos.x + player.transform.position.x, startPos.y, startPos.z);
	}

    private IEnumerator Spawn(int enemyNum)
    {
        working = false;
        yield return new WaitForSeconds(Random.Range(2.7f, 6.5f));

        if (enemyNum == 1)
        {
            Instantiate(enemy1, new Vector2 (transform.position.x, Random.Range(transform.position.y - 0.35f, transform.position.y + 0.35f)), Quaternion.identity);     //makes enemies spawn each at a slighlty different height
        }

        else if (enemyNum == 2)
        {
            Instantiate(enemy2, transform.position, Quaternion.identity);
        }

        working = true;
    }
}
