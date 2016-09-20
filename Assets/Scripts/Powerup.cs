using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		Avatar avatar = coll.GetComponent<Avatar>();
		Puck puck = coll.GetComponent<Puck>();
		if (avatar) {
			avatar.invincible = true;
		} else if (puck) {
			puck.damage = 2;
		}
		Destroy(this.gameObject);
	}
}
