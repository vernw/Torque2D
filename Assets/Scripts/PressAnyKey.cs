using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PressAnyKey : MonoBehaviour {

    public MenuController menuController;

    public float duration;
    public bool flashing;

    private TextMesh textMesh;

	// Use this for initialization
	void Start () {
        menuController = MenuController.instance;

        duration = 3.5f;
        flashing = true;
        textMesh = transform.GetComponent<TextMesh>();

        StartCoroutine(FadeSequence());
	}
	
	IEnumerator FadeSequence () {
        DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, 0.0f, duration);
        yield return new WaitForSeconds(duration);
        DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, 1.0f, duration);
        yield return new WaitForSeconds(duration);

        StartCoroutine(FadeSequence());
    }

    void Update()
    {
        if (flashing && Input.anyKey)
        {
            flashing = false;
            StartCoroutine(menuController.MoveTo("menu"));
        }

    }
}
