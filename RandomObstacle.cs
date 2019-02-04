using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour {

    public GameObject[] enemies;
    private int rng;

	// Use this for initialization
	void Start () {
        rng = Random.Range(0, 2);

        if (rng == 0)
        {
            Instantiate(enemies[0], gameObject.transform.position, Quaternion.identity);
        }

        if (rng == 1)
        {
            Instantiate(enemies[1], gameObject.transform.position, Quaternion.identity);
        }
    }   
}
