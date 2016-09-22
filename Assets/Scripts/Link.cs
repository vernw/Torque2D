using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    // public GameObject[] players = new GameObject[4];
    // public GameObject[] linkColliders = new GameObject[4];

    // GameObject[] linkColliders = new GameObject[4];

	// Use this for initialization
    void Start()
    {
        foreach(Transform childA in transform) {
            CircleCollider2D colliderA = childA.GetComponent<CircleCollider2D>();
            if (colliderA) {
                foreach(Transform childB in transform) {
                    CircleCollider2D colliderB = childB.GetComponent<CircleCollider2D>();
                    if (colliderB) {
                        Physics2D.IgnoreCollision(colliderA, colliderB);
                    }
                }
            }
        }
        // players = GameObject.FindGameObjectsWithTag("Player");

        // // Creates collision ignore for all wall-link and wall-puck collisions
        // switch (gameObject.transform.GetChild(0).tag)
        // {
        //     case "P1":
        //         linkColliders = GameObject.FindGameObjectsWithTag("P1Link");
        //         for (int i = 0; i < linkColliders.Length; i++)
        //         {
        //             foreach (Transform child in players[1].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[2].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[3].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //         }
        //         break;
        //     case "P2":
        //         linkColliders = GameObject.FindGameObjectsWithTag("P2Link");
        //         for (int i = 0; i < linkColliders.Length; i++)
        //         {
        //             foreach (Transform child in players[0].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[2].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[3].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //         }
        //         break;
        //     case "P3":
        //         linkColliders = GameObject.FindGameObjectsWithTag("P3Link");
        //         for (int i = 0; i < linkColliders.Length; i++)
        //         {
        //             foreach (Transform child in players[0].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[1].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[3].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //         }
        //         break;
        //     case "P4":
        //         linkColliders = GameObject.FindGameObjectsWithTag("P4Link");
        //         for (int i = 0; i < linkColliders.Length; i++)
        //         {
        //             foreach (Transform child in players[0].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[1].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //             foreach (Transform child in players[2].transform)
        //             {
        //                 Physics2D.IgnoreCollision(linkColliders[i].transform.GetComponent<CircleCollider2D>(), child.GetComponent<CircleCollider2D>());
        //             }
        //         }
        //         break;
        // }
    }
}
