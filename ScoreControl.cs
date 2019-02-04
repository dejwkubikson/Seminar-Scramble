using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {

	public GameObject leaderBoard;
	LeaderBoard leaderBoardScript;

	public string playerName = "";
    public int objectsHit = 0;
    public LevelEnd levelEndScript;
    public TimeBehaviour timeScript;
	public PlayerScoreList playerScoreList;
    public Text scoreText;

    private int timeAtEnd = 0;
    private int endScore = 0;
    private int numDigits = 0;
	private int highestScore = 0;

    private void ShowScoreOnce()
    {
		levelEndScript.gameEnded = false;

		GameObject levelControlObject = GameObject.Find ("LevelControlObject");
		LevelControl levelControlScript = levelControlObject.GetComponent<LevelControl> ();
		objectsHit = levelControlScript.objectsHit;

        timeAtEnd = (int)timeScript.timeLeft;

        numDigits = timeAtEnd.ToString().Length; // checking how much digits there is in order to have blank space between "Time left:" and "s"

        endScore = timeAtEnd * 100 - objectsHit * 10;

		if (endScore < 0)
			endScore = 0;
		
		if (numDigits == 1)
            scoreText.text = "Time Left:   " + "s\nObstacles hit: " + "\nScore: "; // 2 "space bars" added due to the width of digits
        if (numDigits == 2)
            scoreText.text = "Time Left:     " + "s\nObstacles hit: " + "\nScore: "; // 4 "space bars" added due to the width of digits

		// checking if the player has already have a highest score key
		if (PlayerPrefs.HasKey ("HighestScore") == false)
			PlayerPrefs.SetInt("HighestScore", endScore);

		// setting players score if its better than previous one NOTE: it needs to be <= if it's player's first time playing
		if (PlayerPrefs.GetInt ("HighestScore") <= endScore) {
			leaderBoardScript.SetScore (playerName, "objectsHit", objectsHit);
			leaderBoardScript.SetScore (playerName, "timeLeft", timeAtEnd);
			leaderBoardScript.SetScore (playerName, "score", endScore);

			// saving all the values
			PlayerPrefs.SetInt ("ObjectsHit", objectsHit);
			PlayerPrefs.SetInt ("TimeLeft", timeAtEnd);
			PlayerPrefs.SetInt ("HighestScore", endScore);
		}

		playerScoreList.updateList = true;
        StartCoroutine(AddShowEffectTime());
       
        //scoreText.text = "Time Left: " + timeAtEnd.ToString() + "s\nObstacles hit: " + objectsHit.ToString() + "\nScore: " + endScore.ToString();
	}

    // These coroutines add some effect to displaying the score
    IEnumerator AddShowEffectTime()
    {  
        yield return new WaitForSeconds(0.6f);
        scoreText.text = "Time Left: " + timeAtEnd.ToString() + "s\nObstacles hit: " + "\nScore: ";
        StartCoroutine(AddShowEffectObstacles());
    }

    IEnumerator AddShowEffectObstacles()
    {
        yield return new WaitForSeconds(0.6f);
        scoreText.text = "Time Left: " + timeAtEnd.ToString() + "s\nObstacles hit: " + objectsHit.ToString() + "\nScore: ";
        StartCoroutine(AddShowEffectScore());
    }
    
	IEnumerator AddShowEffectScore()
    {
        yield return new WaitForSeconds(0.6f);
        scoreText.text = "Time Left: " + timeAtEnd.ToString() + "s\nObstacles hit: " + objectsHit.ToString() + "\nScore: <color=yellow>" + endScore.ToString() +"</color>"; // Score points display in yellow colour
		StartCoroutine(ShowLeaderBoard());
	}

	IEnumerator ShowLeaderBoard()
	{
		yield return new WaitForSeconds (1.0f);
		scoreText.text = "";
		leaderBoard.SetActive(true);
	}

    // Use this for initialization
    void Start () {
		objectsHit = 0;

		leaderBoardScript = GameObject.FindObjectOfType<LeaderBoard> ();
		
		playerName = PlayerPrefs.GetString ("Name", "Unknown");
	}
	
	// Update is called once per frame
	void Update () {

        if(levelEndScript.gameEnded)
        {
            ShowScoreOnce();
        }
		
	}
}
