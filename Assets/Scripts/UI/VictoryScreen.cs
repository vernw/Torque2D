using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

    GameController gameController;

    void Start()
    {
        gameController = GameController.instance;
    }
    	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Replay
            SceneManager.LoadScene(1);
            //gameController.gameReset();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Back to menu
            SceneManager.LoadScene(0);
            StartCoroutine(MenuController.instance.MoveTo("menu"));
        }
    }
}
