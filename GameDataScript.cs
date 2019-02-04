using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public static bool thisScript;

    public static bool mutedMusic = false;
    public static bool mutedAll = false;

    private GameObject canvas;
    // The following script makes sure that there will only be one instance of this gameObject
    void Start()
    {

        if (thisScript)     //if this is NOT the first instance of this object, destroy it
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);      //if it IS the first instance of this object in the scene, don't destroy it
            thisScript = this;
        }
    }

    void Update()
    {
        if (mutedMusic)
        {
            if (canvas == null)
            {
                canvas = GameObject.Find("CanvasM");
                if (canvas != null)
                {
                    canvas.GetComponent<AudioSource>().enabled = false;
                }
            }

        }

        else if (!mutedMusic)
        {
            if (mutedMusic)
            {
                if (canvas == null)
                {
                    canvas = GameObject.Find("CanvasM");
                    if (canvas != null)
                    {
                        canvas.GetComponent<AudioSource>().enabled = true;
                    }
                }

            }

            if (mutedAll)
            {
                AudioListener.volume = 0;
            }
            else if (!mutedAll)
            {
                AudioListener.volume = 1;
            }
        }
    }
}
