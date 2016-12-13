using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadhunterController : GameTypeController {
	int maxLives = 1;
	int scoreToWin = 1;
	Dictionary<Player, int> score;

	// Use this for initialization
	void Start () {
		superInitialize (maxLives);
		score = new Dictionary<Player, int> ();
		foreach (Player player in players) {
			score.Add (player, 0);
			player.onDamage = delegate(Player _player) {
			};
			player.onDeath = delegate(Player _player) {
				_player.doDestruct();
				ManageVictoryCondition();
			};
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ManageVictoryCondition() {
		Player remainingPlayer = null;
		foreach (Player player in players) {
			if (player.lives > 0) {
				if (remainingPlayer == null) {
					remainingPlayer = player;
				} else {
					return;
				}
			}
		}
		score [remainingPlayer]++;
		if (score [remainingPlayer] >= scoreToWin) {
			// win
		}
	}
}
