using UnityEngine;
using System.Collections;

public class Wormhole : MonoBehaviour {
	public Wormhole partner;

	private float arrivalDistance = .5f;
	private float force = 1500f;

	// private Avatar target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Avatar avatar = coll.GetComponent<Avatar>();
		if (avatar && !avatar.transporter) {
			// target = avatar;
			StartCoroutine(Act(avatar));
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		Avatar avatar = coll.GetComponent<Avatar>();
		if (avatar && avatar.transporter == this) {
			avatar.transporter = null;
		}
	}

	IEnumerator Act (Avatar target) {
		target.transporter = partner;
		target.controlDisabled = true;
		Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
		Vector2 exitVelocity = rb.velocity;
		rb.velocity = Vector2.zero;
		while (Vector3.Distance(partner.transform.position, target.transform.position) > arrivalDistance) {
			Vector2 dir = (partner.transform.position - target.transform.position);
			dir.Normalize();
			rb.velocity = dir * force * Time.deltaTime;
			// print(Vector3.Distance(partner.transform.position, target.transform.position));
			yield return new WaitForEndOfFrame();
		}
		rb.velocity = exitVelocity;
		target.controlDisabled = false;
	}
}
