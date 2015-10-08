﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    [SerializeField]
    private int _livesP1;
    public int livesP1
    {
        get { return _livesP1; }
        set
        {
            if (value <= 0)
            {
                _livesP2 = 0;
                P1.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P1.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
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
                P1.GetComponent<Avatar>().invincible = true;
                StartCoroutine(P2.transform.parent.transform.GetChild(0).GetComponent<Avatar>().Destruct());
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
            if (value < 0)
                _livesP3 = 0;
            else if (value == 0)
                StartCoroutine(P3.transform.GetChild(0).GetComponent<Avatar>().Destruct());
            else if (value > 10)
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
            if (value < 0)
                _livesP4 = 0;
            else if (value == 0)
                StartCoroutine(P4.transform.GetChild(0).GetComponent<Avatar>().Destruct());
            else if (value > 10)
                _livesP4 = 10;
            else
                _livesP4 = value;
        }
    }*/

    public bool blackHoles;
    public bool whiteHoles;
    public bool gravityFields;
    public bool depthCharges;
    public bool powerUps;

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

    void Start () {
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

    void GameEnd(int winner)
    {
        // Victory screens
    }

    void Update()
    {
        if (livesP1 == 0)
        {
            GameEnd(2);
        }
        if (livesP2 == 0)
        {
            GameEnd(1);
        }
    }
}
