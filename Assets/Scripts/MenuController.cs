using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MenuController : MonoBehaviour {

    public Camera mainCamera;
    public Camera menuCamera;
    public Camera optsCamera;
    public Camera setsCamera;

    private string _selection = "Play";
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

    private Vector3 menuCamPos;
    private Vector3 optsCamPos;
    private Vector3 setsCamPos;

    private Quaternion menuCamRot;
    private Quaternion optsCamRot;
    private Quaternion setsCamRot;

    void Start()
    {
        menuCamPos = menuCamera.transform.position;
        optsCamPos = optsCamera.transform.position;
        setsCamPos = setsCamera.transform.position;

        menuCamRot = menuCamera.transform.rotation;
        optsCamRot = optsCamera.transform.rotation;
        setsCamRot = setsCamera.transform.rotation;
    }

    public IEnumerator MoveTo(string target)
    {
        switch (target)
        {
            case "menu":
                mainCamera.transform.DOMove(menuCamPos, 1.0f);
                mainCamera.transform.DORotate(menuCamRot.eulerAngles, 1);
                break;
            case "opts":
                mainCamera.transform.DOMove(optsCamPos, 1.0f);
                mainCamera.transform.DORotate(optsCamRot.eulerAngles, 1);
                break;
            case "sets":
                mainCamera.transform.DOMove(setsCamPos, 1.0f);
                mainCamera.transform.DORotate(setsCamRot.eulerAngles, 1);
                break;
        }
        yield return null;
    }
}
