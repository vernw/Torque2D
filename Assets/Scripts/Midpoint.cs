using UnityEngine;
using System.Collections;

public class Midpoint : MonoBehaviour {

    public GameObject gameController;
    public GameController controller;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public Avatar P1Avatar;
    public Avatar P2Avatar;
    public Avatar P3Avatar;
    public Avatar P4Avatar;

    private Vector3 center = new Vector3(0, 0, 0);

    void Start()
    {
        controller = gameController.GetComponent<GameController>();

        P1Avatar = P1.GetComponent<Avatar>();
        P2Avatar = P2.GetComponent<Avatar>();
        P3Avatar = P3.GetComponent<Avatar>();
        P4Avatar = P4.GetComponent<Avatar>();
    }


	void Update () {
        switch (controller.totalPlayers)
        {
            case 1:
                if (P1Avatar.alive)
                    transform.position = P1.transform.position + center;
                else if (P2Avatar.alive)
                    transform.position = P2.transform.position + center;
                else if (P3Avatar.alive)
                    transform.position = P3.transform.position + center;
                else if (P4Avatar.alive)
                    transform.position = P4.transform.position + center;
                break;
            case 2:
                if (P1Avatar.alive && P2Avatar.alive)
                    transform.position = (P1.transform.position + P2.transform.position + center) / 2;
                else if (P1Avatar.alive && P3Avatar.alive)
                    transform.position = (P1.transform.position + P3.transform.position + center) / 2;
                else if (P1Avatar.alive && P4Avatar.alive)
                    transform.position = (P1.transform.position + P4.transform.position + center) / 2;
                else if (P2Avatar.alive && P3Avatar.alive)
                    transform.position = (P2.transform.position + P3.transform.position + center) / 2;
                else if (P2Avatar.alive && P4Avatar.alive)
                    transform.position = (P2.transform.position + P4.transform.position + center) / 2;
                else if (P3Avatar.alive && P4Avatar.alive)
                    transform.position = (P3.transform.position + P4.transform.position + center) / 2;
                break;
            case 3:
                if (P1Avatar.alive && P2Avatar.alive && P3Avatar.alive)
                    transform.position = (P1.transform.position + P2.transform.position + P3.transform.position + center) / 3;
                if (P1Avatar.alive && P2Avatar.alive && P4Avatar.alive)
                    transform.position = (P1.transform.position + P2.transform.position + P4.transform.position + center) / 3;
                if (P1Avatar.alive && P3Avatar.alive && P4Avatar.alive)
                    transform.position = (P1.transform.position + P3.transform.position + P4.transform.position + center) / 3;
                if (P2Avatar.alive && P3Avatar.alive && P4Avatar.alive)
                    transform.position = (P2.transform.position + P3.transform.position + P4.transform.position + center) / 3;
                break;
            case 4:
                transform.position = (P1.transform.position + P2.transform.position + P3.transform.position + P4.transform.position + center) / 4;
                break;
        }
	}
}
