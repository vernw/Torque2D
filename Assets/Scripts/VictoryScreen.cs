using UnityEngine;
using System.Collections;

public class VictoryScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Replay
            Application.LoadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Back to menu
            Application.LoadLevel(0);
        }
    }
}
