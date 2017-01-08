using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Wheel : MonoBehaviour {
    public MenuController menuController;

    [SerializeField]
    List<string> wheelElements = new List<string>();
    List<GameObject> wheelObjects = new List<GameObject>();
    List<Sprite> elementSprites = new List<Sprite>();

    public GameObject wheelElementPrefab;
    
    // Manages sprite swapping
    private Sprite _curSprite;
    public Sprite curSprite
    {
        get { return _curSprite; }
        set
        {
            if (elementSprites != null)
                transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sprite = value;
        }
    }

    public float spinTime = 0.15f;
    private float _elementDisplacement;
    [SerializeField]
    private string _selection;

    // Tracks list index for traversal to adjust selection and image accordingly
    [SerializeField]
    private int _curListIndex = 0;
    public int curListIndex
    {
        get { return _curListIndex; }
        set
        {
            if (value < 0)
                _curListIndex = wheelElements.Count - 1;
            else if (value > wheelElements.Count - 1)
                _curListIndex = 0;
            else
                _curListIndex = value;
        }
    }
    
    void Start () {
        menuController = MenuController.instance;

        if (wheelElements.Count != 0)
            _elementDisplacement = 360 / wheelElements.Count;
        InitWheelElements();
	}
	
	void InitWheelElements()
    {
        for (int i = 0; i < wheelElements.Count; ++i)
        {
            GameObject element = Instantiate(wheelElementPrefab, transform.position, transform.rotation) as GameObject;
            element.transform.rotation *= Quaternion.Euler(0, _elementDisplacement * i, 0);
            element.name = wheelElements[i];
            element.transform.SetParent(transform);
            element.GetComponent<TextMesh>().text = wheelElements[i];
            wheelObjects.Add(element);
        }

        _selection = wheelElements[0];

        // Sets initial sprite
        if (elementSprites != null && elementSprites.Count > 0)
        {
            curSprite = elementSprites[0];
        }
    }

    public void spinRight()
    {
        StartCoroutine(Right());
    }

    public void spinLeft()
    {
        StartCoroutine(Left());
    }

    // Spin counterclockwise
    public IEnumerator Right()
    {
        Transform wheel = transform.parent.GetChild(1).transform;
        wheel.DORotate(wheel.rotation.eulerAngles + new Vector3(0, _elementDisplacement, 0), spinTime);

        curListIndex--;
        _selection = wheelElements[curListIndex];
        if (elementSprites != null && elementSprites.Count != 0)
            curSprite = elementSprites[curListIndex];

        if (gameObject.transform.parent.name == "Game Mode Wheel")
            menuController.ChangeMode(_selection, 'r');

        yield return new WaitForSeconds(spinTime);
    }

    // Spin clockwise
    public IEnumerator Left()
    {
        Transform wheel = transform.parent.GetChild(1).transform;
        wheel.DORotate(wheel.rotation.eulerAngles - new Vector3(0, _elementDisplacement, 0), spinTime);

        curListIndex++;
        _selection = wheelElements[curListIndex];
        if (elementSprites != null && elementSprites.Count != 0)
            curSprite = elementSprites[curListIndex];

        if (gameObject.transform.parent.name == "Game Mode Wheel")
            menuController.ChangeMode(_selection, 'l');

        yield return new WaitForSeconds(spinTime);
    }

    public string GetSelection()
    {
        return _selection;
    }
}
