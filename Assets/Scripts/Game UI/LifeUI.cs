using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : GenericUI {

	public override void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs, int maxLives) {
		base.Initialize (playerDefs);
		foreach (KeyValuePair<Util.PLAYER, Corner> corner in corners) {
			corner.Value.g = new LifeValues();
		}
//		corners = new Dictionary<Util.PLAYER, LifeCorner> ();
	}
	
	// Update is called once per frame
	void Update () {
		customUpdate ();

	}

//	protected class LifeCorner : Corner {
//
//	}

	protected class LifeValues : Generic {
		public int curLives;
		public int goalLives;
	}
}
