using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KOTHController : GameTypeController {
//	public static KOTHController instance = null;
	public Player king;
//	public List<Player> players;
	
	Dictionary<Player, int> scores;
	float scoreInterval = 1f;
	int scoreIncrement = 1;
	int scoreToWin = 5;
//	int maxLives = 4;

	// Use this for initialization
//	void Start () {
//		superInitialize(maxLives);
//
//        scores = new Dictionary<Player, int>();
//
//        foreach(Player player in players) {
//        	scores.Add(player, 0);
//			player.onDamage = delegate(Player _player) {
//            };
//            player.onDeath = delegate(Player _player) {
//                _player.doDestruct();
//            };
//        }
//
//        StartCoroutine(ManageScores());
//	}

//    void Awake()
//    {
//        if (instance == null) {
//            instance = this;
//        }
//        else if (instance != this) {
//            Destroy(gameObject);
//        }
//    }
	
	// Update is called once per frame
//	void Update () {
//	
//	}

	protected override void CustomInitialize (MenuController.gameModeSelection curMode)
	{
//		instance = this;
		scores = new Dictionary<Player, int>();
		foreach(Player player in players) {
			scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {
			};
			player.onDeath = delegate(Player _player) {
				_player.doDestruct();
			};
		}
		StartCoroutine(ManageScores());
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
