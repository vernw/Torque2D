using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FadeIn : MonoBehaviour
{
    public float delay = 0.6f;
    public float buffer = 0.2f;
    public float scaleUp = 2;

    private Vector3 _origScale;
    private Vector3 _largeScale;
    private Vector3 _curScale;
    private Color _clear = new Color(1, 1, 1, 0);

    private GameObject _curChild;

    // Use this for initialization
    void Start()
    {
		DoFadeIn();
    }

	public void DoFadeIn() {
		for (int i = 0; i < transform.childCount; ++i)
		{
			// Instantiating references
			_clear = new Color(transform.GetChild(i).transform.GetComponent<SpriteRenderer>().color.r, transform.GetChild(i).transform.GetComponent<SpriteRenderer>().color.g, transform.GetChild(i).transform.GetComponent<SpriteRenderer>().color.b, 0);

			_curScale = transform.GetChild(i).localScale;
			_origScale = _curScale;
			_largeScale = _origScale * scaleUp;

			_curChild = transform.GetChild(i).gameObject;

			// Setting game object scale and color
			_curChild.transform.localScale = _largeScale;
			_curChild.GetComponent<SpriteRenderer>().color = _clear;

			StartCoroutine(FadeSequence(_curChild, _origScale, i + buffer));
		}
	}

    IEnumerator FadeSequence(GameObject curChild, Vector3 origScale, float i)
    {
        // Staggers animations down the chain
        yield return new WaitForSeconds(delay * i);
        curChild.transform.DOScale(origScale, delay);
        DOTween.ToAlpha(() => curChild.GetComponent<SpriteRenderer>().color, x => curChild.GetComponent<SpriteRenderer>().color = x, 1.0f, delay);
    }
}
