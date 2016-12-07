using UnityEngine;
using System.Collections;

public class TDDUPLink : MonoBehaviour {
//	public GameObject neighborOne;
//	public GameObject neighborTwo;
	public bool debug;

	public GameObject fn;
	public GameObject bn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void frontNeighbor (GameObject _fn) {
		fn = _fn;
		Avatar fnAvatar = fn.GetComponent<Avatar> ();
		if (fnAvatar != null) {
//			Player fnPlayer = fnAvatar.player;
			StartCoroutine (MonitorFrontNeighbor (fnAvatar));
		}
//		StartCoroutine (MonitorFrontNeighbor ());
	}

	public void backNeighbor (GameObject _bn) {
		bn = _bn;
		Avatar bnAvatar = bn.GetComponent<Avatar> ();
		if (bnAvatar != null) {
//			Player bnPlayer = bnAvatar.player;
			StartCoroutine (MonitorBackNeighbor (bnAvatar));
		}
//		StartCoroutine (MonitorBackNeighbor ());
	}

	public void doDestroyFowards () {
//		print ("forward " + gameObject.name);
		TDDUPLink link = fn.GetComponent<TDDUPLink> ();
		if (link) {
			link.doDestroyFowards ();
		} else {
			Player pl = fn.GetComponent<Avatar> ().player;
			pl.onDeath (pl);
		}
		Destroy (gameObject);
	}

	public void doDestroyBackwards () {
//		print ("backward " + gameObject.name);
		TDDUPLink link = bn.GetComponent<TDDUPLink> ();
		if (link) {
			link.doDestroyBackwards ();
		} else {
			Player pl = bn.GetComponent<Avatar> ().player;
			pl.onDeath (pl);
		}
		Destroy (gameObject);
	}

	IEnumerator MonitorFrontNeighbor(Avatar fnAvatar) {
		while (true) {
			if (!fnAvatar.gameObject.activeSelf) {
//				Destroy (this.gameObject);
//				bn.GetComponent<TDDUPLink>().doDestroyFowards();
				doDestroyBackwards ();
			}
//			if (debug) {
//				print ("fn " + " : " + fn.name + " : " + fn.activeSelf);
//			}
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator MonitorBackNeighbor(Avatar bnAvatar) {
		while (true) {
//			if (bn == null || !bn.activeSelf) {
//				Avatar fnAvatar = fn.GetComponent<Avatar> ();
//				if (fnAvatar) {
//					fnAvatar.player.doDestruct ();
//				} else {
//					fn.GetComponent<TDDUPLink> ().doDestroy ();
//				}
//				Destroy (gameObject);
//			}
//			if (debug) {
//				print ("bn " + bn.name + " : " + bn.activeSelf);
//			}
			if (!bnAvatar.gameObject.activeSelf) {
//				fn.GetComponent<TDDUPLink> ().doDestroyBackwards ();
				doDestroyFowards ();
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}
