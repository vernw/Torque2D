using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
 * This script contains functions for the instantiating of life tokens at the corners for each player at the beginning of the round.
*/

public class UIController : MonoBehaviour {

    public static LifeOverlay instance = null;

    public MenuController menuController;
	public Util.PLAYER player;
    public Util.COLOR color;

    public GameObject[][] lives;
    public GameObject lifeSprite;

	public Dictionary<Util.COLOR,int> scores = new Dictionary<Util.COLOR, int>();
	public List<Util.COLOR> teamLog  = new List<Util.COLOR>();
    public GameObject[] scoreDisplays;
    public GameObject scoreObject;

    public Vector3[] markers;

    public float delay = 0.6f;
    public float buffer = 0.2f;
    public float scaleUp = 1.5f;

    private Vector3 _origScale;
    private Vector3 _largeScale;
    private Vector3 _curScale;

    int maxLives;
    int maxScore;
    
    // Gets marker positions
    void Start ()
    {
        // Markers numbered from left to right, then from top to bottom
        markers = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            markers[i] = transform.GetChild(i).transform.position;
        }
    }
	
    // Accepts List of Players, Max Lives
    public void Init(List<Player> players, int lifeTotal)
    {
        // Remembers total lives for use later
        maxLives = lifeTotal;

        // Instantiates array of life sprites
        lives = new GameObject[players.Count][];
        for (int i = 0; i < players.Count; i++)
        {
            lives[i] = new GameObject[players[i].lives];
        }

        float displacement = 15f;

        // For each player:
        for (int i = 0; i < players.Count; ++i)
        {
            // Gets position of current child's empty position marker
            Vector3 origPos = markers[i];

            // Populates life icons at start
            for (int j = 0; j < maxLives; ++j)
            {
                // Creates a life icon for currently iterating player
                GameObject obj = Instantiate(lifeSprite, markers[i], Quaternion.identity) as GameObject;

                // Checks for even or odd index and translates life icons in the correct direction
                if (i % 2 == 0)
                    obj.GetComponent<RectTransform>().position += new Vector3(displacement * j, 0, 0);
                else
                    obj.GetComponent<RectTransform>().position -= new Vector3(displacement * j, 0, 0);

                // Inserts the new object into the lives array
                lives[i][j] = obj;

                _curScale = obj.transform.localScale;
                _origScale = _curScale;
                _largeScale = _origScale * scaleUp;

                // Setting game object scale and color
                obj.transform.localScale = _largeScale;

                StartCoroutine(FadeSequence(lives[i][j], _origScale, j));

                // Sets new object's parent to be this transform
                obj.transform.SetParent(transform, false);
            }
        }
    }

	public void Init(List<Util.COLOR> teams, int scoreTotal)
    {
        // Remembers max score for use later
        maxScore = scoreTotal;

        // Initializes each player's starting score
		scores[Util.COLOR.BLUE] = 0;
		scores[Util.COLOR.RED] = 0;
		scores[Util.COLOR.YELLOW] = 0;
		scores[Util.COLOR.GREEN] = 0;
        
        int counter = 0;
        
        // Instantiates all score displays in order corner
		foreach (Util.COLOR t in teams)
        {
            teamLog.Add(t);
            switch (t)
            {
			case Util.COLOR.BLUE:
                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
                scoreDisplays[counter].transform.SetParent(transform, false);
                counter++;
                    break;
			case Util.COLOR.RED:
                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
                scoreDisplays[counter].transform.SetParent(transform, false);
                counter++;
                break;
			case Util.COLOR.YELLOW:
                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
                scoreDisplays[counter].transform.SetParent(transform, false);
                counter++;
                break;
			case Util.COLOR.GREEN:
                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
                scoreDisplays[counter].transform.SetParent(transform, false);
                break;
            }
        }
    }

    // Removes life sprites down to given value
    public void UpdateUI(int player, int value)
    {
        for (int i = maxLives; i > value; i++)
        {
            lives[player][i].SetActive(false);
        }
    }

    // Changes score display to given value
	public void UpdateUI(Util.COLOR team, int value)
    {
		foreach (Util.COLOR t in teamLog)
        {
            if (team == t)
            {
                scoreDisplays[teamLog.IndexOf(t)].GetComponent<TextMesh>().text = value.ToString();
            }
        }
    }

    IEnumerator FadeSequence(GameObject obj, Vector3 origScale, float i)
    {
        //delay = (3 / gameController.maxLives);

        // Staggers animations down the chain
        yield return new WaitForSeconds(delay * i);
        obj.transform.DOScale(origScale, delay);
        DOTween.ToAlpha(() => obj.GetComponent<SpriteRenderer>().color, x => obj.GetComponent<SpriteRenderer>().color = x, 1.0f, delay);
    }
}
