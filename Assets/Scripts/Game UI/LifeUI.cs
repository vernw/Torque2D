using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : GenericUI {
	Dictionary<Corner, LifeValues> associations;
	GameObject lifeVisualGO;
	float fadeRate = 2f;
	float betweenLifeVisuals = 0.75f;
	float startScale = 1f;
	float startOrthoSize = 0f;
	float edgeUpBuffer = 2;
	float edgeSideBuffer = 2;

	public override void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs, int maxLives) {
		base.Initialize (playerDefs);
		associations = new Dictionary<Corner, LifeValues> ();
		lifeVisualGO = Resources.Load ("Prefabs/LifePrefab") as GameObject;
		foreach (KeyValuePair<Util.PLAYER, Corner> c in corners) {
			LifeValues values = new LifeValues (maxLives);
			associations [c.Value] = values;
			StartCoroutine(CustomUpdate(c.Value));
		}
	}

	public void UpdateLives(Player player) {
		Corner corner = corners [player.playerType];
		LifeValues values = associations [corner];
		values.goalLives = player.lives;
		associations [corner] = values;
		Debug.Log (corner.color + " : " + associations [corner].goalLives);
	}

	Vector3[] CalculateCornerPositions() {
		Camera cam = Camera.main;
		Vector3 center = cam.transform.position;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		Vector3[] output = new Vector3[4];
		output [0] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
		output [1] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
		output [2] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
		output [3] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
		return output;
	}

	IEnumerator CustomUpdate(Corner target) {
		StartCoroutine (HandlePosition (target));
		LifeValues lv = associations [target];
		while (true) {
//			Debug.Log (target.color + " : " + lv.goalLives);
			if (lv.goalLives > lv.curLives) {
				GameObject newVisualGO = Instantiate (lifeVisualGO) as GameObject;
				lv.visuals.Add (newVisualGO);
				newVisualGO.transform.parent = transform;
				lv.curLives++;
				SpriteRenderer sr = newVisualGO.GetComponent<SpriteRenderer> ();
				sr.color = Util.ConvertColor (target.color);
				Color temp = sr.color;
				temp.a = 0;
				sr.color = temp;
				while (sr.color.a < 1f) {
					Color newColor = sr.color;
					newColor.a = Mathf.Min (newColor.a + Time.deltaTime * fadeRate, 1f);
					sr.color = newColor;
					yield return new WaitForEndOfFrame ();
				}
			} else if (lv.goalLives < lv.curLives) {
				for (int i = 0; i < lv.goalLives - lv.curLives; i++) {
					GameObject go = lv.visuals [lv.visuals.Count - 1];
					lv.visuals.RemoveAt (lv.visuals.Count - 1);
					//TODO: spawn explosion
					Destroy (go);
					lv.curLives--;
				}
			}
			associations[target] = lv;
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator HandlePosition(Corner target) {
		LifeValues lv = associations [target];
//		startScale = 1f;
		Camera cam = Camera.main;
		startOrthoSize = cam.orthographicSize;
		while (true) {
			CommonUpdate (target);
			for (int i = 0; i < lv.visuals.Count; i++) {
				float scalar = cam.orthographicSize / startOrthoSize;
				lv.visuals [i].transform.position = target.position + new Vector3 (i * betweenLifeVisuals * scalar * (target.position.x > cam.transform.position.x ? -1f : 1f), 0f, 0f);
				lv.visuals [i].transform.localScale = Vector3.one * scalar * startScale;
			}
//			Debug.Log (cam.orthographicSize);
			yield return new WaitForEndOfFrame ();
		}
	}

	protected class Corner {
		public Vector3 position;
		public Util.COLOR color;
		public Util.PLAYER player;
		public int goalLives;
		public int curLives;
		public List<GameObject> visuals;
		//		public int value;
		//		public T generic;

		public Corner(Vector3 _position, Util.COLOR _color, Util.PLAYER _player) {
			position = _position;
			color = _color;
			player = _player;
		}
	}

//	struct LifeValues {
//		public int goalLives;
//		public int curLives;
//		public List<GameObject> visuals;
//
//		public LifeValues(int _goalLives) {
//			goalLives = _goalLives;
//			curLives = 0;
//			visuals = new List<GameObject>();
//		}
//	}
}
