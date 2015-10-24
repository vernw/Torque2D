using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Camera : MonoBehaviour {

    public GameObject gameController;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    public GameObject midpoint;
    
    public GameObject[] players;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float cameraBuffer = 5;
    public float camSize;

    public float origDistance;
    public float curDistance;
    public float distRatio;

    void Start()
    {
        origDistance = Mathf.Abs(Vector3.Distance(P1.transform.position, midpoint.transform.position) * 2);
    }
    
    void Update()
    {
        transform.position = new Vector3(midpoint.transform.position.x, midpoint.transform.position.y, -20.0f);
	}
}
