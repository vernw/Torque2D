using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Countdown : MonoBehaviour {

    private GameObject ct3;
    private GameObject ct2;
    private GameObject ct1;
    private GameObject fight;

    void Start () {
        ct3 = transform.GetChild(0).gameObject;
        ct2 = transform.GetChild(1).gameObject;
        ct1 = transform.GetChild(2).gameObject;
        fight = transform.GetChild(3).gameObject;

        Count();
    }

    void Count()
    {
        foreach(Transform child in transform)
        {
            StartCoroutine(Fade(child));
        }
        gameObject.SetActive(false);
    }

    IEnumerator Fade(Transform child)
    {
        SpriteRenderer sprRend = child.GetComponent<SpriteRenderer>();
        sprRend.DOFade(255, 0.15f);
        yield return new WaitForSeconds(0.15f);
        sprRend.DOFade(0, 0.85f);
        yield return new WaitForSeconds(0.85f);
    }
}
