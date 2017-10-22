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

    ScoreUI ui;

	protected override void CustomInitialize ()
	{
//		instance = this;
		scores = new Dictionary<Player, int>();
		foreach(Player player in players) {
			scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {
                //TODO bounce back further than normal
			};
			player.onDeath = delegate(Player _player) {};
		}
        ui = (Instantiate(Resources.Load("Prefabs/GameModes/ScoreUI") as GameObject) as GameObject).GetComponent<ScoreUI>();
        //ui = gameObject.AddComponent<ScoreUI>();
        ui.Initialize(playerDefs, scoreToWin);
		StartCoroutine(ManageScores());
	}

	IEnumerator ManageScores() {
		while(true) {
			if (king != null) {
				scores[king] += scoreIncrement;
                ui.UpdateScore(king.playerType, scores[king]);
				if (scores[king] >= scoreToWin) {
                    EndGame();
				}
				yield return new WaitForSeconds(scoreInterval);
			} else {
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
