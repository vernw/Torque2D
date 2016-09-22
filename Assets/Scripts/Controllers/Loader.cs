using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameController;
    
    // Creates instance of gameController if it does not exist
	void Awake () {
        if (GameController.instance == null)
            Instantiate(gameController);
	}
}
