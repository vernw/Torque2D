﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

    GameController gameController;

    void Start()
    {
        gameController = GameController.instance;

        // Reset all gameplay values
    }
    	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Replay
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Back to menu
            SceneManager.LoadScene(0);
        }
    }
}
