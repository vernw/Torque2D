using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {

    public MenuController menuController;
    public MenuController.player player;

    SpriteRenderer frameRenderer;
    SpriteRenderer playerRenderer;

    public Color blue, red, yellow, green;

    private int _curSelection;
    public int curSelection
    {
        get { return _curSelection; }
        set
        {
            // Loops from 0-4
            // 1 - blue, 2 - red, 3 - yellow, 4 - green
            if (_curSelection < 1)
                _curSelection = 4;
            else if (_curSelection > 4)
                _curSelection = 1;
            else
                _curSelection = value;

            // Sets new frame and player color
            switch (curSelection)
            {
                case 1:
                    frameRenderer.color = blue;
                    playerRenderer.color = blue;
                    menuController.players[player] = MenuController.color.BLUE;
                    break;
                case 2:
                    frameRenderer.color = red;
                    playerRenderer.color = red;
                    menuController.players[player] = MenuController.color.RED;
                    break;
                case 3:
                    frameRenderer.color = yellow;
                    playerRenderer.color = yellow;
                    menuController.players[player] = MenuController.color.YELLOW;
                    break;
                case 4:
                    frameRenderer.color = green;
                    playerRenderer.color = green;
                    menuController.players[player] = MenuController.color.GREEN;
                    break;
            }

            foreach (var pair in menuController.players) { print(pair.Key + " : " + pair.Value); }
        }
    }

	// Use this for initialization
	public void Init () {
        menuController = MenuController.instance;

        frameRenderer = transform.parent.GetChild(0).GetComponent<SpriteRenderer>();
        playerRenderer = transform.parent.GetChild(1).GetComponent<SpriteRenderer>();

        // Default color
        switch (menuController.players[player])
        {
            case MenuController.color.BLUE:
                frameRenderer.color = blue;
                playerRenderer.color = blue;
                break;
            case MenuController.color.RED:
                frameRenderer.color = red;
                playerRenderer.color = red;
                break;
            case MenuController.color.YELLOW:
                frameRenderer.color = yellow;
                playerRenderer.color = yellow;
                break;
            case MenuController.color.GREEN:
                frameRenderer.color = green;
                playerRenderer.color = green;
                break;
        }
    }

    public void CycleRight()
    {
        print("Right");
        curSelection++;
        /*
        if (frameRenderer.color == blue)
            frameRenderer.color = red;
        if (frameRenderer.color == red)
            frameRenderer.color = yellow;
        if (frameRenderer.color == yellow)
            frameRenderer.color = green;
        if (frameRenderer.color == green)
            frameRenderer.color = blue;
            */
    }

    public void CycleLeft()
    {
        print("Left");
        curSelection--;
        /*
        if (frameRenderer.color == blue)
            frameRenderer.color = green;
        if (frameRenderer.color == green)
            frameRenderer.color = yellow;
        if (frameRenderer.color == yellow)
            frameRenderer.color = red;
        if (frameRenderer.color == red)
            frameRenderer.color = blue;
            */
    }
}
