using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (mainCamera.orthographic)
            transform.LookAt(transform.position - mainCamera.transform.forward, mainCamera.transform.up);
        else
            transform.LookAt(mainCamera.transform.position, mainCamera.transform.up);
    }
}
