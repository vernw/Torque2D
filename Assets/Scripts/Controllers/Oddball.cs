using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Oddball : MonoBehaviour {
	List<Player> players;
	int maxLives = 4;
	LifeOverlay lifeOverlay;
	Dictionary<Player, int> scores;

	// Use this for initialization
	void Start () {
		players = new List<Player>();
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
        players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));

        GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
        lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
        lifeOverlay.CustomStart(players, maxLives);

        Camera.main.GetComponent<GameCamera>().players = players;

        scores = new Dictionary<Player, int>();
        foreach(Player player in players) {
        	scores.Add(player, 0);
			player.onDamage = delegate(Player _player) {
                this.lifeOverlay.UpdateLife(_player);
            };
            player.onDeath = delegate(Player _player) {
                _player.doDestruct();
            };
        }
        GameObject oddballObjectiveGO = (GameObject)(Resources.Load("Prefabs/OddballObjective", typeof(GameObject)));
        Instantiate(oddballObjectiveGO, Vector3.zero, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
