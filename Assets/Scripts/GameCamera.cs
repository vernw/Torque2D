using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameCamera : MonoBehaviour {

    // public GameController gameController;

    // public GameObject P1;
    // public GameObject P2;
    // public GameObject P3;
    // public GameObject P4;

    // public GameObject midpoint;
    
    // public GameObject[] players;
    public List<Player> players;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float cameraBuffer = 5;
    public float camSize;

    // public float origDistance;
    public float curDistance;
    public float distRatio;

    // List<Player> players;
    float minWidth = 5f;
    float minHeight = 2.5f;
    float zoomMultiplier = .5f;
    float speed = 3f;
    float zoomSpeed = 3f;

    void Start()
    {
        // gameController = GameController.instance;
        // origDistance = Mathf.Abs(Vector3.Distance(P1.transform.position, midpoint.transform.position) * 2);
    }
    
    void Update()
    {
        if (players == null) {
            return;
        }
        // position
        Vector3 midpoint = Vector3.zero;
        foreach(Player player in players) {
            // print(player.transform.position + " : " + midpoint);
            midpoint += player.transform.GetChild(0).transform.position;
        }
        midpoint /= players.Count;
        // print(midpoint);
	   transform.position = Vector3.Lerp(transform.position, new Vector3(midpoint.x, midpoint.y, -20.0f), Time.deltaTime * speed);
       // zoom
       // float left = Mathf.Min(P1.transform.position.x, P2.transform.position.x, P3.transform.position.x, P4.transform.position.x);
       // float right = Mathf.Max(P1.transform.position.x, P2.transform.position.x, P3.transform.position.x, P4.transform.position.x);
       // float leftRight = Mathf.Max((right - left), minWidth);
       // float up = Mathf.Max(P1.transform.position.y, P2.transform.position.y, P3.transform.position.y, P4.transform.position.y);
       // float down = Mathf.Min(P1.transform.position.y, P2.transform.position.y, P3.transform.position.y, P4.transform.position.y);
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
       Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, bound, Time.deltaTime * zoomSpeed);
    }
}
