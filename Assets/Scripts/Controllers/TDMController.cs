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
    public bool playing = true;
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
        team1 = (GameObject)Instantiate(Resources.Load("Prefabs/Team1", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
        team2 = (GameObject)Instantiate(Resources.Load("Prefabs/Team2", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);

        players = new List<Player>();
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives, team1));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives, team2));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives, team1));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives, team2));

        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
        lifeOverlay.CustomStart(players, maxLives);

        Camera.main.GetComponent<GameCamera>().players = players;

        foreach(Player player in players) {
            player.onDamage = delegate(Player _player) {
                lifeOverlay.UpdateLife(_player);
            };
            player.onDeath = delegate(Player _player) {
                // print(this);
                // StartCoroutine("Destruct");
                // _player.StartCoroutine("Destruct");
                _player.doDestruct();
            };
        }

        try {
            //menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
            lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
        } catch (UnityException e) { }
    }

    IEnumerator GameEnd(int winnerNumber)
    {
        playing = false;

        // Display victory screen
        print("Game Ending " + winnerNumber);
        TextMesh victoryPlayerTextMesh = victoryScreen.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryScreen.transform.GetChild(2).GetComponent<TextMesh>();

        yield return new WaitForSeconds(2.5f);

        // Victory screens
        switch (winnerNumber)
        {
            case 1:
                Color _P1Color = players[0].GetComponent<SpriteRenderer>().color;
                victoryNumberTextMesh.text = "1";
                victoryNumberTextMesh.color = _P1Color;
                victoryPlayerTextMesh.color = _P1Color;
                break;
            case 2:
                Color _P2Color = players[1].GetComponent<SpriteRenderer>().color;
                victoryNumberTextMesh.text = "2";
                victoryNumberTextMesh.color = _P2Color;
                victoryPlayerTextMesh.color = _P2Color;
                break;
        }
        victoryScreen.SetActive(true);
    }

   void Update()
    {
        if (playing)
        {
            int team1DeathCount = 0;
            int team2DeathCount = 0;

            foreach (Transform child in team1.transform)
            {
                if (child.gameObject.activeSelf)
                    team1DeathCount++;
            }
            foreach (Transform child in team2.transform)
            {
                if (child.gameObject.activeSelf)
                    team2DeathCount++;
            }

            if (team1DeathCount == 0)
                GameEnd(2);
            if (team2DeathCount == 0)
                GameEnd(1);
        }
    }
}
