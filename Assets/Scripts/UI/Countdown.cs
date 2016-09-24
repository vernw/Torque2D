using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Countdown : MonoBehaviour {

    private GameObject _ct3;
    private GameObject _ct2;
    private GameObject _ct1;
    private GameObject _ctfight;

    public float countdownDelay = 0.5f;

    void Start ()
    {
        // Sets count variables
        _ct3 = transform.GetChild(0).gameObject;
        _ct2 = transform.GetChild(1).gameObject;
        _ct1 = transform.GetChild(2).gameObject;
        _ctfight = transform.GetChild(3).gameObject;

        Invoke("Count", countdownDelay);
    }

    // Consecutively fades each count number
    public void Count()
    {
        StartCoroutine(FadeOut(_ct3.transform, 0.0f));
        StartCoroutine(FadeOut(_ct2.transform, 1.0f));
        StartCoroutine(FadeOut(_ct1.transform, 2.0f));
        StartCoroutine(FadeOut(_ctfight.transform, 3.0f));
    }

    public IEnumerator FadeOut(Transform currentCount, float delayTime)
    {
        // Delays the next countdown number
        yield return new WaitForSeconds(delayTime);

        // Activates avatars after countdown finishes
        if (currentCount == _ctfight.transform)
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

    /*
    // Called from GameController to reset the counter on game restart
    public void resetCounter()
    {
        // Resets all countdown TextMesh colors to white
        Color white = new Color(1, 1, 1, 1);
        _ct3.GetComponent<TextMesh>().color = white;
        _ct2.GetComponent<TextMesh>().color = white;
        _ct1.GetComponent<TextMesh>().color = white;
        _ctfight.GetComponent<TextMesh>().color = white;

        // Reactivates the countdown GameObjects
        _ct3.SetActive(true);
        _ct2.SetActive(true);
        _ct1.SetActive(true);
        _ctfight.SetActive(true);
    }
    */
}
