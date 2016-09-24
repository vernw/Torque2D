using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Game Controller that manages all the game setup data, including player count, lives, countdown freezing, etc.
 * Contains gameEnd(), which is activated when only one player is left alive.
 * Instantiates a victory screen whose script can be located in VictoryScreen.cs.
*/

public class TDMController : MonoBehaviour {

    public static TDMController instance = null;

    public MenuController menuController;
    public LifeOverlay lifeOverlay;

    public GameObject team1;
    public GameObject team2;

    public int maxLives = 5;
    public bool countdown = false;
    public GameObject victoryScreen;

    List<Player> players;

    void Awake()
    {
        // Ensures singleton status of TDMController
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        GameObject team1Prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Team1", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
        GameObject team2Prefab = (GameObject)Instantiate(Resources.Load("Prefabs/Team2", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);

        players = new List<Player>();
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives, team1Prefab));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives, team2Prefab));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives, team1Prefab));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives, team2Prefab));

        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
        lifeOverlay.CustomStart(players);

        Camera.main.GetComponent<GameCamera>().players = players;

        foreach(Player player in players) {
            player.onDamage = delegate(Player _player) {
                GameObject.Find("GameController").GetComponent<GameController>().lifeOverlay.UpdateLife(_player);
            };
            player.onDeath = delegate(Player _player) {
                // print(this);
                // StartCoroutine("Destruct");
                // _player.StartCoroutine("Destruct");
                _player.doDestruct();
            };
        }

        try {
            menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
            lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
        } catch (UnityException e) { }
    }

    IEnumerator GameEnd()
    {
        // Display victory screen
        int winnerNumber = 0;
        TextMesh victoryPlayerTextMesh = victoryScreen.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryScreen.transform.GetChild(2).GetComponent<TextMesh>();

        yield return new WaitForSeconds(2.5f);
    }

    // Called by VictoryScreen.cs to reset game values if necessary - unused
    public void gameReset()
    {
        Debug.Log("Reset!");

        // countdown = true;

        // Resets game start values
        // totalPlayers = 4;
        // livesP1 = maxLives;
        // livesP2 = maxLives;
        // livesP3 = maxLives;
        // livesP4 = maxLives;

        // P1 = GameObject.FindGameObjectWithTag("P1");
        // P2 = GameObject.FindGameObjectWithTag("P2");
        // P3 = GameObject.FindGameObjectWithTag("P3");
        // P4 = GameObject.FindGameObjectWithTag("P4");

        // _P1Color = P1.GetComponent<SpriteRenderer>().color;
        // _P2Color = P2.GetComponent<SpriteRenderer>().color;
        // _P3Color = P3.GetComponent<SpriteRenderer>().color;
        // _P4Color = P4.GetComponent<SpriteRenderer>().color;
    }
}
