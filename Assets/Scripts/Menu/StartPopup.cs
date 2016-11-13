using UnityEngine;
using System.Collections;

public class StartPopup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			// continue to next state
		} else if (Input.GetMouseButtonDown (1)) {
			// return to previous state
			Destroy(this.gameObject);
		}
	}

	public void CustomMouseDown() {
		print ("foo");
	}

	void OnMouseDown() {
		print ("foo");
	}
}
