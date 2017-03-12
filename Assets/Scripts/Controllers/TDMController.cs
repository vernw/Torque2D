using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Game Controller that manages all the game setup data, including player count, lives, countdown freezing, etc.
 * Contains gameEnd(), which is activated when only one player is left alive.
 * Instantiates a victory screen whose script can be located in VictoryScreen.cs.
*/

public class TDMController : GameTypeController {

//    public static TDMController instance = null;

    public MenuController menuController;
    public LifeOverlay lifeOverlay;

//    public GameObject team1;
//    public GameObject team2;

//    public int maxLives = 5;
    public bool playing = false;
    public bool countdown = false;
    public GameObject victoryScreen;

//    List<Player> players;

//    void Awake()
//    {
//        if (instance == null)
//            instance = this;
//        else if (instance != this)
//            Destroy(gameObject);
//    }

//    void Start()
//    {
//        team1 = (GameObject)Instantiate(Resources.Load("Prefabs/Team1", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
//        team2 = (GameObject)Instantiate(Resources.Load("Prefabs/Team2", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
//        
//		superInitialize(maxLives);
//
//        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
//        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
//        lifeOverlay.CustomStart(players, maxLives);
//
//        foreach(Player player in players) {
//            player.onDamage = delegate(Player _player) {
//                lifeOverlay.UpdateLife(_player);
//            };
//            player.onDeath = delegate(Player _player) {
//                _player.doDestruct();
//            };
//        }
//
//        try {
//            //menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
//            lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
//        } catch (UnityException e) { }
//    }

	protected override void CustomInitialize ()
	{
//		instance = this;
//		team1 = (GameObject)Instantiate(Resources.Load("Prefabs/Team1", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
//		team2 = (GameObject)Instantiate(Resources.Load("Prefabs/Team2", typeof(GameObject)), new Vector2(0, 0), Quaternion.identity);
		GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
		lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
		lifeOverlay.CustomStart(players, 1);
		foreach(Player player in players) {
			player.onDamage = delegate(Player _player) {
				lifeOverlay.UpdateLife(_player);
			};
			player.onDeath = delegate(Player _player) {
				_player.doDestruct();
			};
		}
		try {
			lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
		} catch (UnityException e) { }
	}

    bool LoseCheck(GameObject team)
    {
        bool lCheck = true;
        foreach (Transform child in team.transform)
        {
            if (child.GetComponent<Player>().lives > 0)
                lCheck = false;
        }
        return lCheck;
    }

   void Update()
    {
//        if (playing)
//        {
//            if (LoseCheck(team1))
//            {
//                StartCoroutine(Victory.EndGame(team2, team2.GetComponentInChildren<SpriteRenderer>().color));
//                playing = false;
//            }
//            else if (LoseCheck(team2))
//            {
//                StartCoroutine(Victory.EndGame(team1, team1.GetComponentInChildren<SpriteRenderer>().color));
//                playing = false;
//            }
//        }
		if (Input.GetKeyDown (KeyCode.K)) { // test end
			players[1].avatar.TakeDamage(1);
			players[2].avatar.TakeDamage(1);
			players[3].avatar.TakeDamage(1);
			EndCard ();
		}
    }
}
