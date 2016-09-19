using UnityEngine;
using System.Collections;

public class DepthCharge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Avatar avatar = coll.collider.GetComponent<Avatar>();
		if (avatar) {
			avatar.TakeDamage(1);
		}
	}
}
