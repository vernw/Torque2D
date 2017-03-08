using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameTypeController : MonoBehaviour {
	public static GameTypeController instance;
	public Dictionary<Util.PLAYER, SpawnPoint> spawnPoints;
	public List<Player> players;

	int maxLives = 1; // TODO: take this value from settings

	//	protected void superInitialize(int maxLives) {
//		instance = this;
//		players = new List<Player>();
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));
//
//		Camera.main.GetComponent<GameCamera>().players = players;
//	}

	// TODO: place players and set color based on settings
	public void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs) {
		SpawnPoint[] points = GameObject.FindObjectsOfType<SpawnPoint> ();
		spawnPoints = new Dictionary<Util.PLAYER, SpawnPoint> ();
		Util.PLAYER pItor = Util.PLAYER.ONE;
		foreach (SpawnPoint point in points) {
			spawnPoints [pItor] = point;
			pItor = Util.iteratePlayer (pItor);
		}
		instance = this;
		players = new List<Player>();
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
//		players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));
		foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerDefs) {
//			print (player.Key);
			players.Add (Respawn.SpawnPlayer(p.Key, p.Value, spawnPoints[p.Key], maxLives));
		}
		Camera.main.GetComponent<GameCamera>().players = players;
		CustomInitialize ();
	}

	protected abstract void CustomInitialize ();

	// pass name of text file in Resources/LevelDefs/
	void loadLevelDef(string name) {
		string path = "LevelDefs/" + name;
		TextAsset textAss = Resources.Load (path) as TextAsset;
		LevelDef def = JsonUtility.FromJson<LevelDef> (textAss.text) as LevelDef;
	}
}
