using UnityEngine;
using System.Collections;

public class SoccerBall : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Goal1" || coll.gameObject.name == "Goal1(Clone)")
        {
            print("Team 2 Scores!");
            SoccerController.instance.team2Score++;
        }
        else if (coll.gameObject.name == "Goal2" || coll.gameObject.name == "Goal2(Clone)")
        {
            print("Team 1 Scores!");
            SoccerController.instance.team1Score++;
        }
    }
}
