using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * This script manages the instantiating of life tokens on the corners for each player at the beginning of the round.
 * Contains funtion subtractLife() to be called on player collisions, and will remove life tokens from the UI display.
*/

public class LifeOverlay : MonoBehaviour {

    public static LifeOverlay instance = null;
    public GameController gameController;

    public GameObject[] lifeIcons;
    public GameObject[][] lives;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Lives jagged array for [player][lifeID]
        lives = new GameObject[4][]
        {
            new GameObject[gameController.maxLives],
            new GameObject[gameController.maxLives],
            new GameObject[gameController.maxLives],
            new GameObject[gameController.maxLives]
        };
        
        int displacement = 30;

        // For each player:
        for (int i = 0; i < gameController.maxPlayers; ++i)
        {
            // Gets position of current child's empty position marker
            Vector3 origPos = transform.GetChild(i).transform.position;

            // Populates life icons at start
            for (int j = 0; j < gameController.maxLives; ++j)
            {
                // Creates a life icon for currently iterating player
                GameObject obj = Instantiate(lifeIcons[i]) as GameObject;

                // Checks for even or odd index and translates life icons in the correct direction
                if (i % 2 == 0)
                    obj.GetComponent<RectTransform>().position += new Vector3(displacement * j, 0, 0);
                else
                    obj.GetComponent<RectTransform>().position -= new Vector3(displacement * j, 0, 0);
                
                // Puts the new object into the lives array
                lives[i][j] = obj;

                // Sets new object's parent to be this transform
                obj.transform.SetParent(transform, false);
            }
        }
	}

    // Called to remove a life
    public void subtractLife(int targetPlayer, int targetLife)
    {
        Destroy(lives[targetPlayer - 1][targetLife].gameObject);
        lives[targetPlayer - 1][targetLife] = null;
    }
}
