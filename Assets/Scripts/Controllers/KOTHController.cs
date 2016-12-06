using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KOTHController : GameTypeController {
	public static KOTHController instance = null;
	public Player king;
//	public List<Player> players;
	
	Dictionary<Player, int> scores;
	float scoreInterval = 1f;
	int scoreIncrement = 1;
	int scoreToWin = 5;
	int maxLives = 4;

	// Use this for initialization
	void Start () {
//		players = new List<Player>();
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));
//
		superInitialize(maxLives);

        scores = new Dictionary<Player, int>();

        foreach(Player player in players) {
        	scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {
                // this.lifeOverlay.UpdateLife(_player);
            };
            player.onDeath = delegate(Player _player) {
                _player.doDestruct();
            };
        }

//        Camera.main.GetComponent<GameCamera>().players = players;

        StartCoroutine(ManageScores());
	}

    void Awake()
    {
        // Ensures singleton status of GameController
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator ManageScores() {
		while(true) {
			if (king != null) {
				scores[king] += scoreIncrement;
				if (scores[king] >= scoreToWin) {
					//handle victory
				}
				yield return new WaitForSeconds(scoreInterval);
			} else {
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
