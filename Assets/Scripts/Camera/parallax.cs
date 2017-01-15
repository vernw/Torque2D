using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	float speed = .05f;
//	float startWidth;
	float cameraStartWidth;
	MeshRenderer mr;
	Vector3 cameraStart;
	Vector3 startScale;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		cameraStart = Camera.main.transform.position;
//		startWidth = transform.localScale.x;
		startScale = transform.localScale;
		cameraStartWidth = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = Camera.main.transform.position - cameraStart;
		mr.material.mainTextureOffset = offset * speed;
		float cameraWidth = Camera.main.orthographicSize;
//		print (cameraWidth);
		float ratio = cameraWidth / cameraStartWidth;
		transform.localScale = startScale * ratio;
	}
}
