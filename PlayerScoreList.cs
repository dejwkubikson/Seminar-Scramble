using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;
	public bool updateList;
	ScoreControl scoreControl;
	LeaderBoard leaderBoard;

	// Use this for initialization
	void Start () {
		leaderBoard = GameObject.FindObjectOfType<LeaderBoard> ();
	}

	// Update is called once per frame
	void Update () {

		//if (updateList) {
			// Deleting the objects to prevent from duplicating
			while (this.transform.childCount > 0) {
				Transform child = this.transform.GetChild (0);
				child.SetParent (null); 
				Destroy (child.gameObject);
			}

			string[] names = leaderBoard.GetPlayerNames ("score"); 

			foreach (string name in names) {
				GameObject playerEntryObject = (GameObject)Instantiate (playerScoreEntryPrefab);
				playerEntryObject.transform.SetParent (this.transform);
				playerEntryObject.transform.Find ("Name").GetComponent<Text> ().text = name;
				playerEntryObject.transform.Find ("ObjectsHit").GetComponent<Text> ().text = leaderBoard.GetScore (name, "objectsHit").ToString ();
				playerEntryObject.transform.Find ("TimeLeft").GetComponent<Text> ().text = leaderBoard.GetScore (name, "timeLeft").ToString ();
				playerEntryObject.transform.Find ("Score").GetComponent<Text> ().text = leaderBoard.GetScore (name, "score").ToString ();
			}
		//}
	}
}
