using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameTypeController : MonoBehaviour {
	public static GameTypeController instance;
	public Dictionary<Util.PLAYER, SpawnPoint> spawnPoints;
	public List<Player> players;

	protected Dictionary<Util.PLAYER, Util.COLOR> playerDefs;
	protected int maxLives = 3; // TODO: take this value from settings

	public void Initialize(Dictionary<Util.PLAYER, Util.COLOR> _playerDefs) {
		gameObject.name = "GameController";
		SpawnPoint[] points = GameObject.FindObjectsOfType<SpawnPoint> ();
		spawnPoints = new Dictionary<Util.PLAYER, SpawnPoint> ();
		Util.PLAYER pItor = Util.PLAYER.ONE;
		foreach (SpawnPoint point in points) {
			spawnPoints [pItor] = point;
			pItor = Util.IteratePlayer (pItor);
		}
		instance = this;
		players = new List<Player>();
		playerDefs = _playerDefs;
		foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerDefs) {
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

	public void EndCard() {
		GameObject go = (GameObject)Instantiate (Resources.Load ("Prefabs/EndCard"));
		EndCard ec = go.GetComponent<EndCard> ();
		go.transform.parent = Camera.main.transform;
		Camera.main.GetComponent<GameCamera> ().EndCardZoom ();
	}
}
