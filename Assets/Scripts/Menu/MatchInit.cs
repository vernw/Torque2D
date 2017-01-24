using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchInit : MonoBehaviour {
	public MenuController.gameModeSelection curMode;

	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded() {
		switch (curMode) {
		case MenuController.gameModeSelection.Headhunter:
			(new GameObject ()).AddComponent<HeadhunterController> ().Initialize(curMode);
			break;
		case MenuController.gameModeSelection.King:
			(new GameObject ()).AddComponent<KOTHController> ().Initialize(curMode);
			break;
		case MenuController.gameModeSelection.Oddball:
			(new GameObject ()).AddComponent<OddballController> ().Initialize(curMode);
			break;
		case MenuController.gameModeSelection.Soccer:
			(new GameObject ()).AddComponent<SoccerController> ().Initialize(curMode);
			break;
		case MenuController.gameModeSelection.Standard:
			(new GameObject ()).AddComponent<TDMController> ().Initialize(curMode);
			break;
		}
	}

	public void Initialize(MenuController.gameModeSelection _curMode) {
		curMode = _curMode;
	}
}
