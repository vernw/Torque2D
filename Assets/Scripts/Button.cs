using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Button : MonoBehaviour {

    private Sequence OverSequence;
    private Sequence ExitSequence;
    private float zDefault;
    private float zOffset;
    
    private string btnTag;

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

        zDefault = transform.position.z;
        zOffset = transform.position.z - 1;

        btnTag = gameObject.tag;
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
        if (btnTag == "Play")
        {
            print("Play");
            // Start match
        }
        if (btnTag == "Options")
        {
            print("Options");
            // Go to match options
        }
        if (btnTag == "Settings")
        {
            print("Settings");
            // Go to game settings
        }
        if (btnTag == "Quit")
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
            OverSequence.Append(transform.DOMoveZ(zOffset, 0.7f))
                .Join(transform.DOScale(1.2f, 0.5f));
        }
        else
        {
            ExitSequence.Append(transform.DOMoveZ(zDefault, 0.4f))
                .Join(transform.DOScale(1.0f, 0.25f));
        }
    }
}
