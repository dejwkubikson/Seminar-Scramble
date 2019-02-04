using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour {

    public float timeLeft = 6;
    public Text timeText;
    public Text deathText;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject pauseButton;

    public GameObject deathScreen;
    public bool isDead = false;
    public float extraTime = 3.05f;

    public bool canBeCalled = true;     //this will ensure that the coroutine will only be called once, since more than one script can call it


	void Update () {
        timeLeft -= Time.deltaTime;
        timeText.text = "Time Left: " + (int)timeLeft;
        bool canCall = true;    //this will ensure that Update won't call the coroutine more than once

        if(timeLeft <= 0 && canCall)
        {
            canCall = false;

            StartCoroutine(Death(false, true));
        }
    }

    public void AddTime()
    {
        timeLeft += extraTime;
    }

    public IEnumerator Death(bool fall, bool timeUp)
    {

        if (canBeCalled)
        {
            canBeCalled = false;

            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<AudioSource>().volume = 0;

            yield return new WaitForSeconds(0.135f);
                
            //The following two if statements are not empty, they were replaced with an image that already has text on it
            timeText.enabled = false;
            if (fall)
            {
                deathText.text = "";
            }

            if (timeUp)
            {
                deathText.text = "";
            }

            deathScreen.SetActive(true);    //the player will not see the character sadly falling down

            isDead = true;
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
            pauseButton.SetActive(false);

            Destroy(gameObject.GetComponent<LevelEnd>());
        }
    }

    public void DeathCall() //other scripts can access this method to avoid bugs
    {
        StartCoroutine (Death(true, false));
    }
}
