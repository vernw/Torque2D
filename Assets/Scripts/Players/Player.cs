using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public Util.PLAYER playerType;
	public Util.COLOR color;
	public int lives;
	public delegate void OnDamage(Player _player);
	public OnDamage onDamage;
	public delegate void OnDeath(Player _player);
	public OnDeath onDeath;
	public Avatar avatar;
	public bool finishedInitializing;

	GameObject explosion;
	Dictionary<GameObject, Vector3> startLocations;
	Dictionary<GameObject, Quaternion> startRotations;
	FadeIn fadeIn;
	WallController wallController;

	void Start () {
		fadeIn = GetComponent<FadeIn> ();
		explosion = (GameObject)Resources.Load("Prefabs/Explosion", typeof(GameObject));
		wallController = GameObject.FindObjectsOfType<WallController>()[0].GetComponent<WallController>();
		startLocations = new Dictionary<GameObject, Vector3> ();
		startRotations = new Dictionary<GameObject, Quaternion> ();
		foreach(Transform child in transform) {
//			Avatar _avatar = child.gameObject.GetComponent<Avatar>();
//			if (!_avatar) {
//				wallController.IgnoreCollisions(child.GetComponent<CircleCollider2D>());
//			} else {
//				avatar = _avatar;
//				avatar.player = this;
//			}
			doNoCollide();
			startLocations [child.gameObject] = child.localPosition;
			startRotations [child.gameObject] = child.rotation;
		}
		switch (playerType) {
		case Util.PLAYER.ONE:
			gameObject.name = "player one";
			break;
		case Util.PLAYER.TWO:
			gameObject.name = "player two";
			break;
		case Util.PLAYER.THREE:
			gameObject.name = "player three";
			break;
		case Util.PLAYER.FOUR:
			gameObject.name = "player four";
			break;
		}
		finishedInitializing = true;
	}

	public void doDestruct() {
		StartCoroutine(Destruct());
	}

	public void disable() {
		foreach (Transform child in transform) {
			child.gameObject.gameObject.SetActive (false);
			//child.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	public void enable() {
		foreach (Transform child in transform) {
			child.gameObject.gameObject.SetActive (true);
		}
		doNoCollide ();
		avatar.invincible = false;
		fadeIn.DoFadeIn ();
	}

	public void reset() {
//		print (transform.position);
		foreach (Transform child in transform) {
//			print ("===================================");
//			print (localStarts [child.gameObject]);
			child.localPosition = startLocations [child.gameObject];
			Rigidbody2D childRB = child.GetComponent<Rigidbody2D>();
			childRB.velocity = Vector2.zero;
//			childRB.rotation = 0f;
			child.rotation = startRotations[child.gameObject];
//			print (child.localPosition);
//			print (child.position);
		}
	}

	private void doNoCollide() {
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
