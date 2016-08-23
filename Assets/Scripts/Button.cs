using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {
    
    public MenuController menuController;

    private Sequence _OverSequence;
    private Sequence _ExitSequence;
    private float _zDefault;
    private float _zOffset;
    
    private string _btnTag;

    private bool _selected;
    public bool selected
    {
        get { return _selected; }
        set
        {
            _selected = value;
            // Add selection marker while selected
        }
    }

    void Start()
    {
        DOTween.Init();
        menuController = MenuController.instance;
        print(menuController.selection);

        _zDefault = transform.position.z;
        _zOffset = transform.position.z - 1;

        _btnTag = gameObject.tag;
    }

    void OnMouseOver()
    {
        selected = true;
    }

    void OnMouseExit()
    {
        selected = false;
    }

    void OnMouseDown()
    {
        if (_btnTag == "Start")
        {
            print("Start");
            // Start match
            SceneManager.LoadScene(1);
        }
        if (_btnTag == "Options")
        {
            print("Options");
            // Go to match options
            StartCoroutine(menuController.MoveTo("opts"));
        }
        if (_btnTag == "Settings")
        {
            print("Settings");
            // Go to game settings
            StartCoroutine(menuController.MoveTo("sets"));
        }
        if (_btnTag == "Back")
        {
            print("Back");
            // Go to main menu
            StartCoroutine(menuController.MoveTo("menu"));
        }
        if (_btnTag == "Quit")
        {
            print("Quit");
            // Exit application
            Application.Quit();
        }
    }

    void Update()
    {
        if (selected)
        {
            // Animation while selected
            _OverSequence.Append(transform.DOMoveZ(_zOffset, 0.7f))
                .Join(transform.DOScale(1.2f, 0.5f));
        }
        else
        {
            // Animation while unselected
            _ExitSequence.Append(transform.DOMoveZ(_zDefault, 0.4f))
                .Join(transform.DOScale(1.0f, 0.25f));
        }
    }
}
