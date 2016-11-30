using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticalList : MonoBehaviour {
	public List<string> elements;
	public float verticalSpacing = 40f;

	float wordHeight = 40f;

	// Use this for initialization
	void Start () {
//		if (elements == null) {
//			elements = new List<string> ();
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(List<string> _elements) {
		elements = _elements;
		float curVerticalSpacing = verticalSpacing * elements.Count / 2 - wordHeight / 2f;
		foreach (string element in elements) {
			GameObject elementGO = Instantiate (Resources.Load("Prefabs/VerticalListElement", typeof(GameObject))) as GameObject;
			elementGO.transform.position = new Vector2 (0, curVerticalSpacing);
			elementGO.transform.parent = transform;
			elementGO.GetComponent<VerticalListElement>().Initialize (element);
			curVerticalSpacing -= verticalSpacing;
		}
	}
}
