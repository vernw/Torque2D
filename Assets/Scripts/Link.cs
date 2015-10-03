using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public GameObject[] linkColliders = new GameObject[4];
    public GameObject[] players = new GameObject[2];

	// Use this for initialization
    void Start()
    {
        // Creates collision ignore for all wall-link and wall-puck collisions
        if (gameObject.tag == "P1")
        {
            for (int i = 0; i < linkColliders.Length; i++)
            {
                foreach (Transform child in players[1].transform)
                {
                    Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
                }
            }
        }
        else if (gameObject.tag == "P2")
        {
            for (int i = 0; i < linkColliders.Length; i++)
            {
                foreach (Transform child in players[0].transform)
                {
                    Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
                }
            }
        }
    }
}
