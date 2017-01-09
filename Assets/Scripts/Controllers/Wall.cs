using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Wall : MonoBehaviour {
	public Dictionary<Vector2, Wall> neighbors;
//	public Vector2 waveDir = Vector2.zero;
//	public bool debug;

    float fadeTime = .5f;
    float width;
//    float propagateDelay = .1f;
//	float waveDelay = .1f;
	float defaultFade = 0.2f;
	float collisionOpacity = 1f;
//	float fadeTime = 0.5f;
//	float waveOpacity = 0.5f;
//	float waveFadeTime = 0.5f;
//	float colOpacity = 1f;
//	float colFadeTime = 0.5f;
    int propagateDistance = 4;
//    List<Wall> neighbors_;
	SpriteRenderer sr;

    void Start()
    {
//    	neighbors = new List<Wall>();
		neighbors = new Dictionary<Vector2, Wall>();
        DOTween.Init();
		sr = gameObject.GetComponent<SpriteRenderer>();
		Color startColor = sr.color;
		startColor.a = defaultFade;
		sr.color = startColor;
        width = GetComponent<SpriteRenderer>().bounds.extents.x * 2;
//		neighbors.Add(getWall(Vector2.up));
//		neighbors.Add(getWall(Vector2.right));
//		neighbors.Add(getWall(Vector2.down));
//		neighbors.Add(getWall(Vector2.left));
		neighbors[Vector2.up] = getWall(Vector2.up);
		neighbors[Vector2.right] = getWall(Vector2.right);
		neighbors[Vector2.down] = getWall(Vector2.down);
		neighbors[Vector2.left] = getWall(Vector2.left);
//		if (waveDir != Vector2.zero) {
//			doWave(waveDir);
//		}
//		if (debug) {
//			foreach (Wall wall in neighbors) {
//				print (wall);
//			}
//		}
    }

    // Pulses when avatars collide with wall segments
    void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.GetComponent<Avatar>()) {
//			StartCoroutine (Pulse ());
//			List<Wall> visited = new List<Wall>();
//			visited.Add(this);
//			foreach (Wall neighbor in neighbors) {
//				if (neighbor != null) {
//					neighbor.doPulse (visited, propagateDistance);
//				}
//			}
//			StartCoroutine(ColPulse());
//			DoPulse(collisionOpacity);
			foreach (KeyValuePair<Vector2, Wall> pair in neighbors) {
				if (pair.Value) {
					Wave generated = GameTypeController.instance.gameObject.AddComponent<Wave> ();
					generated.Initialize (this, pair.Key, collisionOpacity, propagateDistance);
				}
			}
		}
    }

//	public void doWave(Vector2 _waveDir) {
//		waveDir = _waveDir;
//		StartCoroutine (Wave ());
//	}

	public void DoPulse(float max) {
		StartCoroutine (Pulse (max));
	}

//	public void DoWavePulse() {
//		StartCoroutine (WavePulse ());
//	}

    Wall getWall (Vector2 dir)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, dir, width);
		foreach (RaycastHit2D hit in hits) {
			Wall hitWall = hit.collider.GetComponent<Wall> ();
			if (hitWall && hitWall != this) {
				return hitWall;
			}
		}
		return null;
	}

//	void doPulse (List<Wall> visited, int propagate)
//	{
//		visited.Add(this);
//		StartCoroutine (Pulse ());
//		if (propagate > 0) {
//			StartCoroutine(DelayedPropagate(visited, propagate - 1));
//		}
//	}

//	IEnumerator DelayedPropagate (List<Wall> visited, int propagate)
//	{
//		yield return new WaitForSeconds(propagateDelay);
//		foreach (Wall neighbor in neighbors) {
//			if (neighbor != null && !visited.Contains(neighbor)) {
//				neighbor.doPulse(visited, propagate);
//			}
//		}
//	}

//    IEnumerator Pulse()
//    {
//        yield return GetComponent<SpriteRenderer>().DOFade(1, fadeTime).WaitForCompletion();
//        yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, fadeTime).WaitForCompletion();
//    }

//	IEnumerator WavePulse() {
//		yield return GetComponent<SpriteRenderer>().DOFade(waveOpacity, waveFadeTime).WaitForCompletion();
//		yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, waveFadeTime).WaitForCompletion();
//	}
//
//	IEnumerator ColPulse() {
//		yield return GetComponent<SpriteRenderer>().DOFade(colOpacity, colFadeTime).WaitForCompletion();
//		yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, colFadeTime).WaitForCompletion();
//	}
//
	IEnumerator Pulse(float max) {
		yield return GetComponent<SpriteRenderer>().DOFade(max, fadeTime).WaitForCompletion();
		yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, fadeTime).WaitForCompletion();
	}

//	IEnumerator Wave() {
//		StartCoroutine(Pulse());
//		yield return new WaitForSeconds (waveDelay);
//		Wall next = getWall(waveDir);
//		if (next != null) {
//			next.doWave (waveDir);
//		} else {
//			Wall one;
//			Wall two;
//			if (waveDir.x != 0) {
//				one = getWall (Vector2.up);
//				two = getWall (Vector2.down);
//				if (one != null && two != null) {
//					if (Random.Range (0f, 1f) < 0.5f) {
//						one.doWave (Vector2.up);
//					} else {
//						two.doWave (Vector2.down);
//					}
//				} else {
//					if (one != null) {
//						one.doWave (Vector2.up);
//					} else {
//						two.doWave (Vector2.down);
//					}
//				}
//			} else {
//				one = getWall (Vector2.right);
//				two = getWall (Vector2.left);
//				if (one != null && two != null) {
//					if (Random.Range (0f, 1f) < 0.5f) {
//						one.doWave (Vector2.right);
//					} else {
//						two.doWave (Vector2.left);
//					}
//				} else {
//					if (one != null) {
//						one.doWave (Vector2.right);
//					} else {
//						two.doWave (Vector2.left);
//					}
//				}
//			}
//		}
//	}
}
