using UnityEngine;
using System.Collections;
using DG.Tweening;

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
        menuController.ButtonInput(_btnTag);
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
