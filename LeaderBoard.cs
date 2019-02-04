using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Helps using the GetPlayerNames() function

public class LeaderBoard : MonoBehaviour {

	Dictionary<string, Dictionary<string, int>> playerScores;

	void Start(){
		SetScore ("Dawid", "objectsHit", 1);
		SetScore ("Dawid", "timeLeft", 15);
		SetScore ("Dawid", "score", 1490);

		SetScore ("Kirsty", "objectsHit", 0);
		SetScore ("Kirsty", "timeLeft", 9);
		SetScore ("Kirsty", "score", 900);

		SetScore ("Andrea", "objectsHit", 4);
		SetScore ("Andrea", "timeLeft", 17);
		SetScore ("Andrea", "score", 1660);

		SetScore ("Josh", "objectsHit", 3);
		SetScore ("Josh", "timeLeft", 11);
		SetScore ("Josh", "score", 1070);

		SetScore ("Ross", "objectsHit", 8);
		SetScore ("Ross", "timeLeft", 5);
		SetScore ("Ross", "score", 420);

		SetScore ("Will", "objectsHit", 2);
		SetScore ("Will", "timeLeft", 9);
		SetScore ("Will", "score", 880);

		if(PlayerPrefs.HasKey("Name") && PlayerPrefs.HasKey("HighestScore"))
		{
			SetScore (PlayerPrefs.GetString ("Name"), "objectsHit", PlayerPrefs.GetInt ("ObjectsHit"));
			SetScore (PlayerPrefs.GetString ("Name"), "timeLeft", PlayerPrefs.GetInt ("TimeLeft"));
			SetScore (PlayerPrefs.GetString ("Name"), "score", PlayerPrefs.GetInt ("HighestScore"));
		}
	}

	// Initializes the player's score if it doesnt exist
	void Init(){
		if (playerScores != null)
			return;
		playerScores = new Dictionary<string, Dictionary<string, int>> ();
	}

	// gets the score of the type choosen
	public int GetScore(string username, string scoreType){
		Init ();

		// if player hasnt got any score he will have 0
		if(playerScores.ContainsKey(username) == false){
			return 0;
		}

		//if the player doesnt have this type of score e.g. objects hit it will be 0
		if(playerScores[username].ContainsKey(scoreType) == false){
			return 0;
		}
			
		return playerScores[username][scoreType];
	}

	// sets the score
	public void SetScore(string username, string scoreType, int value){
		Init ();

		if (playerScores.ContainsKey (username) == false) {
			playerScores [username] = new Dictionary<string, int> ();
		}

		playerScores [username] [scoreType] = value;
	}
		
	// returns an array of player names sorted by score
	public string[] GetPlayerNames(string sortingScoreType){
		Init ();
		return playerScores.Keys.OrderByDescending( n => GetScore(n, sortingScoreType)).ToArray();
	}

}
