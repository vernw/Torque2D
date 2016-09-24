using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	public static Player SpawnPlayer(Player.PLAYER playerType, Vector2 location, float rotation, Color color, int lives, GameObject team = default(GameObject))
    {
		GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/Player", typeof(GameObject));
		// print(playerPrefab);
		GameObject playerGO = (GameObject)Instantiate(playerPrefab, location, Quaternion.Euler(0, 0, rotation));

		Player player = playerGO.GetComponent<Player>();
		player.playerType = playerType;
		player.lives = lives;
        foreach (Transform child in playerGO.transform) {
			SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
			if (sr) {
				sr.color = color;
			}
		}

        if (team != null)
        {
            player.transform.SetParent(team.transform);
        }

        return player;
	}
}
