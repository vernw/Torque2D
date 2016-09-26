using UnityEngine;
using System.Collections;

public class Puck : MonoBehaviour {
	public Avatar myAvatar;

	private float powerUpDuration = 5f;
	private Rigidbody2D rb;
	private float startMass;

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
		rb = GetComponent<Rigidbody2D>();
		startMass = rb.mass;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Avatar avatar = coll.collider.GetComponent<Avatar>();
        // print("Collision hit from: " + myAvatar + " on " + avatar);

        if(avatar && avatar != myAvatar)
        {
            Transform collTeam = avatar.transform.parent.parent;
            Transform myTeam = myAvatar.transform.parent.parent;

            // Team check if present, else damage as normal
            if (myTeam != null && collTeam != myTeam)
            {
                avatar.TakeDamage(damage);
            }
            else if (myTeam == null)
            {
                avatar.TakeDamage(damage);
            }
        }
	}

	IEnumerator powerUp() {
		//TODO: particle effect on
		yield return new WaitForSeconds(powerUpDuration);
		//TODO: particle effect off
		damage = 1;
	}
}
