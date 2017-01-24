using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameCamera : MonoBehaviour {
    public List<Player> players;

	float minX;
	float maxX;
	float minY;
	float maxY;
	float cameraBuffer = 5;
	float camSize;

    float curDistance;
    float distRatio;

	float minWidth = 15f;
	float minHeight = 0f;
    float zoomMultiplier = .5f;
    float speed = 3f;
    float zoomSpeed = 3f;

	Camera cam;

    void Start()
    {
		cam = GetComponent<Camera> ();
		float aspectRatio = cam.aspect;
		minHeight = 1f / (aspectRatio / minWidth);
    }
    
    void Update()
    {
		if (players == null || players.Count == 0) {
		    return;
		}
		// position
		Vector3 midpoint = Vector3.zero;
		foreach(Player player in players) {
		    midpoint += player.transform.GetChild(0).transform.position;
		}
		midpoint /= players.Count;
		transform.position = Vector3.Lerp(transform.position, new Vector3(midpoint.x, midpoint.y, -20.0f), Time.deltaTime * speed);
		// zoom
		float left = Mathf.Infinity;
		float right = -Mathf.Infinity;
		float up = -Mathf.Infinity;
		float down = Mathf.Infinity;
		foreach(Player player in players) {
		    left = Mathf.Min(left, player.transform.GetChild(0).transform.position.x);
		    right = Mathf.Max(right, player.transform.GetChild(0).transform.position.x);
		    up = Mathf.Max(up, player.transform.GetChild(0).transform.position.y);
		    down = Mathf.Min(down, player.transform.GetChild(0).transform.position.y);
		}
		float leftRight = Mathf.Max((right - left), minWidth);
		float upDown = Mathf.Max(((up - down) * Camera.main.aspect), minHeight);
		float bound = Mathf.Max(leftRight, upDown) * zoomMultiplier;
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, bound, Time.deltaTime * zoomSpeed);
    }
}
