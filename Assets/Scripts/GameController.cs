using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject victoryScreen;

    /*** Player Count ***/
    [SerializeField]
    private int _totalPlayers;
    public int totalPlayers
    {
        get { return _totalPlayers; }
        set
        {
            if (value <= 1)
            {
                _totalPlayers = 1;
                StartCoroutine(GameEnd());
            }
            if (value > 4)
                _totalPlayers = 4;
            else
                _totalPlayers = value;
        }
    }

    /*** Players ***/
    [SerializeField]
    private int _livesP1;
    public int livesP1
    {
        get { return _livesP1; }
        set
        {
            if (value <= 0)
            {
                P1.GetComponent<Avatar>().alive = false;
                _livesP1 = 0;
                P1.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P1.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
                totalPlayers--;
            }
            if (value > 10)
                _livesP1 = 10;
            else
                _livesP1 = value;
        }
    }
    [SerializeField]
    private int _livesP2;
    public int livesP2
    {
        get { return _livesP2; }
        set
        {
            if (value <= 0)
            {
                P2.GetComponent<Avatar>().alive = false;
                _livesP2 = 0;
                P2.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P2.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
                totalPlayers--;
            }
            if (value > 10)
                _livesP2 = 10;
            else
                _livesP2 = value;
        }
    }
    /*
    [SerializeField]
    private int _livesP3;
    public int livesP3
    {
        get { return _livesP3; }
        set
        {
            if (value <= 0)
            {
                P3.GetComponent<Avatar>().alive = false;
                _livesP3 = 0;
                P3.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P3.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
                totalPlayers--;
            }
            if (value > 10)
                _livesP3 = 10;
            else
                _livesP3 = value;
        }
    }
    [SerializeField]
    private int _livesP4;
    public int livesP4
    {
        get { return _livesP4; }
        set
        {
            if (value <= 0)
            {
                P4.GetComponent<Avatar>().alive = false;
                _livesP4 = 0;
                P4.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P4.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
                totalPlayers--;
            }
            if (value > 10)
                _livesP4 = 10;
            else
                _livesP4 = value;
        }
    }*/

    /*** Event Toggles ***/
    public bool blackHoles;
    public bool whiteHoles;
    public bool gravityFields;
    public bool depthCharges;
    public bool powerUps;

    /*** Audio ***/
    private int _musicVolume;
    public int musicVolume
    {
        get { return musicVolume; }
        set
        {
            if (value < 0)
                _musicVolume = 0;
            else if (value > 100)
                _musicVolume = 100;
            else
                _musicVolume = value;
        }
    }
    private int _effectsVolume;
    public int effectsVolume
    {
        get { return _effectsVolume; }
        set
        {
            if (value < 0)
                _effectsVolume = 0;
            else if (value > 100)
                _effectsVolume = 100;
            else
                _effectsVolume = value;
        }
    }

    void Start ()
    {
        totalPlayers = 2;
        livesP1 = 3;
        livesP2 = 3;
        //livesP3 = 5;
        //livesP4 = 5;

        blackHoles = false;
        whiteHoles = false;
        gravityFields = false;
        depthCharges = false;
        powerUps = false;

        musicVolume = 50;
        effectsVolume = 50;
	}

    IEnumerator GameEnd()
    {
        int winnerNumber = 0;

        yield return new WaitForSeconds(4.5f);

        // Check for final player
        if (livesP1 > 0)
        {
            winnerNumber = 1;
        }
        else if (livesP2 > 0)
        {
            winnerNumber = 2;
        }
        /*
        else if (livesP3 > 0)
        {
            winnerNumber = "3";
        }
        else if (livesP4 > 0)
        {
            winnerNumber = "4";
        }*/

        // Victory screens
        switch (winnerNumber)
        {
            case 1:
                victoryScreen.transform.GetChild(2).GetComponent<TextMesh>().text = "1";
                break;
            case 2:
                victoryScreen.transform.GetChild(2).GetComponent<TextMesh>().text = "2";
                break;
            case 3:
                victoryScreen.transform.GetChild(2).GetComponent<TextMesh>().text = "3";
                break;
            case 4:
                victoryScreen.transform.GetChild(2).GetComponent<TextMesh>().text = "4";
                break;
        }
        victoryScreen.SetActive(true);
    }
}
