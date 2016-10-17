using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

/*
 * Singleton Menu Controller that persists through scene loads, holding onto settings data input by players such as desired max lives.
 * Contains camerawork for the animated screen translation in the main menu.
*/

public class MenuController : GenericSingletonClass<MenuController> {

    public Camera titleCamera;
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

    private Vector3 _titleCamPos;
    private Vector3 _menuCamPos;
    private Vector3 _optsCamPos;
    private Vector3 _setsCamPos;

    private Quaternion _titleCamRot;
    private Quaternion _menuCamRot;
    private Quaternion _optsCamRot;
    private Quaternion _setsCamRot;

    public int maxLives = 5;

    // Initializes menu screen position
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _titleCamPos = menuCamera.transform.position;
        _menuCamPos = menuCamera.transform.position;
        _optsCamPos = optsCamera.transform.position;
        _setsCamPos = setsCamera.transform.position;

        _titleCamRot = menuCamera.transform.rotation;
        _menuCamRot = menuCamera.transform.rotation;
        _optsCamRot = optsCamera.transform.rotation;
        _setsCamRot = setsCamera.transform.rotation;
    }

    public void ButtonInput(string input)
    {
        switch (input)
        {
            case "Start":
                print("Start");
                // Start match
                SceneManager.LoadScene("Game2D");
                break;
            case "Options":
                print("Options");
                // Go to match options
                StartCoroutine(MoveTo("opts"));
                break;
            case "Settings":
                print("Settings");
                // Go to game settings
                StartCoroutine(MoveTo("sets"));
                break;
            case "Back":
                print("Back");
                // Go to main menu
                StartCoroutine(MoveTo("menu"));
                break;
            case "Quit":
                print("Quit");
                // Exit application
                Application.Quit();
                break;
        }
    }

    // Translates menu camera target menu screen
    public IEnumerator MoveTo(string target)
    {
        switch (target)
        {
            case "menu":
                mainCamera.transform.DOMove(_menuCamPos, 1.0f);
                mainCamera.transform.DORotate(_menuCamRot.eulerAngles, 1);
                break;
            case "opts":
                mainCamera.transform.DOMove(_optsCamPos, 1.0f);
                mainCamera.transform.DORotate(_optsCamRot.eulerAngles, 1);
                break;
            case "sets":
                mainCamera.transform.DOMove(_setsCamPos, 1.0f);
                mainCamera.transform.DORotate(_setsCamRot.eulerAngles, 1);
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
