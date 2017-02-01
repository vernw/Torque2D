using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Breadcrumbs : GenericSingletonClass<MenuController> {

    public MenuController menuController;

    GameObject menuText;
    GameObject gameModeText;
    GameObject stageText;

    public int menuTextPos = 0;
    public int gameModeTextPos = 0;
    public int stageTextPos = 0;

    void Start () {
        menuController = MenuController.instance;

        menuText = gameObject.transform.GetChild(0).gameObject;
        gameModeText = gameObject.transform.GetChild(1).gameObject;
        stageText = gameObject.transform.GetChild(2).gameObject;
    }

    public void UpdateBreadcrumbs()
    {
        gameModeText.GetComponent<TextMesh>().text = menuController.GetGameMode();
    }

    public void SlideBreadcrumb(int id)
    {
        StartCoroutine(Slide(id));
    }

    // Toggles whether breadcrumb is on or off camera
    // 1 - Menu, 2 - Game Type, 3 - Stage
    public IEnumerator Slide(int id)
    {
        // print("slide " + id);
        float direction = 150f;

        switch (id)
        {
            // Menu
            case 1:
                if (menuTextPos == 0)
                {
                    yield return new WaitForSeconds(0.8f);
                    menuText.transform.DOMoveX(menuText.transform.position.x + direction, 0.6f);
                    DOTween.ToAlpha(() => menuText.GetComponent<TextMesh>().color, x => menuText.GetComponent<TextMesh>().color = x, 1f, 1.5f);
                    menuTextPos = 1;
                }
                else if (menuTextPos == 1)
                {
                    DOTween.ToAlpha(() => menuText.GetComponent<TextMesh>().color, x => menuText.GetComponent<TextMesh>().color = x, 0.0f, 0.4f);
                    yield return new WaitForSeconds(1.5f);
                    menuText.transform.DOMoveX(menuText.transform.position.x - direction, 0.1f);
                    menuTextPos = 0;
                }
                break;
            // Game type
            case 2:
                if (gameModeTextPos == 0)
                {
                    yield return new WaitForSeconds(0.8f);
                    gameModeText.transform.DOMoveX(gameModeText.transform.position.x + direction, 0.6f);
                    DOTween.ToAlpha(() => gameModeText.GetComponent<TextMesh>().color, x => gameModeText.GetComponent<TextMesh>().color = x, 1f, 1.5f);
                    gameModeTextPos = 1;
                }
                else if (gameModeTextPos == 1)
                {
                    DOTween.ToAlpha(() => gameModeText.GetComponent<TextMesh>().color, x => gameModeText.GetComponent<TextMesh>().color = x, 0.0f, 0.4f);
                    yield return new WaitForSeconds(1.5f);
                    gameModeText.transform.DOMoveX(gameModeText.transform.position.x - direction, 0.1f);
                    gameModeTextPos = 0;
                }
                break;
            // Stage select
            case 3:
                if (stageTextPos == 0)
                {
                    yield return new WaitForSeconds(0.8f);
                    stageText.transform.DOMoveX(stageText.transform.position.x + direction, 0.6f);
                    DOTween.ToAlpha(() => stageText.GetComponent<TextMesh>().color, x => stageText.GetComponent<TextMesh>().color = x, 1f, 1.5f);
                    stageTextPos = 1;
                }
                else if (stageTextPos == 1)
                {
                    DOTween.ToAlpha(() => stageText.GetComponent<TextMesh>().color, x => stageText.GetComponent<TextMesh>().color = x, 0.0f, 0.4f);
                    yield return new WaitForSeconds(1.5f);
                    stageText.transform.DOMoveX(stageText.transform.position.x - direction, 0.1f);
                    stageTextPos = 0;
                }
                break;
        }
    }
}
