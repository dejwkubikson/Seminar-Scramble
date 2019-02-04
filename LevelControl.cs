using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

	public int scene = 0;
	public int objectsHit = 0;
	public float timeAtEndLevelOne = 0;

	private bool setTime = false;
	private bool timePassed = false;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {

		Scene currentScene = SceneManager.GetActiveScene ();
		GameObject player = GameObject.Find ("Player");
		if (player) {
			TimeBehaviour timeScript = player.GetComponent<TimeBehaviour> ();
			LevelEnd levelEndScript = player.GetComponent<LevelEnd> ();

			if (currentScene.name == "TestScene2" && timePassed == false) {
				scene = 2;
				timeScript.timeLeft = timeAtEndLevelOne;
				timePassed = true;
			}

			if (levelEndScript.levelEnded && setTime == false && scene == 1){
				SettingsForSecondLevel (timeScript.timeLeft);
			}
		}

		GameObject scoreControlObject = GameObject.Find ("ScoreControlObject");
		if (scoreControlObject) {
			ScoreControl scoreControlScript = scoreControlObject.GetComponent<ScoreControl> ();
			scoreControlScript.objectsHit = objectsHit;
		}
	
		if (currentScene.name == "TestScene") {
			scene = 1;
		}
	}

	void SettingsForSecondLevel(float timeLeft){
		setTime = true;
		timeAtEndLevelOne = timeLeft;
	}
}
