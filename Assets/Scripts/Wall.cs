using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Wall : MonoBehaviour {

    public float defaultFade;

    float fadeTime = 1f;
    float width;
    float propagateDelay = .1f;
    int propagateDistance = 3;
    Wall north;
    Wall east;
    Wall south;
    Wall west;

    void Start()
    {
        DOTween.Init();

        defaultFade = gameObject.GetComponent<SpriteRenderer>().color.a;
        width = GetComponent<SpriteRenderer>().bounds.extents.x * 2;
	north = getWall(Vector2.up);
	east = getWall(Vector2.right);
	south = getWall(Vector2.down);
	west = getWall(Vector2.left);
    }

    // Pulses when avatars collide with wall segments
    void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "P1" || coll.gameObject.tag == "P2" || coll.gameObject.tag == "P3" || coll.gameObject.tag == "P4") {
			StartCoroutine (Pulse ());
			if (north) north.doPulse(DIR.NORTH, propagateDistance);
			if (east) east.doPulse(DIR.EAST, propagateDistance);
			if (south) south.doPulse(DIR.SOUTH, propagateDistance);
			if (west) west.doPulse(DIR.WEST, propagateDistance);
		}
    }

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

	void doPulse (DIR dir, int propagate)
	{
		StartCoroutine (Pulse ());
		if (propagate > 0)
			StartCoroutine(DelayedPropagate(dir, propagate - 1));
	}

	IEnumerator DelayedPropagate (DIR dir, int propagate)
	{
		yield return new WaitForSeconds(propagateDelay);
		switch (dir) {
			case DIR.NORTH:
				if (north) north.doPulse(dir, propagate);
			break;
			case DIR.EAST:
				if (east) east.doPulse(dir, propagate);
			break;
			case DIR.SOUTH:
				if (south) south.doPulse(dir, propagate);
			break;
			case DIR.WEST:
				if (west) west.doPulse(dir, propagate);
			break;
		}
	}

    IEnumerator Pulse()
    {
        yield return GetComponent<SpriteRenderer>().DOFade(1, fadeTime).WaitForCompletion();
        yield return GetComponent<SpriteRenderer>().DOFade(defaultFade, fadeTime).WaitForCompletion();
    }

    enum DIR {
    	NORTH,
    	EAST,
    	SOUTH,
    	WEST
    }
}
