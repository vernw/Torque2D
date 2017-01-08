using UnityEngine;
using System.Collections;

public class Breadcrumbs : MonoBehaviour {

    public MenuController menuController;

    TextMesh gameModeText;
    TextMesh stageText;

	void Start () {
        menuController = MenuController.instance;

        gameModeText = gameObject.transform.GetChild(1).GetComponent<TextMesh>();
        stageText = gameObject.transform.GetChild(2).GetComponent<TextMesh>();
    }

    public void UpdateBreadcrumbs()
    {
        gameModeText.text = menuController.GetGameMode();
    }
}
