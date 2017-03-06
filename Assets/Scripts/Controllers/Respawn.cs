using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
//	public static Player SpawnPlayer(Util.PLAYER playerType, Vector2 location, float rotation, Color color, int lives = default(int), GameObject team = default(GameObject))
//    {
//		GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/Player", typeof(GameObject));
//		// print(playerPrefab);
//		GameObject playerGO = (GameObject)Instantiate(playerPrefab, location, Quaternion.Euler(0, 0, rotation));
//
//		Player player = playerGO.GetComponent<Player>();
//		player.playerType = playerType;
//		player.lives = lives;
//        foreach (Transform child in playerGO.transform) {
//			SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
//			if (sr) {
//				sr.color = color;
//			}
//		}
//
//        return player;
//	}

	public static Player SpawnPlayer(Util.PLAYER playerType, Util.COLOR color, SpawnPoint point, int lives = default(int))
	{
		GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/Player", typeof(GameObject));
		GameObject playerGO = (GameObject)Instantiate(playerPrefab, point.location, Quaternion.Euler(0, 0, point.rotation));
		Player player = playerGO.GetComponent<Player>();
		player.playerType = playerType;
		player.color = color;
		player.lives = lives;
		foreach (Transform child in playerGO.transform) {
			SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
			if (sr) {
				sr.color = Util.convertColor(color);
			}
		}
		return player;
	}

	public static void RevivePlayer(Player player) {
		player.reset ();
		player.enable ();
	}
}
