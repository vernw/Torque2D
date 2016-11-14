using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Wheel : MonoBehaviour {

    [SerializeField]
    List<string> wheelElements = new List<string>();
    List<GameObject> wheelObjects = new List<GameObject>();

    public GameObject wheelElementPrefab;
    public string selection;
    float elementDisplacement;
    float spinTime = 0.15f;
    
    void Start () {
        if (wheelElements.Count != 0)
            elementDisplacement = 360 / wheelElements.Count;
        InitWheelElements();
	}
	
	void InitWheelElements()
    {
        for (int i = 0; i < wheelElements.Count; ++i)
        {
            GameObject element = Instantiate(wheelElementPrefab, transform.position, transform.rotation) as GameObject;
            element.transform.rotation *= Quaternion.Euler(0, elementDisplacement * i, 0);
            element.name = wheelElements[i];
            element.transform.SetParent(transform);
            element.GetComponent<TextMesh>().text = wheelElements[i];
            wheelObjects.Add(element);
        }

        selection = wheelElements[0];
    }

    public void spinRight()
    {
        StartCoroutine(Right());
    }

    public void spinLeft()
    {
        StartCoroutine(Left());
    }

    public IEnumerator Right()
    {
        Transform wheel = transform.parent.GetChild(0).transform;
        wheel.DORotate(wheel.rotation.eulerAngles + new Vector3(0, elementDisplacement, 0), spinTime);
        yield return new WaitForSeconds(spinTime);
    }

    public IEnumerator Left()
    {
        Transform wheel = transform.parent.GetChild(0).transform;
        wheel.DORotate(wheel.rotation.eulerAngles + new Vector3(0, -elementDisplacement, 0), spinTime);
        yield return new WaitForSeconds(spinTime);
    }

    public string GetSelection()
    {
        return selection;
    }
}
