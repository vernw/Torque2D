using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// print(col.name);
		Avatar avatar = col.GetComponent<Avatar>();
		if (avatar) {
			// print("avatar");
			avatar.invincible = true;
		}
		if (false) {
			// print("puck");

		}
	}
}
