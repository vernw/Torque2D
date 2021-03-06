﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;
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

    public GameObject[] lifeMarkers;
    public GameObject[] lifeIcons;
    public GameObject[][] lives;

    public float delay = 0.6f;
    public float buffer = 0.2f;
    public float scaleUp = 1.5f;

    private Vector3 _origScale;
    private Vector3 _largeScale;
    private Vector3 _curScale;
    private Color _clear = new Color(1, 1, 1, 0);

    // void Start ()
    // {
        // gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Lives jagged array for [player][lifeID]
        // lives = new GameObject[4][]
        // {
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives]
        // };
        
        // float displacement = 22f;

        // For each player:
        // for (int i = 0; i < gameController.maxPlayers; ++i)
        // {
        //     // Gets position of current child's empty position marker
        //     Vector3 origPos = transform.GetChild(i).transform.position;

        //     // Populates life icons at start
        //     for (int j = 0; j < gameController.maxLives; ++j)
        //     {
        //         // Creates a life icon for currently iterating player
        //         GameObject obj = Instantiate(lifeIcons[i], lifeMarkers[i].GetComponent<RectTransform>().localPosition, Quaternion.identity) as GameObject;

        //         // Checks for even or odd index and translates life icons in the correct direction
        //         if (i % 2 == 0)
        //             obj.GetComponent<RectTransform>().position += new Vector3(displacement * j, 0, 0);
        //         else
        //             obj.GetComponent<RectTransform>().position -= new Vector3(displacement * j, 0, 0);
                
        //         // Puts the new object into the lives array
        //         lives[i][j] = obj;
                
        //         _curScale = obj.transform.localScale;
        //         _origScale = _curScale;
        //         _largeScale = _origScale * scaleUp;

        //         // Setting game object scale and color
        //         obj.transform.localScale = _largeScale;

        //         StartCoroutine(FadeSequence(lives[i][j], _origScale, j));

        //         // Sets new object's parent to be this transform
        //         obj.transform.SetParent(transform, false);
        //     }
        // }
	// }

    public void CustomStart(List<Player> players, int maxLives, bool scoreMode = default(bool)) {
        // gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Lives jagged array for [player][lifeID]
        // lives = new GameObject[4][]
        // {
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives],
        //     new GameObject[gameController.maxLives]
        // };
        if (!scoreMode)
        {
            lives = new GameObject[4][];
            for (int i = 0; i < players.Count; i++)
            {
                lives[i] = new GameObject[players[i].lives];
            }

            float displacement = 22f;

            // For each player:
            for (int i = 0; i < players.Count; ++i)
            // foreach(Player player in players)
            {
                // Gets position of current child's empty position marker
                Vector3 origPos = transform.GetChild(i).transform.position;

                // Populates life icons at start
                for (int j = 0; j < maxLives; ++j)
                {
                    // Creates a life icon for currently iterating player
                    GameObject obj = Instantiate(lifeIcons[i], lifeMarkers[i].GetComponent<RectTransform>().localPosition, Quaternion.identity) as GameObject;

                    // Checks for even or odd index and translates life icons in the correct direction
                    if (i % 2 == 0)
                        obj.GetComponent<RectTransform>().position += new Vector3(displacement * j, 0, 0);
                    else
                        obj.GetComponent<RectTransform>().position -= new Vector3(displacement * j, 0, 0);

                    // Puts the new object into the lives array
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
        else
        {
            // TODO: Change life display to score counters
        }
    }

    // // Called to remove a life
    // public void subtractLife(int targetPlayer, int targetLife)
    // {
    //     // print("LifeOverlay " + targetPlayer + " : " + targetLife);
    //     // print("Arrays " + lives.Length + " : " + lives[targetPlayer].Length);
    //     targetPlayer--;
    //     targetLife--;
    //     if (targetPlayer < 0 || targetPlayer > lives.Length || targetLife < 0 || targetLife > lives[targetPlayer].Length) {
    //         return;
    //     }
    //     Destroy(lives[targetPlayer][targetLife].gameObject);
    //     lives[targetPlayer][targetLife] = null;
    // }

    public void UpdateLife(Player target) {
        int targetPlayer = (int)target.playerType;
        int targetLife = target.lives;
        for(int i = lives[targetPlayer].Length - 1; i >= targetLife; i--) {
            //TODO: fade out icon
            GameObject targetIcon = lives[targetPlayer][targetLife];
            if (targetIcon) {
                Destroy(targetIcon);
            }
            lives[targetPlayer][targetLife] = null;
        }
    }

    IEnumerator FadeSequence(GameObject curObj, Vector3 origScale, float i)
    {
        //delay = (3 / gameController.maxLives);

        // Staggers animations down the chain
        yield return new WaitForSeconds(delay * i);
        curObj.transform.DOScale(origScale, delay);
        DOTween.ToAlpha(() => curObj.GetComponent<Image>().color, x => curObj.GetComponent<Image>().color = x, 1.0f, delay);
    }
}
