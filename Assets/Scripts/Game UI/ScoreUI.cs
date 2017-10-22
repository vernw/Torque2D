using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    Dictionary<Util.PLAYER, GameObject> corners;
    int goal;

    public virtual void Initialize(Dictionary<Util.PLAYER, Util.COLOR> playerColors, int _goal)
    {
        corners = new Dictionary<Util.PLAYER, GameObject>();
        corners[Util.PLAYER.ONE] = playerOne;
        corners[Util.PLAYER.TWO] = playerTwo;
        corners[Util.PLAYER.THREE] = playerThree;
        corners[Util.PLAYER.FOUR] = playerFour;
        goal = _goal;
        foreach (KeyValuePair<Util.PLAYER, Util.COLOR> p in playerColors)
        {
            corners[p.Key].GetComponent<Text>().color = Util.ConvertColor(p.Value);
            corners[p.Key].GetComponent<Text>().text = "0";
        }
        transform.parent = Camera.main.transform;
    }

    public void UpdateScore(Util.PLAYER player, int score)
    {
        corners[player].GetComponent<Text>().text = "" + score;
    }
}
