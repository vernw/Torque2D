using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsController : MonoBehaviour {
	RectTransform leftChild;
	RectTransform rightChild;
	RectTransform rt;
	Canvas canvas;
//	GameObject active;
	float activeWidthRatio = .75f;
	float inactiveWidthRatio = 0;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		canvas = GetComponent<Canvas> ();
		inactiveWidthRatio = 1f - activeWidthRatio;
		GameObject blankCanvasGO = (GameObject)Resources.Load ("Prefabs/BlankCanvas");
		leftChild = ((GameObject) Instantiate (blankCanvasGO)).GetComponent<RectTransform>();
		leftChild.parent = transform;
		leftChild.localScale = new Vector3 (1, 1, 1);
		leftChild.rotation = rt.rotation;
		leftChild.name = "Left";
		rightChild = ((GameObject) Instantiate (blankCanvasGO)).GetComponent<RectTransform>();
		rightChild.parent = transform;
		rightChild.localScale = new Vector3 (1, 1, 1);
		rightChild.rotation = rt.rotation;
		rightChild.name = "Right";
		List<string> listVals = new List<string> {"foo", "bar", "baz"};
		(rightChild.gameObject.AddComponent<VerticalList> ()).Init(listVals);
		SetActive (Children.LEFT);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			SetActive (Children.LEFT);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			SetActive (Children.RIGHT);
		}
	}

	void SetActive(Children newActive) {
		float width = rt.rect.width;
//		print (width);
		switch (newActive) {
		case Children.LEFT:
			leftChild.sizeDelta = new Vector2 (width * activeWidthRatio, rt.rect.height);
			rightChild.sizeDelta = new Vector2 (width * inactiveWidthRatio, rt.rect.height);
			leftChild.localPosition = new Vector2 (-width / 2f + width * activeWidthRatio / 2f, 0);
			rightChild.localPosition = new Vector2 (width / 2f - width * inactiveWidthRatio / 2f, 0);
			break;
		case Children.RIGHT:
			leftChild.sizeDelta = new Vector2 (width * inactiveWidthRatio, rt.rect.height);
			rightChild.sizeDelta = new Vector2 (width * activeWidthRatio, rt.rect.height);
			leftChild.localPosition = new Vector2 (-width / 2f + width * inactiveWidthRatio / 2f, 0);
			rightChild.localPosition = new Vector2 (width / 2f - width * activeWidthRatio / 2f, 0);
			break;
		}
	}

	enum Children {
		LEFT,
		RIGHT
	}
}
