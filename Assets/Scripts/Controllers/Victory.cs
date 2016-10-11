using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
    
    public static IEnumerator EndGame(GameObject team, Color color)
    {
        print("EndGame " + team.name.Substring(4, 1));

        yield return new WaitForSeconds(2.5f);

        GameObject victoryPrefab = (GameObject)Resources.Load("Prefabs/VictoryTeam", typeof(GameObject));
        GameObject victoryGO = (GameObject)Instantiate(victoryPrefab, new Vector2 (0, 0), Quaternion.identity);

        TextMesh victoryPlayerTextMesh = victoryGO.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryGO.transform.GetChild(2).GetComponent<TextMesh>();
        string teamName = team.name;

        victoryNumberTextMesh.text = teamName.Substring(4, 1);
        victoryNumberTextMesh.color = color;
        victoryPlayerTextMesh.color = color;
    }

    public static IEnumerator EndGame(int winner, Color color)
    {
        print("EndGame " + winner);

        yield return new WaitForSeconds(2.5f);

        GameObject victoryPrefab = (GameObject)Resources.Load("Prefabs/VictoryPlayer", typeof(GameObject));
        GameObject victoryGO = (GameObject)Instantiate(victoryPrefab, new Vector2(0, 0), Quaternion.identity);
        
        TextMesh victoryPlayerTextMesh = victoryGO.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryGO.transform.GetChild(2).GetComponent<TextMesh>();

        victoryNumberTextMesh.text = winner.ToString();
        victoryNumberTextMesh.color = color;
        victoryPlayerTextMesh.color = color;
        }
    }
