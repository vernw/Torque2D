using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Wall : MonoBehaviour {

    public float defaultFade;

    void Start()
    {
        DOTween.Init();

        defaultFade = gameObject.GetComponent<SpriteRenderer>().color.a;
    }

    // Pulses when avatars collide with wall segments
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "P1" || coll.gameObject.tag == "P2")
        {
            StartCoroutine(Pulse());
        }
    }

    IEnumerator Pulse()
    {
        print("fadeup");
        GetComponent<SpriteRenderer>().DOFade(100, 1.0f).WaitForCompletion();
        GetComponent<SpriteRenderer>().DOFade(defaultFade, 2.0f).WaitForCompletion();
        print("fadeback");
        yield return null;
    }
}
