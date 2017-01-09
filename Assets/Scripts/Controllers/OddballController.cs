using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OddballController : GameTypeController {
	public Player currentOddball;
	public Wall testWallA;
	public Wall testWallB;

	int maxLives = 1;
	int scoreToWin = 30;
	LifeOverlay lifeOverlay;
	Dictionary<Player, int> scores;
	float timeToScore = 1f; // 1 second per point
	float respawnTime = 4f;

	// Use this for initialization
	void Start () {
		superInitialize(maxLives);

        scores = new Dictionary<Player, int>();
        foreach(Player player in players) {
        	scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {};
            player.onDeath = delegate(Player _player) {
				StartCoroutine(ManageRespawn(_player));
            };
        }
        GameObject oddballObjectiveGO = (GameObject)(Resources.Load("Prefabs/Game Mode Controllers and Assets/OddballObjective", typeof(GameObject)));
        OddballObjective oddballObjective = ((GameObject)Instantiate(oddballObjectiveGO, Vector3.zero, Quaternion.identity)).GetComponent<OddballObjective>();
        oddballObjective.controller = this;
        StartCoroutine(CalculateScore());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			Wave newWave = gameObject.AddComponent<Wave> ();
			newWave.Initialize(testWallA, new Vector2(-1, 0));
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			Wave newWave = gameObject.AddComponent<Wave> ();
			newWave.Initialize(testWallB, new Vector2(1, 0));
		}
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
		player.doDestruct ();
		yield return new WaitForSeconds (respawnTime);
		Respawn.RevivePlayer (player);
	}
}
