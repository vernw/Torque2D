using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    private string _selection;
    public string selection
    {
        get { return _selection; }
        set
        {
            // Removes last selection and updates with new selection
            GameObject.FindGameObjectWithTag(_selection).GetComponent<Button>().selected = false;
            _selection = value;
            GameObject.FindGameObjectWithTag(value).GetComponent<Button>().selected = true;
        }
    }
    
    void Start()
    {
        selection = "Play";
    }
}
