using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Wall : MonoBehaviour {

    public float defaultFade;

    float fadeTime = .5f;
    float width;
    float propagateDelay = .1f;
    int propagateDistance = 3;
    List<Wall> neighbors;
	SpriteRenderer sr;

    void Start()
    {
    	neighbors = new List<Wall>();
        DOTween.Init();
//        defaultFade = gameObject.GetComponent<SpriteRenderer>().color.a;
		sr = gameObject.GetComponent<SpriteRenderer>();
		Color startColor = sr.color;
		startColor.a = defaultFade;
		sr.color = startColor;
        width = GetComponent<SpriteRenderer>().bounds.extents.x * 2;
		getWall(Vector2.up);
		getWall(Vector2.right);
		getWall(Vector2.down);
		getWall(Vector2.left);
    }

    // Pulses when avatars collide with wall segments
    void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.GetComponent<Avatar>()) {
			StartCoroutine (Pulse ());
			List<Wall> visited = new List<Wall>();
			visited.Add(this);
			foreach (Wall neighbor in neighbors) {
				neighbor.doPulse(visited, propagateDistance);
			}
		}
    }

    void getWall (Vector2 dir)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, dir, width);
		foreach (RaycastHit2D hit in hits) {
			Wall hitWall = hit.collider.GetComponent<Wall> ();
			if (hitWall && hitWall != this) {
				neighbors.Add(hitWall);
				return;
			}
		}
	}

	void doPulse (List<Wall> visited, int propagate)
	{
		visited.Add(this);
		StartCoroutine (Pulse ());
		if (propagate > 0) {
			StartCoroutine(DelayedPropagate(visited, propagate - 1));
		}
	}

	IEnumerator DelayedPropagate (List<Wall> visited, int propagate)
	{
		yield return new WaitForSeconds(propagateDelay);
		foreach (Wall neighbor in neighbors) {
			if (!visited.Contains(neighbor)) {
				neighbor.doPulse(visited, propagate);
			}
		}
	}

    IEnumerator Pulse()
    {
        yield return GetComponent<SpriteRenderer>().DOFade(1, fadeTime).WaitForCompletion();
        yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, fadeTime).WaitForCompletion();
    }
}
