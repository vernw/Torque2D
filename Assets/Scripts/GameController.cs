using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public int livesP1;
    public int livesP2;

    public bool blackHoles;
    public bool whiteHoles;
    public bool gravityFields;
    public bool depthCharges;
    public bool powerUps;

    public int musicVolume;
    public int effectsVolume;

    void Start () {
        livesP1 = 5;
        livesP2 = 5;

        blackHoles = false;
        whiteHoles = false;
        gravityFields = false;
        depthCharges = false;
        powerUps = false;

        musicVolume = 50;
        effectsVolume = 50;
	}

    void GameEnd(int winner)
    {
        // Victory screens
    }

    void Update()
    {
        if (livesP1 == 0)
        {
            GameEnd(2);
        }
        if (livesP2 == 0)
        {
            GameEnd(1);
        }
    }
}
