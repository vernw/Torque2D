using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {
	Wall loc;
	Vector2 dir;
	float moveDelay = 0.1f;
	float maxOpacity = 0.5f;
	int propagateDistance = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Wall _loc, Vector2 _dir, float _maxOpacity = -1f, int _propagateDistance = -1) {
		loc = _loc;
		dir = _dir;
		if (_maxOpacity >= 0f) {
			maxOpacity = _maxOpacity;
		}
		if (_propagateDistance > 0) {
			propagateDistance = _propagateDistance;
		}
		StartCoroutine (Routine ());
	}

	IEnumerator Routine() {
		while (true) {
			loc.DoPulse (maxOpacity);
			if (propagateDistance > 0) {
				propagateDistance--;
				if (propagateDistance == 0) {
					Destroy (this);
					yield return false;
				}
			}
			yield return new WaitForSeconds (moveDelay);
			Wall next = loc.neighbors [dir];
			if (!next) {
				Vector2 dirA = (dir.x == 0) ? new Vector2 (1, 0) : new Vector2 (0, 1);
				Vector2 dirB = (dir.x == 0) ? new Vector2 (-1, 0) : new Vector2 (0, -1);
				next = loc.neighbors [dirA];
				dir = dirA;
				Wall propLoc = loc.neighbors [dirB];
				if (propLoc) {
					Wave propagation = gameObject.AddComponent<Wave> ();
					propagation.Initialize (propLoc, dirB, maxOpacity, propagateDistance);
				}
				if (!next) {
					Destroy (this);
					yield return false;
				}
			}
			loc = next;
		}
	}
}
