using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Countdown : MonoBehaviour {

    private GameObject ct3;
    private GameObject ct2;
    private GameObject ct1;
    private GameObject ctfight;

    public float countdownDelay = 0.5f;

    void Start ()
    {
        // Sets count variables
        ct3 = transform.GetChild(0).gameObject;
        ct2 = transform.GetChild(1).gameObject;
        ct1 = transform.GetChild(2).gameObject;
        ctfight = transform.GetChild(3).gameObject;

        Invoke("Count", countdownDelay);
    }

    // Consecutively fades each count number
    public void Count()
    {
        StartCoroutine(FadeOut(ct3.transform, 0.0f));
        StartCoroutine(FadeOut(ct2.transform, 1.0f));
        StartCoroutine(FadeOut(ct1.transform, 2.0f));
        StartCoroutine(FadeOut(ctfight.transform, 3.0f));
    }

    public IEnumerator FadeOut(Transform currentCount, float delayTime)
    {
        // Delays the next countdown number
        yield return new WaitForSeconds(delayTime);

        // Activates avatars after countdown finishes
        if (currentCount == ctfight.transform)
        {
            GameController.instance.countdown = false;
        }

        // Fade tweens
        TextMesh textMesh = currentCount.GetComponent<TextMesh>();
        DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, 1.0f, 0.15f);
        yield return new WaitForSeconds(0.15f);
        DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, 0.0f, 0.85f);
        yield return new WaitForSeconds(0.85f);
        currentCount.gameObject.SetActive(false);
    }
}
