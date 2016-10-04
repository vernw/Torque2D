﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public PLAYER playerType;
	public int lives;
	public delegate void OnDamage(Player _player);
	public OnDamage onDamage;
	public delegate void OnDeath(Player _player);
	public OnDeath onDeath;
	public Avatar avatar;

	private GameObject explosion;

	void Start () {
		explosion = (GameObject)Resources.Load("Prefabs/Explosion", typeof(GameObject));
		WallController wallController = GameObject.FindObjectsOfType<WallController>()[0].GetComponent<WallController>();
		foreach(Transform child in transform) {
			Avatar _avatar = child.gameObject.GetComponent<Avatar>();
			if (!_avatar) {
				wallController.IgnoreCollisions(child.GetComponent<CircleCollider2D>());
			} else {
				avatar = _avatar;
				avatar.player = this;
			}
		}
	}

	public void doDestruct() {
		StartCoroutine(Destruct());
	}

	public IEnumerator Destruct()
    {
        // Sequentially destructs all components of a player
        yield return new WaitForSeconds(0.2f);
        // for (int i = transform.parent.transform.childCount - 1; i > 0; i--)
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            GameObject explode = Instantiate(explosion, child.position, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(0.5f);
            Destroy(explode);
        }
        // StartCoroutine(Explode());
        gameObject.SetActive(false);
    }

    public enum PLAYER {
        ONE,
        TWO,
        THREE,
        FOUR
    }
}