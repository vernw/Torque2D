using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
	public enum PLAYER { ONE, TWO, THREE, FOUR };
	public enum COLOR { BLUE, RED, YELLOW, GREEN };

	public static PLAYER iteratePlayer (PLAYER player) {
		switch (player) {
		case PLAYER.ONE:
			return PLAYER.TWO;
		case PLAYER.TWO:
			return PLAYER.THREE;
		case PLAYER.THREE:
			return PLAYER.FOUR;
		}
		Debug.Log ("ERROR: bad iterate player");
		return PLAYER.ONE;
	}

	public static Color convertColor (COLOR color) {
		switch (color) {
		case COLOR.BLUE:
			return Color.blue;
		case COLOR.RED:
			return Color.red;
		case COLOR.YELLOW:
			return Color.yellow;
		case COLOR.GREEN:
			return Color.green;
		}
		Debug.Log ("ERROR: bad convert color");
		return Color.black;
	}
}
