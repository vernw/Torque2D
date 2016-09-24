﻿using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	public Avatar myAvatar;

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
		foreach(Transform child in transform.parent) {
			Avatar avatar = child.GetComponent<Avatar>();
			if (avatar) {
				myAvatar = avatar;
				break;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Avatar avatar = coll.collider.GetComponent<Avatar>();
		if (avatar && avatar != myAvatar && avatar.transform.parent.parent != myAvatar.transform.parent.parent) {
			avatar.TakeDamage(damage);
		}
	}

    public void TDM()
    {

    }

	public void oddball() {
		
	}

	IEnumerator powerUp() {
		//TODO: particle effect on
		yield return new WaitForSeconds(powerUpDuration);
		//TODO: particle effect off
		damage = 1;
	}
}
