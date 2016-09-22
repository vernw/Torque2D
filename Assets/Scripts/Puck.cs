using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	// public Avatar myAvatar;

	private float powerUpDuration = 5f;

	private int _damage = 1;
	public int damage {
        get { return _damage; }
        set {
            _damage = value;
            StartCoroutine(powerUp());
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Avatar avatar = coll.collider.GetComponent<Avatar>();
		// if (avatar && avatar != myAvatar) {
		if (avatar) {
			avatar.TakeDamage(damage);
		}
	}

	IEnumerator powerUp() {
		//TODO: particle effect on
		yield return new WaitForSeconds(powerUpDuration);
		//TODO: particle effect off
		damage = 1;
	}
}
