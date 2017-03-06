using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInit : MonoBehaviour {
	MenuController.gameModeSelection curMode;
	Dictionary<Util.PLAYER, Util.COLOR> players;

	void Awake () {
		DontDestroyOnLoad (gameObject);
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
