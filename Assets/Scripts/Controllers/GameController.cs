using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Game Controller that manages all the game setup data, including player count, lives, countdown freezing, etc.
 * Contains gameEnd(), which is activated when only one player is left alive.
 * Instantiates a victory screen whose script can be located in VictoryScreen.cs.
*/

public class GameController : MonoBehaviour {

    /*
    public static GameController instance = null;

    public MenuController menuController;
    public LifeOverlay lifeOverlay;

    public int maxLives = 5;
    public bool countdown = false;
    public GameObject victoryScreen;

    public bool blackHoles;
    public bool whiteHoles;
    public bool gravityFields;
    public bool depthCharges;
    public bool powerUps;

    List<Player> players;
<<<<<<< HEAD
    
=======

    private int _musicVolume;
    public int musicVolume
    {
        get { return musicVolume; }
        set
        {
            if (value < 0)
                _musicVolume = 0;
            else if (value > 100)
                _musicVolume = 100;
            else
                _musicVolume = value;
        }
    }
    private int _effectsVolume;
    public int effectsVolume
    {
        get { return _effectsVolume; }
        set
        {
            if (value < 0)
                _effectsVolume = 0;
            else if (value > 100)
                _effectsVolume = 100;
            else
                _effectsVolume = value;
        }
    }

>>>>>>> refs/remotes/origin/master
    void Awake()
    {
        // Ensures singleton status of GameController
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        players = new List<Player>();
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));

        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
        lifeOverlay.CustomStart(players);

        Camera.main.GetComponent<GameCamera>().players = players;

        foreach(Player player in players) {
            player.onDamage = delegate(Player _player) {
                GameObject.Find("GameController").GetComponent<GameController>().lifeOverlay.UpdateLife(_player);
            };
            player.onDeath = delegate(Player _player) {
                _player.doDestruct();
            };
        }

        try {
            menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
            lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
        } catch (UnityException e) { }

        blackHoles = false;
        whiteHoles = false;
        gravityFields = false;
        depthCharges = false;
        powerUps = false;

<<<<<<< HEAD
        // lifeOverlay = ((FindObjectsOfType(typeof(LifeOverlay))))[0].GetComponent<LifeOverlay>();
        // print(lifeOverlay);
=======
        musicVolume = 50;
        effectsVolume = 50;
>>>>>>> refs/remotes/origin/master
    }

    IEnumerator GameEnd()
    {
        // Display victory screen
        int winnerNumber = 0;
        TextMesh victoryPlayerTextMesh = victoryScreen.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryScreen.transform.GetChild(2).GetComponent<TextMesh>();

        yield return new WaitForSeconds(2.5f);

        // Check for final player
        // if (livesP1 > 0)
        // {
        //     winnerNumber = 1;
        // }
        // else if (livesP2 > 0)
        // {
        //     winnerNumber = 2;
        // }
        // else if (livesP3 > 0)
        // {
        //     winnerNumber = 3;
        // }
        // else if (livesP4 > 0)
        // {
        //     winnerNumber = 4;
        // }

        // Victory screens
        // switch (winnerNumber)
        // {
        //     case 1:
        //         victoryNumberTextMesh.text = "1";
        //         victoryNumberTextMesh.color = _P1Color;
        //         victoryPlayerTextMesh.color = _P1Color;
        //         break;
        //     case 2:
        //         victoryNumberTextMesh.text = "2";
        //         victoryNumberTextMesh.color = _P2Color;
        //         victoryPlayerTextMesh.color = _P2Color;
        //         break;
        //     case 3:
        //         victoryNumberTextMesh.text = "3";
        //         victoryNumberTextMesh.color = _P3Color;
        //         victoryPlayerTextMesh.color = _P3Color;
        //         break;
        //     case 4:
        //         victoryNumberTextMesh.text = "4";
        //         victoryNumberTextMesh.color = _P4Color;
        //         victoryPlayerTextMesh.color = _P4Color;
        //         break;
        // }
        // victoryScreen.SetActive(true);
    }

    // Called by VictoryScreen.cs to reset game values if necessary - unused
    public void gameReset()
    {
        Debug.Log("Reset!");
    }
    */
}
