using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Game Controller that manages all the game setup data, including player count, lives, countdown freezing, etc.
 * Contains gameEnd(), which is activated when only one player is left alive.
 * Instantiates a victory screen whose script can be located in VictoryScreen.cs.
*/

public class SoccerController : MonoBehaviour
{
    public static SoccerController instance = null;

    public MenuController menuController;
    public LifeOverlay lifeOverlay;

    public GameObject team1;
    public GameObject team2;
    public GameObject goal1;
    public GameObject goal2;
    public GameObject ball;

    public int targetScore = 5;
    public bool playing = false;
    public bool countdown = false;
    public GameObject victoryScreen;

    private int _team1Score = 0;
    public int team1Score
    {
        get { return _team1Score; }
        set
        {
            _team1Score = value;
            if (value == targetScore)
            {
                StartCoroutine(Victory.EndGame(team1, team1.GetComponentInChildren<SpriteRenderer>().color));
                playing = false;
            }
            else
            {
                CleanGame();
                LoadGame();
            }
        }
    }

    private int _team2Score = 0;
    public int team2Score
    {
        get { return _team2Score; }
        set
        {
            _team2Score = value;
            if (value == targetScore)
            {
                StartCoroutine(Victory.EndGame(team2, team2.GetComponentInChildren<SpriteRenderer>().color));
                playing = false;
            }
            else
            {
                CleanGame();
                LoadGame();
            }
        }
    }

    List<Player> players;

    void Awake()
    {
        // Ensures singleton status of the controller
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Maintains persistence through loading scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadGame();

        try
        {
            //menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
            lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
        }
        catch (UnityException e) { }
    }

    // Removes all assets for clean slate to reload
    void CleanGame()
    {
        Destroy(team1);
        Destroy(team2);
        Destroy(goal1);
        Destroy(goal2);
        Destroy(ball);

        playing = false;
        players = null;
    }

    // Setup for game start
    void LoadGame()
    {
        team1 = (GameObject)Instantiate(Resources.Load("Prefabs/Team1", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
        team2 = (GameObject)Instantiate(Resources.Load("Prefabs/Team2", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
        goal1 = (GameObject)Instantiate(Resources.Load("Prefabs/Goal1", typeof(GameObject)), new Vector2(-11.5f, 0), Quaternion.identity);
        goal2 = (GameObject)Instantiate(Resources.Load("Prefabs/Goal2", typeof(GameObject)), new Vector2(11.5f, 0), Quaternion.identity);
        ball = (GameObject)Instantiate(Resources.Load("Prefabs/SoccerBall", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);

        // TODO: Select teams here with foreach when selection through menu is in
        players = new List<Player>();
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), unchecked((int)Mathf.Infinity), team1));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), unchecked((int)Mathf.Infinity), team2));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), unchecked((int)Mathf.Infinity), team1));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), unchecked((int)Mathf.Infinity), team2));

        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
        lifeOverlay.CustomStart(players, targetScore, true);

        Camera.main.GetComponent<GameCamera>().players = players;
    }
}
