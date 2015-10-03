using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Camera : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    public GameObject midpoint;
    public float origDistance;
    public float curDistance;
    public float distRatio;

    void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");

        origDistance = Mathf.Abs(Vector3.Distance(P1.transform.position, midpoint.transform.position) * 2);
    }

    void Update()
    {
        curDistance = Mathf.Abs(Vector3.Distance(P1.transform.position, midpoint.transform.position) * 2);
        distRatio = curDistance / origDistance;
        if (distRatio > 0.65f)
        {
            //this.GetComponent<Camera>(). = 7 + distRatio;
        }
        transform.position = new Vector3(midpoint.transform.position.x, midpoint.transform.position.y, -20.0f);
	}
}
