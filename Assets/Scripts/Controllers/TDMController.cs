using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Game Controller that manages all the game setup data, including player count, lives, countdown freezing, etc.
 * Contains gameEnd(), which is activated when only one player is left alive.
 * Instantiates a victory screen whose script can be located in VictoryScreen.cs.
*/

public class TDMController : GameTypeController {

    public MenuController menuController;
//    public LifeOverlay lifeOverlay;
    public bool playing = false;
    public bool countdown = false;
    public GameObject victoryScreen;

	LifeUI ui;

	protected override void CustomInitialize ()
	{
		//GameObject lifeOverlayGO = (GameObject)Instantiate(Resources.Load("Prefabs/LifeOverlay", typeof(GameObject)));
		//lifeOverlay = lifeOverlayGO.GetComponent<LifeOverlay>();
		//lifeOverlay.CustomStart(players, 1);
		foreach(Player player in players) {
			player.onDamage = delegate(Player _player) {
//				lifeOverlay.UpdateLife(_player);
				ui.UpdateLives(_player);
			};
			player.onDeath = delegate(Player _player) {
				_player.doDestruct();
			};
		}
//		ui = new UIController ();
		ui = gameObject.AddComponent<LifeUI>();
		ui.Initialize (playerDefs, maxLives);
		//try {
		//	lifeOverlay = (FindObjectsOfType(typeof(LifeOverlay)) as LifeOverlay[])[0];
		//} catch (UnityException e) { }
	}

    bool LoseCheck(GameObject team)
    {
        bool lCheck = true;
        foreach (Transform child in team.transform)
        {
            if (child.GetComponent<Player>().lives > 0)
                lCheck = false;
        }
        return lCheck;
    }

   //void Update()
    //{
		//if (Input.GetKeyDown (KeyCode.K)) { // test end
		//	players[1].avatar.TakeDamage(1);
		//	players[2].avatar.TakeDamage(1);
		//	players[3].avatar.TakeDamage(1);
		//	EndCard ();
		//}
    //}
}
