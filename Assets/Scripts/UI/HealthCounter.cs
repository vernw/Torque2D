using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HealthCounter : MonoBehaviour {

    private Color _color;

	// Use this for initialization
	void Start () {
        DOTween.Init();
        _color = GetComponent<TextMesh>().color;
    }
	
	// Update is called once per frame
	void Update () {
        _color = new Color(_color.r, _color.g, _color.b, _color.a--);
	}
}
