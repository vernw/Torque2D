using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OddballController : GameTypeController {
	public Player currentOddball;

//	List<Player> players;
	int maxLives = 1;
	int scoreToWin = 30;
	LifeOverlay lifeOverlay;
	Dictionary<Player, int> scores;
	float timeToScore = 1f; // 1 second per point
	float respawnTime = 4f;

	// Use this for initialization
	void Start () {
//		players = new List<Player>();
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
//        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));

//        Camera.main.GetComponent<GameCamera>().players = players;
		superInitialize(maxLives);

        scores = new Dictionary<Player, int>();
        foreach(Player player in players) {
        	scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {};
            player.onDeath = delegate(Player _player) {
				StartCoroutine(ManageRespawn(_player));
            };
        }
        GameObject oddballObjectiveGO = (GameObject)(Resources.Load("Prefabs/OddballObjective", typeof(GameObject)));
        OddballObjective oddballObjective = ((GameObject)Instantiate(oddballObjectiveGO, Vector3.zero, Quaternion.identity)).GetComponent<OddballObjective>();
        oddballObjective.controller = this;
        StartCoroutine(CalculateScore());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator CalculateScore() {
		while (true) {
			yield return new WaitForSeconds(timeToScore);
			if (currentOddball) {
				scores[currentOddball] += 1;
				if (scores[currentOddball] > scoreToWin) {
					// TODO: winning
				}
			}
		}
	}

	IEnumerator ManageRespawn(Player player) {
		player.DoDestruct ();
		yield return new WaitForSeconds (respawnTime);
		Respawn.RevivePlayer (player);
	}
}
