using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	float speed = .05f;
	MeshRenderer mr;
	Vector3 cameraStart;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		cameraStart = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = Camera.main.transform.position - cameraStart;
		mr.material.mainTextureOffset = offset * speed;
	}
}
