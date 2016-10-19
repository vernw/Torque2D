using UnityEngine;
using System.Collections;

public class OddballObjective : MonoBehaviour {
	public OddballController controller;

	private float targetMass = .01f;
	private int oddballDamage = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		Puck puck = coll.GetComponent<Puck>();
		if (puck) {
			controller.currentOddball = puck.transform.parent.GetComponent<Player>();
			Destroy(this.gameObject);
		}
	}
}
