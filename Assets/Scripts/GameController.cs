using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    private Color _P1Color;
    private Color _P2Color;
    private Color _P3Color;
    private Color _P4Color;

    public int maxLives = 5;
    public bool countdown = true;
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
                _livesP1 = 0;
                P1.GetComponent<Avatar>().alive = false;
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
                _livesP2 = 0;
                P2.GetComponent<Avatar>().alive = false;
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
    [SerializeField]
    private int _livesP3;
    public int livesP3
    {
        get { return _livesP3; }
        set
        {
            if (value <= 0)
            {
                _livesP3 = 0;
                P3.GetComponent<Avatar>().alive = false;
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
                _livesP4 = 0;
                P4.GetComponent<Avatar>().alive = false;
                P4.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P4.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
                totalPlayers--;
            }
            if (value > 10)
                _livesP4 = 10;
            else
                _livesP4 = value;
        }
    }

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

    void Awake()
    {
        // Ensures singleton status of GameController
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Maintains persistence through loading scenes
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        totalPlayers = 4;
        livesP1 = maxLives;
        livesP2 = maxLives;
        livesP3 = maxLives;
        livesP4 = maxLives;

        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");
        P3 = GameObject.FindGameObjectWithTag("P3");
        P4 = GameObject.FindGameObjectWithTag("P4");

        _P1Color = P1.GetComponent<SpriteRenderer>().color;
        _P2Color = P2.GetComponent<SpriteRenderer>().color;
        _P3Color = P3.GetComponent<SpriteRenderer>().color;
        _P4Color = P4.GetComponent<SpriteRenderer>().color;

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
        // Display victory screen
        int winnerNumber = 0;
        TextMesh victoryPlayerTextMesh = victoryScreen.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh victoryNumberTextMesh = victoryScreen.transform.GetChild(2).GetComponent<TextMesh>();

        yield return new WaitForSeconds(2.5f);

        // Check for final player
        if (livesP1 > 0)
        {
            winnerNumber = 1;
        }
        else if (livesP2 > 0)
        {
            winnerNumber = 2;
        }
        else if (livesP3 > 0)
        {
            winnerNumber = 3;
        }
        else if (livesP4 > 0)
        {
            winnerNumber = 4;
        }

        // Victory screens
        switch (winnerNumber)
        {
            case 1:
                victoryNumberTextMesh.text = "1";
                victoryNumberTextMesh.color = _P1Color;
                victoryPlayerTextMesh.color = _P1Color;
                break;
            case 2:
                victoryNumberTextMesh.text = "2";
                victoryNumberTextMesh.color = _P2Color;
                victoryPlayerTextMesh.color = _P2Color;
                break;
            case 3:
                victoryNumberTextMesh.text = "3";
                victoryNumberTextMesh.color = _P3Color;
                victoryPlayerTextMesh.color = _P3Color;
                break;
            case 4:
                victoryNumberTextMesh.text = "4";
                victoryNumberTextMesh.color = _P4Color;
                victoryPlayerTextMesh.color = _P4Color;
                break;
        }
        victoryScreen.SetActive(true);
    }

    // Called by VictoryScreen.cs to reset game values if necessary - unused
    public void gameReset()
    {
        Debug.Log("Reset!");

        countdown = true;

        // Resets game start values
        totalPlayers = 4;
        livesP1 = maxLives;
        livesP2 = maxLives;
        livesP3 = maxLives;
        livesP4 = maxLives;

        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");
        P3 = GameObject.FindGameObjectWithTag("P3");
        P4 = GameObject.FindGameObjectWithTag("P4");

        _P1Color = P1.GetComponent<SpriteRenderer>().color;
        _P2Color = P2.GetComponent<SpriteRenderer>().color;
        _P3Color = P3.GetComponent<SpriteRenderer>().color;
        _P4Color = P4.GetComponent<SpriteRenderer>().color;
    }
}
