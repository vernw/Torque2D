using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WheelElement : MonoBehaviour {

    public string modeName;
    public float wheelPosition;
    
    void Start () {
	}
	
    public WheelElement (string newName, float newPosition)
    {
        modeName = newName;
        wheelPosition = newPosition;
    }
}
