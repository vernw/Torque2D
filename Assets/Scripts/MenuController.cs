using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MenuController : MonoBehaviour {

    public static MenuController instance = null;

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

    private Vector3 _menuCamPos;
    private Vector3 _optsCamPos;
    private Vector3 _setsCamPos;

    private Quaternion menuCamRot;
    private Quaternion optsCamRot;
    private Quaternion setsCamRot;

    void Awake()
    {
        // Ensures singleton status of MenuController
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Maintains persistence through loading scenes
        DontDestroyOnLoad(gameObject);
    }

    // Initializes menu screen position
    void Start()
    {
        _menuCamPos = menuCamera.transform.position;
        _optsCamPos = optsCamera.transform.position;
        _setsCamPos = setsCamera.transform.position;

        menuCamRot = menuCamera.transform.rotation;
        optsCamRot = optsCamera.transform.rotation;
        setsCamRot = setsCamera.transform.rotation;
    }

    // Translates menu camera target menu screen
    public IEnumerator MoveTo(string target)
    {
        switch (target)
        {
            case "menu":
                mainCamera.transform.DOMove(_menuCamPos, 1.0f);
                mainCamera.transform.DORotate(menuCamRot.eulerAngles, 1);
                break;
            case "opts":
                mainCamera.transform.DOMove(_optsCamPos, 1.0f);
                mainCamera.transform.DORotate(optsCamRot.eulerAngles, 1);
                break;
            case "sets":
                mainCamera.transform.DOMove(_setsCamPos, 1.0f);
                mainCamera.transform.DORotate(setsCamRot.eulerAngles, 1);
                break;
        }
        yield return null;
    }

    /*
    // Scene Loader
    public IEnumerator LoadScene(string sceneName, string music)
    {

        // Fade to black
        yield return StartCoroutine(blackness.FadeInAsync());

        // Load loading screen
        yield return Application.LoadLevelAsync("LoadingScreen");

        // !!! unload old screen (automatic)

        // Fade to loading screen
        yield return StartCoroutine(m_blackness.FadeOutAsync());

        float endTime = Time.time + m_minDuration;

        // Load level async
        yield return Application.LoadLevelAdditiveAsync(sceneName);

        if (Time.time < endTime)

            yield return new WaitForSeconds(endTime - Time.time);

        // Load appropriate zone's music based on zone data
        AudioController.PlayMusic(music);

        // Fade to black
        yield return StartCoroutine(m_blackness.FadeInAsync());

        // !!! unload loading screen
        LoadingSceneManager.UnloadLoadingScene();

        // Fade to new screen
        yield return StartCoroutine(m_blackness.FadeOutAsync());
    }
    */
}
