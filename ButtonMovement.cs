using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<RectTransform>().position.x > Screen.width/2)
        {
            gameObject.GetComponent<RectTransform>().Translate(Vector3.left * Time.deltaTime * 227);
        }
	}
}
