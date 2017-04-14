using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
	public enum PLAYER { ONE, TWO, THREE, FOUR };
	public enum COLOR { BLUE, RED, YELLOW, GREEN };

	public static PLAYER IteratePlayer (PLAYER player) {
		switch (player) {
		case PLAYER.ONE:
			return PLAYER.TWO;
		case PLAYER.TWO:
			return PLAYER.THREE;
		case PLAYER.THREE:
			return PLAYER.FOUR;
		}
		return PLAYER.ONE;
	}

	public static Color ConvertColor (COLOR color) {
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
		return Color.black;
	}

	public static int PlayerToInt (PLAYER player) {
		switch (player) {
		case PLAYER.ONE:
			return 0;
		case PLAYER.TWO:
			return 1;
		case PLAYER.THREE:
			return 2;
		case PLAYER.FOUR:
			return 3;
		}
		return -1;
	}
}
