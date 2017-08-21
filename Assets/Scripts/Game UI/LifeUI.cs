using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : GenericUI {
	public override void Initialize (Dictionary<Util.PLAYER, Util.COLOR> playerColors, int maxLives) {
		base.Initialize (playerColors);
		foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerColors) {
			corners [p.Key] = new LifeCorner (p.Key, p.Value, maxLives);
		}
	}

	public void Update() {
		foreach (KeyValuePair<Util.PLAYER, Corner> p in corners) {
			((LifeCorner)p.Value).Update ();
		}
	}

	class LifeCorner : Corner {
		List<GameObject> lifeGOs;
		LifeUI parent;
		float lifeBuffer = .1f;

		public LifeCorner(Util.PLAYER player, Util.COLOR color, int count) : base (player, color, count) {
			lifeGOs = new List<GameObject>();
			parent = GameObject.FindObjectOfType<LifeUI>();
			for (int i = 0; i < count; i++) {
				AddLife();
			}
			StartCoroutine(HandleGoal());
		}

		public void Update() {
			base.Update();
			for (int i = 0; i < count; i++) {
				GameObject go = lifeGOs [i];
				go.transform.localScale = new Vector3 (scale, scale, 1f);
				float seperation = go.GetComponent<SpriteRenderer> ().bounds.extents.x * 2f + lifeBuffer * scale;
				go.transform.position = new Vector3 (
					position.x + (seperation * i) * ((edge == Edge.TOPLEFT || edge == Edge.BOTLEFT) ? 1f : -1f),
					position.y, 0f
				);
			}
		}

		void AddLife() {
			GameObject go = Instantiate(Resources.Load("Prefabs/GameModes/LifePrefab") as GameObject) as GameObject;
			go.transform.parent = parent.transform;
			lifeGOs.Add(go);
		}

		IEnumerator HandleGoal() {
			LifeVisual newestLife = null;
			while (true) {
				if (goal < count) {
					for (int i = count; i >= goal; i--) {
						GameObject go = lifeGOs [i];
						lifeGOs.Remove (go);
						Destroy (go);
						//TODO explosion!
					}
					count = goal;
				} else if (goal > count) {
					if (!newestLife || !newestLife.entering) {

					}
				}
				yield return new WaitForEndOfFrame ();
			}
		}
	}
}
