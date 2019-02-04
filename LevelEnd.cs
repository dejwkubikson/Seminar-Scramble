using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    public Text timeText;
	public bool gameEnded = false;
	public bool levelEnded = false;

    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject nextLevelButton;
    public GameObject pauseButton;

    public TimeBehaviour tBehav;
			
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "End") {
			Scene currentScene = SceneManager.GetActiveScene ();

			collision.gameObject.GetComponent<AudioSource> ().Play ();

			timeText.enabled = false;

			GameObject camera = GameObject.Find ("Main Camera");
			CameraRig cRig = camera.GetComponent<CameraRig> ();
			Destroy (cRig);

			gameObject.GetComponent<CapsuleCollider2D> ().enabled = false;

			tBehav.canBeCalled = false;

			//these two lines hide some sounds that shouldn't be heard after the game finishes
			gameObject.GetComponent<CapsuleCollider2D> ().enabled = false;
			gameObject.GetComponent<AudioSource> ().volume = 0;

			restartButton.SetActive (true);
			mainMenuButton.SetActive (true);
			if (currentScene.name == "TestScene")
				nextLevelButton.SetActive (true);
			pauseButton.SetActive (false);

			if (currentScene.name == "TestScene2") {
				gameEnded = true;
			}

			if (currentScene.name == "TestScene") {
				levelEnded = true;				
			}
		}
     }
}