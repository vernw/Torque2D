using UnityEngine;
using System.Collections;

public class Midpoint : MonoBehaviour {

    public GameObject P1, P2;

	void Update () {
        transform.position = (P1.transform.position + P2.transform.position) / 2;
	}
}
