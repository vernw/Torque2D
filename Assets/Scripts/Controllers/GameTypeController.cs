using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameTypeController : MonoBehaviour {
	public List<Player> players;
	public static GameTypeController instance;

	int maxLives = 1; // TODO: take this value from settings

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

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
	public void Initialize(MenuController.gameModeSelection curMode) {
		instance = this;
		players = new List<Player>();
		players.Add(Respawn.SpawnPlayer(Player.PLAYER.ONE, new Vector2(-4, 3), 0f, new Color(255f, 255f, 255f), maxLives));
		players.Add(Respawn.SpawnPlayer(Player.PLAYER.TWO, new Vector2(4, 3), 180f, new Color(255f, 255f, 255f), maxLives));
		players.Add(Respawn.SpawnPlayer(Player.PLAYER.THREE, new Vector2(-4, -3), 0f, new Color(255f, 255f, 255f), maxLives));
		players.Add(Respawn.SpawnPlayer(Player.PLAYER.FOUR, new Vector2(4, -3), 180f, new Color(255f, 255f, 255f), maxLives));

		Camera.main.GetComponent<GameCamera>().players = players;
		CustomInitialize (curMode);
	}

	protected abstract void CustomInitialize (MenuController.gameModeSelection curMode);

	// pass name of text file in Resources/LevelDefs/
	void loadLevelDef(string name) {
		string path = "LevelDefs/" + name;
		TextAsset textAss = Resources.Load (path) as TextAsset;
		LevelDef def = JsonUtility.FromJson<LevelDef> (textAss.text) as LevelDef;
	}
}
