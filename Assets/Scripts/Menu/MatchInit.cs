using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInit : MonoBehaviour {
	public MenuController.gameModeSelection curMode;
	public bool doTest = false;
	public Util.COLOR[] playerColors;
	Dictionary<Util.PLAYER, Util.COLOR> players;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		if (doTest) {
			players = new Dictionary<Util.PLAYER, Util.COLOR> ();
			if (playerColors.Length < 4) {
				players [Util.PLAYER.ONE] = Util.COLOR.BLUE;
				players [Util.PLAYER.TWO] = Util.COLOR.RED;
				players [Util.PLAYER.THREE] = Util.COLOR.YELLOW;
				players [Util.PLAYER.FOUR] = Util.COLOR.GREEN;
			} else {
				players [Util.PLAYER.ONE] = playerColors[0];
				players [Util.PLAYER.TWO] = playerColors[1];
				players [Util.PLAYER.THREE] = playerColors[2];
				players [Util.PLAYER.FOUR] = playerColors[3];
			}
			OnLevelWasLoaded ();
			//(new GameObject ()).AddComponent<TDMController> ().Initialize(players);
		}
	}

	void OnLevelWasLoaded() {
		switch (curMode) {
		case MenuController.gameModeSelection.Headhunter:
			(new GameObject ()).AddComponent<HeadhunterController> ().Initialize(players);
			break;
		case MenuController.gameModeSelection.King:
			(new GameObject ()).AddComponent<KOTHController> ().Initialize(players);
			break;
		case MenuController.gameModeSelection.Oddball:
			(new GameObject ()).AddComponent<OddballController> ().Initialize(players);
			break;
		case MenuController.gameModeSelection.Soccer:
			(new GameObject ()).AddComponent<SoccerController> ().Initialize(players);
			break;
		case MenuController.gameModeSelection.Standard:
			(new GameObject ()).AddComponent<TDMController> ().Initialize(players);
			break;
		}
	}

	public void Initialize(MenuController.gameModeSelection _curMode, Dictionary<Util.PLAYER, Util.COLOR> _players) {
		curMode = _curMode;
		players = _players;
	}
}
