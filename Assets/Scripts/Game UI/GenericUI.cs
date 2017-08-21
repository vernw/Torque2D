using System.Collections;
using System.Collections.Generic;
//using DG.Tweening;
using UnityEngine;

/*
 * This script contains functions for the instantiating of life tokens at the corners for each player at the beginning of the round.
*/

public abstract class GenericUI : MonoBehaviour {
	protected Dictionary<Util.PLAYER, Corner> corners;

	public virtual void Initialize (Dictionary<Util.PLAYER, Util.COLOR> playerColors, int count = 0)
	{
		corners = new Dictionary<Util.PLAYER, Corner> ();
	}

	public void UpdateCount(Util.PLAYER player, int count) {
		corners [player].goal = count;
	}

	public class Corner : MonoBehaviour
	{
		protected enum Edge { TOPLEFT, TOPRIGHT, BOTLEFT, BOTRIGHT }
		protected Edge edge;
		Util.COLOR color;
		protected Vector3 position;
		protected int count;
		public int goal;
		protected Camera cam;
		Vector3 camPos;
		float camHeight;
		protected float camWidth;
		float horizontalBuffer = 1f;
		float verticalBuffer = 1f;
		float expectedWidth = 30;
		protected float scale;

		public Corner(Util.PLAYER player, Util.COLOR _color, int _count) {
			edge = PlayerToEdge(player);
			color = _color;
			count = _count;
			goal = count;
			Update();
		}

		public void Update() {
			cam = Camera.main;
			camHeight = 2f * cam.orthographicSize;
			camWidth = camHeight * cam.aspect;
			camPos = cam.transform.position;
			position = new Vector3 (
				camPos.x + (camWidth / 2f - horizontalBuffer) *
				((edge == Edge.TOPRIGHT || edge == Edge.BOTRIGHT) ? 1f : -1f),
				camPos.y + (camHeight / 2f - verticalBuffer) *
				((edge == Edge.TOPLEFT || edge == Edge.TOPRIGHT) ? 1f : -1f), -10f
			);
			scale = camWidth / expectedWidth;
		}

		protected Edge PlayerToEdge(Util.PLAYER player) {
			switch (player) {
			case Util.PLAYER.ONE:
				return Edge.TOPLEFT;
				break;
			case Util.PLAYER.TWO:
				return Edge.TOPRIGHT;
				break;
			case Util.PLAYER.THREE:
				return Edge.BOTLEFT;
				break;
			case Util.PLAYER.FOUR:
				return Edge.BOTRIGHT;
				break;
			}
			return Edge.TOPRIGHT;
		}
	}
}
