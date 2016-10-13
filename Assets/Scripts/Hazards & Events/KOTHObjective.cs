using UnityEngine;
using System.Collections;

public class KOTHObjective : MonoBehaviour {
	KOTHController controller;
	Player occupant;
	float pulseForce = 5000f;
	float pulseMaxDistance = 3.5f;

	// Use this for initialization
	void Start () {
		controller = KOTHController.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Avatar avatar = coll.GetComponent<Avatar>();
		// print(avatar);
		if (avatar) {
			if (occupant != null) {
				occupant = avatar.player;
				Pulse();
			}
			controller.king = occupant = avatar.player;
		}
		// print(controller);
	}

	private void Pulse() {
//		print(occupant.playerType);
		foreach(Player player in controller.players) {
			Avatar avatar = player.avatar;
			Vector2 diff = transform.position - avatar.transform.position;
			// print(player);
			// print(diff + " : " + diff.magnitude);
			if (player == occupant || diff.magnitude > pulseMaxDistance) {
				continue;
			}
			// print("pulse");
			diff.Normalize();
			diff *= -1f;
			diff *= pulseForce;
			print(diff);
			avatar.GetComponent<Rigidbody2D>().AddForce(diff);
		}
	}
}
