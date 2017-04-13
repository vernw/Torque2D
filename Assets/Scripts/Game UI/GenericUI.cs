using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/*
 * This script contains functions for the instantiating of life tokens at the corners for each player at the beginning of the round.
*/

public abstract class GenericUI : MonoBehaviour {
	protected Dictionary<Util.PLAYER, Corner> corners;

	float edgeUpBuffer = 2;
	float edgeSideBuffer = 2;
//	List<Corner> corners;

	public virtual void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerDefs, int foo = 0) {
//		corners = new List<Corner> ();
		corners = new Dictionary<Util.PLAYER, Corner>();
		Vector3[] cornerPositions = calculateCornerPositions ();
		foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerDefs) {
			Corner newCorner = new Corner (cornerPositions [Util.playerToInt (p.Key)], p.Value);
			corners [p.Key] = newCorner;
//			corners [p.Key].position = cornerPositions [Util.playerToInt (p.Key)];
//			corners [p.Key].color = p.Value;
		}
	}

	protected void customUpdate() {

	}

	Vector3[] calculateCornerPositions() {
		Camera cam = Camera.main;
		Vector3 center = cam.transform.position;
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		Vector3[] output = new Vector3[4];
		output [0] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
		output [1] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
		output [2] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
		output [3] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
		return output;
	}

	protected class Corner {
		public Vector3 position;
		public Util.COLOR color;
//		public int value;
		public Generic g;

		public Corner(Vector3 _position, Util.COLOR _color) {
			position = _position;
			color = _color;
		}
	}

	protected class Generic {

	}

//    public static LifeOverlay instance = null;

//    public MenuController menuController;
//	public Util.PLAYER player;
//    public Util.COLOR color;

//    public GameObject[][] lives;
//    public GameObject lifePrefab;

//	public Dictionary<Util.COLOR,int> scores = new Dictionary<Util.COLOR, int>();
//	public List<Util.COLOR> teamLog  = new List<Util.COLOR>();
//    public GameObject[] scoreDisplays;
//    public GameObject scoreObject;

//    public Vector3[] markers;

//    public float delay = 0.6f;
//    public float buffer = 0.2f;
//    public float scaleUp = 1.5f;

//    private Vector3 _origScale;
//    private Vector3 _largeScale;
//    private Vector3 _curScale;

//    int maxLives;
//    int maxScore;
//
//	float edgeUpBuffer = 2;
//	float edgeSideBuffer = 2;
//
//	enum types { LIVES, SCORE };
//	types type;
//	GameObject lifePrefab;
//	List<GameObject> lives;
    
    // Gets marker positions
    //void Start ()
    //{
        // Markers numbered from left to right, then from top to bottom
        //markers = new Vector3[4];
        //for (int i = 0; i < 4; i++)
        //{
        //    markers[i] = transform.GetChild(i).transform.position;
        //}
    //}
	
    // Accepts List of Players, Max Lives
//    public void Init(List<Player> players, int _maxLives)
//    {
//		commonInit ();
//		lifePrefab = Resources.Load ("Prefabs/LifePrefab") as GameObject;
//		type = types.LIVES;
//        // Remembers total lives for use later
//		maxLives = _maxLives;

        // Instantiates array of life sprites
//        lives = new GameObject[players.Count][];
//        for (int i = 0; i < players.Count; i++)
//        {
//            lives[i] = new GameObject[players[i].lives];
//        }

//        float displacement = 15f;

        // For each player:
//        for (int i = 0; i < players.Count; ++i)
//        {
            // Gets position of current child's empty position marker
//            Vector3 origPos = markers[i];

            // Populates life icons at start
//            for (int j = 0; j < maxLives; ++j)
//            {
//				Debug.Log (i + " : " + j);
                // Creates a life icon for currently iterating player
//                GameObject obj = Instantiate(lifeSprite, markers[i], Quaternion.identity) as GameObject;
//				GameObject obj = Instantiate(lifePrefab, Vector3.zero, Quaternion.identity) as GameObject;

                // Checks for even or odd index and translates life icons in the correct direction
//				if (i % 2 == 0) {
//                    obj.GetComponent<RectTransform>().position += new Vector3(displacement * j, 0, 0);
//					obj.transform.Translate(new Vector3 (displacement * j, 0, 0));
//				}
//				else {
//                    obj.GetComponent<RectTransform>().position -= new Vector3(displacement * j, 0, 0);
//					obj.transform.Translate(new Vector3 (-displacement * j, 0, 0));
//				}

                // Inserts the new object into the lives array
//                lives[i][j] = obj;

//                _curScale = obj.transform.localScale;
//                _origScale = _curScale;
//                _largeScale = _origScale * scaleUp;

                // Setting game object scale and color
//                obj.transform.localScale = _largeScale;

//                StartCoroutine(FadeSequence(lives[i][j], _origScale, j));

                // Sets new object's parent to be this transform
//                obj.transform.SetParent(transform, false);
//            }
//        }
//    }

//	public void Init(List<Util.COLOR> teams, int scoreTotal)
//    {
//		commonInit ();
//		type = types.SCORE;
//        // Remembers max score for use later
//        maxScore = scoreTotal;
//
//        // Initializes each player's starting score
//		scores[Util.COLOR.BLUE] = 0;
//		scores[Util.COLOR.RED] = 0;
//		scores[Util.COLOR.YELLOW] = 0;
//		scores[Util.COLOR.GREEN] = 0;
//        
//        int counter = 0;
//        
//        // Instantiates all score displays in order corner
//		foreach (Util.COLOR t in teams)
//        {
//            teamLog.Add(t);
//            switch (t)
//            {
//			case Util.COLOR.BLUE:
//                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
//                scoreDisplays[counter].transform.SetParent(transform, false);
//                counter++;
//                    break;
//			case Util.COLOR.RED:
//                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
//                scoreDisplays[counter].transform.SetParent(transform, false);
//                counter++;
//                break;
//			case Util.COLOR.YELLOW:
//                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
//                scoreDisplays[counter].transform.SetParent(transform, false);
//                counter++;
//                break;
//			case Util.COLOR.GREEN:
//                scoreDisplays[counter] = (GameObject)Instantiate(scoreObject, markers[counter], Quaternion.identity);
//                scoreDisplays[counter].transform.SetParent(transform, false);
//                break;
//            }
//        }
//    }

//    // Removes life sprites down to given value
//    public void UpdateUI(int player, int value)
//    {
//        for (int i = maxLives; i > value; i++)
//        {
//            lives[player][i].SetActive(false);
//        }
//    }

//    // Changes score display to given value
//	public void UpdateUI(Util.COLOR team, int value)
//    {
//		foreach (Util.COLOR t in teamLog)
//        {
//            if (team == t)
//            {
//                scoreDisplays[teamLog.IndexOf(t)].GetComponent<TextMesh>().text = value.ToString();
//            }
//        }
//    }

//	void Update() {
//		Vector3[] corners = caculateCorners ();
//		switch (type) {
//		case types.LIVES:
//			break;
//		case types.SCORE:
//			break;
//		}
//	}

//	void commonInit() {
//		markers = new Vector3[4];
//		for (int i = 0; i < 4; i++)
//		{
//			markers[i] = transform.GetChild(i).transform.position;
//			markers[i] = new Vector3();
//		}
//		markers = cornerPoints();
//		transform.parent = Camera.main.transform;
//	}

//	Vector3[] caculateCorners() {
//		Camera cam = Camera.main;
//		Vector3 center = cam.transform.position;
//		float height = 2f * cam.orthographicSize;
//		float width = height * cam.aspect;
//		Vector3[] output = new Vector3[4];
//		output [0] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
//		output [1] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y - height / 2f + edgeUpBuffer, 0f);
//		output [2] = new Vector3 (center.x - width / 2f + edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
//		output [3] = new Vector3 (center.x + width / 2f - edgeSideBuffer, center.y + height / 2f - edgeUpBuffer, 0f);
//		return output;
//	}

//    IEnumerator FadeSequence(GameObject obj, Vector3 origScale, float i)
//    {
//        //delay = (3 / gameController.maxLives);
//
//        // Staggers animations down the chain
//        yield return new WaitForSeconds(delay * i);
//        obj.transform.DOScale(origScale, delay);
//        DOTween.ToAlpha(() => obj.GetComponent<SpriteRenderer>().color, x => obj.GetComponent<SpriteRenderer>().color = x, 1.0f, delay);
//    }
}
