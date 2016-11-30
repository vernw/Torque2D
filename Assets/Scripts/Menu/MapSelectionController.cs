using UnityEngine;
using System.Collections;

public class MapSelectionController : MonoBehaviour {
	RectTransform leftUp;
	RectTransform leftDown;
	RectTransform right;
	RectTransform rt;
	Canvas canvas;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		canvas = GetComponent<Canvas> ();
		GameObject blankCanvasGO = (GameObject)Resources.Load ("Prefabs/BlankCanvas");
		leftDown = ((GameObject) Instantiate (blankCanvasGO)).GetComponent<RectTransform>();
		leftDown.parent = transform;
		leftDown.localScale = new Vector3 (1, 1, 1);
		leftDown.rotation = rt.rotation;
		leftDown.name = "LeftDown";
		leftUp = ((GameObject) Instantiate (blankCanvasGO)).GetComponent<RectTransform>();
		leftUp.parent = transform;
		leftUp.localScale = new Vector3 (1, 1, 1);
		leftUp.rotation = rt.rotation;
		leftUp.name = "LeftUp";
		right = ((GameObject) Instantiate (blankCanvasGO)).GetComponent<RectTransform>();
		right.parent = transform;
		right.localScale = new Vector3 (1, 1, 1);
		right.rotation = rt.rotation;
		right.name = "Right";
		SetActive (Children.LEFTUP);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			SetActive (Children.LEFTUP);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			SetActive (Children.LEFTDOWN);
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			SetActive (Children.RIGHT);
		}
	}

	void SetActive(Children newActive) {
		float width = rt.rect.width;
		float height = rt.rect.height;
		switch (newActive) {
		case Children.LEFTUP:
			leftUp.sizeDelta = new Vector2 (width, height);
			leftUp.localPosition = new Vector2 (0, 0);
			leftDown.sizeDelta = new Vector2 (0, 0);
			leftDown.localPosition = new Vector2 (0, 0);
			right.sizeDelta = new Vector2 (0, 0);
			right.localPosition = new Vector2 (0, 0);
			break;
		case Children.LEFTDOWN:
			leftUp.sizeDelta = new Vector2 (width, height * 0.2f);
			leftUp.localPosition = new Vector2 (0, height / 2f - height * 0.2f / 2f);
			leftDown.sizeDelta = new Vector2 (width, height * 0.8f);
			leftDown.localPosition = new Vector2 (0, -height / 2f + height * 0.8f / 2f);
			right.sizeDelta = new Vector2 (0, 0);
			right.localPosition = new Vector2 (0, 0);
			break;
		case Children.RIGHT:
			leftUp.sizeDelta = new Vector2 (width * 0.2f, height * 0.5f);
			leftUp.localPosition = new Vector2 (-width / 2f + width * 0.2f / 2f, height / 2f - height * 0.5f / 2f);
			leftDown.sizeDelta = new Vector2 (width * 0.2f, height * 0.5f);
			leftDown.localPosition = new Vector2 (-width / 2f + width * 0.2f / 2f, -height / 2f + height * 0.5f / 2f);
			right.sizeDelta = new Vector2 (width * 0.8f, height);
			right.localPosition = new Vector2 (width / 2f - width * 0.8f / 2f, 0);
			break;
		}
	}

	enum Children {
		LEFTUP,
		LEFTDOWN,
		RIGHT
	}
}
