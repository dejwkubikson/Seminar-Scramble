using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {

	public string nameText = "";
	public Text textField;
	public GameObject inputField;
	public bool hasKey = false;

    public bool mutedMusic = false;
    public bool mutedAll = false;

    public GameObject canvas;
    public GameObject musicButton;
    public GameObject allSoundsButton;
    public GameObject player;

    public Sprite musicOff;
    public Sprite musicOn;
    public Sprite allSoundsOff;
    public Sprite allSoundsOn;

    public void LoadMainLevel()
    {
			// if player didnt put his name and he hasnt picked one already
			if (nameText == "" && hasKey == false)
				nameText = "Unknown";

			if (hasKey == false)
				PlayerPrefs.SetString ("Name", nameText);
			
			Application.LoadLevel ("TestScene");
			Time.timeScale = 1;
    }

	public void SetName(string name){
		nameText = name;
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSecondLevel()
    {
        Application.LoadLevel("TestScene2");
    }

    public void LoadMenu()
    {
        Application.LoadLevel("Menu");
        Time.timeScale = 1;
    }

    public void MuteMusic()
    {
        if (!mutedMusic)
        {
            canvas.GetComponent<AudioSource>().volume = 0;
            mutedMusic = true;
            GameDataScript.mutedMusic = true;
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        
        else if (mutedMusic)
        {
            canvas.GetComponent<AudioSource>().volume = 0.1f;
            mutedMusic = false;
            GameDataScript.mutedMusic = false;
            musicButton.GetComponent<Image>().sprite = musicOn;
        }
    }

    public void MuteAll()
    {
        if (!mutedAll)
        {
            AudioListener.volume = 0;
            GameDataScript.mutedAll = true;
            mutedAll = true;
            allSoundsButton.GetComponent<Image>().sprite = allSoundsOff;
        }

        else if (mutedAll)
        {
            AudioListener.volume = 1;
            GameDataScript.mutedAll = false;
            mutedAll = false;
            allSoundsButton.GetComponent<Image>().sprite = allSoundsOn;
        }
    }
    
    public void PauseButton()
    {
        player.GetComponent<Pause>().PauseGame();
    }

    public void UnPauseButton()
    {
        player.GetComponent<Pause>().UnPause();
    }
    void Start()
	{
		// use this to test the game - delets all the keys that are saved
		if(PlayerPrefs.GetString("Name") == "Unknown")
			PlayerPrefs.DeleteAll();

		Scene currentScene = SceneManager.GetActiveScene ();

		// if player already has choosen a name before, he can't change it here (maybe in options?)
		if (PlayerPrefs.HasKey ("Name") && currentScene.name == "Menu") {
			nameText = PlayerPrefs.GetString ("Name");
			textField.text = "Welcome back, " + nameText;
			inputField.SetActive (false);
			hasKey = true;
		}
	}
}
