using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject pauseSymbol;
    public GameObject restartButton;
    public GameObject mainMenuButton;

    private bool paused = false;

	// Use this for initialization
	void Start () {

    }	

	/*void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!paused)
            {
                StartCoroutine("PauseGame");
            }
            else
            {
                UnPause();
            }
        }
	}*/

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseSymbol.SetActive(true);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
        GetComponent<JumpScript>().enabled = false;
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseSymbol.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        GetComponent<JumpScript>().enabled = true;
    }
}
