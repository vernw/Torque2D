﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public PLAYER playerType;
	public int lives;
	public delegate void OnDamage(Player _player);
	public OnDamage onDamage;
	public delegate void OnDeath(Player _player);
	public OnDeath onDeath;
	public Avatar avatar;

	GameObject explosion;
	Dictionary<GameObject, Vector3> localStarts;
	FadeIn fadeIn;

	void Start () {
		fadeIn = GetComponent<FadeIn> ();
		explosion = (GameObject)Resources.Load("Prefabs/Explosion", typeof(GameObject));
		WallController wallController = GameObject.FindObjectsOfType<WallController>()[0].GetComponent<WallController>();
		localStarts = new Dictionary<GameObject, Vector3> ();
		foreach(Transform child in transform) {
			Avatar _avatar = child.gameObject.GetComponent<Avatar>();
			if (!_avatar) {
				wallController.IgnoreCollisions(child.GetComponent<CircleCollider2D>());
			} else {
				avatar = _avatar;
				avatar.player = this;
			}
			localStarts [child.gameObject] = child.localPosition;
		}
	}

	public void DoDestruct() {
		StartCoroutine(Destruct());
	}

	public void Disable() {
		foreach (Transform child in transform) {
			child.gameObject.gameObject.SetActive (false);
			//child.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	public void Enable() {
		foreach (Transform child in transform) {
			child.gameObject.gameObject.SetActive (true);
		}
		avatar.invincible = false;
		fadeIn.DoFadeIn ();
	}

	public void Reset() {
		print (transform.position);
		foreach (Transform child in transform) {
			print ("===================================");
			print (localStarts [child.gameObject]);
			child.localPosition = localStarts [child.gameObject];
			print (child.localPosition);
			print (child.position);
		}
	}

	public IEnumerator Destruct()
    {
        // Sequentially destructs all components of a player
        yield return new WaitForSeconds(0.2f);
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            GameObject explode = Instantiate(explosion, child.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.5f);
            Destroy(explode);
        }
        //gameObject.SetActive(false);
    }

    public enum PLAYER {
        ONE,
        TWO,
        THREE,
        FOUR
    }
}
