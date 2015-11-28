using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HealthCounter : MonoBehaviour {

    private Color color;

	// Use this for initialization
	void Start () {
        DOTween.Init();
        color = GetComponent<TextMesh>().color;
    }
	
	// Update is called once per frame
	void Update () {
        color = new Color(color.r, color.g, color.b, color.a--);
	}
}
