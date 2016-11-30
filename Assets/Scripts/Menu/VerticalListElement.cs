using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

//public class VerticalListElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
public class VerticalListElement : MonoBehaviour {
	public string value;

	Text text;
//	UnityEngine.UI.Button button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(string _value) {
		value = _value;
		text = GetComponent<Text> ();
		gameObject.name = value;
		text.fontSize = 100;
		text.text = value;
//		button = GetComponent<UnityEngine.UI.Button> ();
//		button.onClick.AddListener (() => {OnClick();});
//		button.onClick.AddListener(OnClick);
	}

//	public void OnClick() {
//		print (value);
//	}
//
//	void OnMouseDown() {
//		print (value);
//	}
//
//	void OnMouseOver() {
//		print (value);
//	}
//
//	public void OnPointerEnter(PointerEventData eventData) {
//		print (value);
//	}
//
//	public void OnPointerExit(PointerEventData eventData) {
//		print (value);
//	}
}
