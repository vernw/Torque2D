using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameBumper : MonoBehaviour {

    Frame frame;

    void Start()
    {
        frame = transform.parent.GetChild(0).GetComponent<Frame>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (name == "Left Bumper" && col != null)
        {
            frame.CycleLeft();
        }
        else if (name == "Right Bumper" && col != null)
        {
            frame.CycleRight();
        }
    }
}
