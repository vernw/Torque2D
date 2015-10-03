using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Button : MonoBehaviour {

    private Sequence OverSequence;
    private Sequence ExitSequence;
    private float zDefault;
    private float zOffset;

    private bool mouseOver = false;

    void Start()
    {
        DOTween.Init();

        zDefault = transform.position.z;
        zOffset = transform.position.z - 1;
    }

    void OnMouseOver()
    {
        mouseOver = true;
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }

    void Update()
    {
        if (mouseOver)
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
