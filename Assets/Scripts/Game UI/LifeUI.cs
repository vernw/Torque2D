using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : GenericUI {
	Dictionary<Corner, LifeValues> associations;
	GameObject lifeVisualGO;
	float fadeRate = 1f;
	float betweenLifeVisuals = 1f;

	public override void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs, int maxLives) {
		base.Initialize (playerDefs);
//		Debug.Log ("init");
		associations = new Dictionary<Corner, LifeValues> ();
		lifeVisualGO = Resources.Load ("Prefabs/LifePrefab") as GameObject;
		foreach (KeyValuePair<Util.PLAYER, Corner> c in corners) {
			LifeValues values = new LifeValues (maxLives);
			associations [c.Value] = values;
//			corner.Value.g = values;
		}
//		corners = new Dictionary<Util.PLAYER, LifeCorner> ();
		StartCoroutine(CustomUpdate());
	}
	
	// Update is called once per frame
//	void Update () {
//		customUpdate ();
//		foreach (KeyValuePair<Util.PLAYER, Corner> c in corners) {
//			Corner corner = c.Value;
////			Debug.Log (associations + " : " + corner);
//			LifeValues lv = associations [corner];
////			int foo = corner.g.curLives;
//			if (lv.state == LifeValues.State.ACT) {
//				if (lv.goalLives > lv.curLives) {
//					GameObject newVisualGO = Instantiate (lifeVisualGO) as GameObject;
//					lv.visuals.Add (newVisualGO);
//					newVisualGO.GetComponent<LifeVisual> ().Initialize (this);
//					lv.state = LifeValues.State.WAIT;
//				} else if (lv.goalLives < lv.curLives) {
//
//				}
//			}
//		}
//	}

	IEnumerator CustomUpdate() {
		while (true) {
			CommonUpdate ();
			foreach (KeyValuePair<Util.PLAYER, Corner> c in corners) {
				Corner corner = c.Value;
				//			Debug.Log (associations + " : " + corner);
				LifeValues lv = associations [corner];
//				Debug.Log (corner.position);
				for (int i = 0; i < lv.visuals.Count; i++) {
//				Debug.Log (corner.position);
					lv.visuals [i].transform.position = corner.position + new Vector3 (i * betweenLifeVisuals * (corner.position.x > Camera.main.transform.position.x ? -1f : 1f), 0f, 0f);
//				lv.visuals [i].GetComponent<SpriteRenderer> ().color = Util.ConvertColor(corner.color);
				}
				//			int foo = corner.g.curLives;
				if (lv.goalLives > lv.curLives) {
					GameObject newVisualGO = Instantiate (lifeVisualGO) as GameObject;
					lv.visuals.Add (newVisualGO);
					lv.curLives++;
					Debug.Log (lv.curLives);
					SpriteRenderer sr = newVisualGO.GetComponent<SpriteRenderer> ();
					sr.color = Util.ConvertColor (corner.color);
					while (sr.color.a < 1f) {
						Color newColor = sr.color;
						newColor.a = Mathf.Min (newColor.a + Time.deltaTime * fadeRate, 1f);
						sr.color = newColor;
						yield return new WaitForEndOfFrame ();
					}
//				newVisualGO.GetComponent<LifeVisual> ().Initialize (this);
//				lv.state = LifeValues.State.WAIT;
				} else if (lv.goalLives < lv.curLives) {
					for (int i = 0; i < lv.goalLives - lv.curLives; i++) {
						GameObject go = lv.visuals [lv.visuals.Count - 1];
						lv.visuals.RemoveAt (lv.visuals.Count - 1);
						//TODO: spawn explosion
						Destroy (go);
						lv.curLives--;
					}
				}
			}
			yield return new WaitForEndOfFrame ();
		}
	}

//	protected class LifeCorner : Corner {
//
//	}

	struct LifeValues {
		public int goalLives;
		public int curLives;
		public List<GameObject> visuals;
//		public enum State { WAIT, ACT };
//		public State state;

		public LifeValues(int _goalLives) {
			goalLives = _goalLives;
			curLives = 0;
			visuals = new List<GameObject>();
//			state = State.ACT:
		}
	}
}
