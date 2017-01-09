using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

/*
 * Singleton Menu Controller that persists through scene loads, holding onto settings input by players such as desired max lives.
 * Contains camerawork for the animated screen translation in the main menu.
*/

public class MenuController : GenericSingletonClass<MenuController> {

    public Camera titleCamera;
    public Camera mainCamera;
    public Camera menuCamera;
    public Camera gameSelectCamera;
    public Camera teamSelectCamera;
    public Camera stageSelectCamera;
    public Camera trialsCamera;
    public Camera settingsCamera;
    public Camera creditsCamera;

    private Vector3 _titleCamPos;
    private Vector3 _menuCamPos;
    private Vector3 _gameSelectCamPos;
    private Vector3 _teamSelectCamPos;
    private Vector3 _stageSelectCamPos;
    private Vector3 _trialsCamPos;
    private Vector3 _settingsCamPos;
    private Vector3 _creditsCamPos;

    private Quaternion _titleCamRot;
    private Quaternion _menuCamRot;
    private Quaternion _gameSelectCamRot;
    private Quaternion _teamSelectCamRot;
    private Quaternion _stageSelectCamRot;
    private Quaternion _trialsCamRot;
    private Quaternion _settingsCamRot;
    private Quaternion _creditsCamRot;

    public GameObject gameModeWheel;

    public float screenTransitionTime = 0.7f;

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

    public int maxLives = 5;

    // Screens declared as ints to reduce switch case overhead
    public enum screens : int { Title = 1, Menu, GameSelect, TeamSelect, StageSelect, Trials, Settings, Credits, InGame};
    public screens curScreen = screens.Title;

    public enum gameModeSelection { Standard, Headhunter, Oddball, Soccer, King };
    public gameModeSelection curMode = gameModeSelection.Standard;

    public enum teamSelection { Blue, Red, Green, Yellow };

    // Initializes menu screen position
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _titleCamPos = menuCamera.transform.position;
        _menuCamPos = menuCamera.transform.position;
        _gameSelectCamPos = gameSelectCamera.transform.position;
        _teamSelectCamPos = teamSelectCamera.transform.position;
        _stageSelectCamPos = stageSelectCamera.transform.position;
        _trialsCamPos = trialsCamera.transform.position;
        _settingsCamPos = settingsCamera.transform.position;
        _creditsCamPos = creditsCamera.transform.position;

        _titleCamRot = menuCamera.transform.rotation;
        _menuCamRot = menuCamera.transform.rotation;
        _gameSelectCamRot = gameSelectCamera.transform.rotation;
        _teamSelectCamRot = teamSelectCamera.transform.rotation;
        _stageSelectCamRot = stageSelectCamera.transform.rotation;
        _trialsCamRot = trialsCamera.transform.rotation;
        _settingsCamRot = settingsCamera.transform.rotation;
        _creditsCamRot = creditsCamera.transform.rotation;
    }

    public void StartGame()
    {
        print("Start");
        // Start match
        SceneManager.LoadScene("Game2D");
    }

    public void MoveToScreen (Camera targetScreen)
    {
        mainCamera.transform.DOMove(targetScreen.transform.position, screenTransitionTime);
        mainCamera.transform.DORotate(targetScreen.transform.rotation.eulerAngles, screenTransitionTime);
    }

    // Translates menu camera target menu screen; called from Button.cs
    public IEnumerator MoveTo (string input)
    {
        switch (input)
        {
            case "Start":
                print("Start");
                // Start match
                SceneManager.LoadScene("Game2D");
                curScreen = screens.InGame;
                break;
            case "Menu":
                print("Menu");
                // Go to game select
                mainCamera.transform.DOMove(_menuCamPos, 1.0f);
                mainCamera.transform.DORotate(_menuCamRot.eulerAngles, 1);
                break;
            case "GameSelect":
                print("Game Select");
                // Go to game select
                mainCamera.transform.DOMove(_gameSelectCamPos, 1.0f);
                mainCamera.transform.DORotate(_gameSelectCamRot.eulerAngles, 1);
                break;
            case "TeamSelect":
                print("Team Select");
                // Go to team select
                mainCamera.transform.DOMove(_teamSelectCamPos, 1.0f);
                mainCamera.transform.DORotate(_teamSelectCamRot.eulerAngles, 1);
                break;
            case "StageSelect":
                print("Stage Select");
                // Go to stage select
                mainCamera.transform.DOMove(_stageSelectCamPos, 1.0f);
                mainCamera.transform.DORotate(_stageSelectCamRot.eulerAngles, 1);
                break;
            case "Trials":
                print("Trials");
                // Go to game select
                mainCamera.transform.DOMove(_trialsCamPos, 1.0f);
                mainCamera.transform.DORotate(_trialsCamRot.eulerAngles, 1);
                break;
            case "Settings":
                print("Settings");
                // Go to game settings
                mainCamera.transform.DOMove(_settingsCamPos, 1.0f);
                mainCamera.transform.DORotate(_settingsCamRot.eulerAngles, 1);
                break;
            case "Credits":
                print("Credits");
                // Go to game settings
                mainCamera.transform.DOMove(_settingsCamPos, 1.0f);
                mainCamera.transform.DORotate(_settingsCamRot.eulerAngles, 1);
                break;
            case "Back":
                print("Back");
                // Go to main menu
                mainCamera.transform.DOMove(_menuCamPos, 1.0f);
                mainCamera.transform.DORotate(_menuCamRot.eulerAngles, 1);
                break;
            case "Quit":
                print("Quit");
                // Exit application
                Application.Quit();
                break;
            case "LeftArrow":
                print("Scroll Left");
                RotateWheel(Mathf.Abs(72f) * -1);
                break;
            case "RightArrow":
                print("Scroll Right");
                RotateWheel(Mathf.Abs(72f));
                break;
        }
        yield return null;
    }

    void RotateWheel(float rotation)
    {
        print("Rotating: " + rotation);
        gameModeWheel.transform.DORotate(gameModeWheel.transform.rotation.eulerAngles + new Vector3(0, rotation, 0), 0.15f);

        // If going right
        if (rotation > 0)
        {
            if (curMode == gameModeSelection.Standard)
                curMode = gameModeSelection.Headhunter;
            else if (curMode == gameModeSelection.Headhunter)
                curMode = gameModeSelection.Oddball;
            else if (curMode == gameModeSelection.Oddball)
                curMode = gameModeSelection.Soccer;
            else if (curMode == gameModeSelection.Soccer)
                curMode = gameModeSelection.King;
            else if (curMode == gameModeSelection.King)
                curMode = gameModeSelection.Standard;
        }
        // If going left
        else if (rotation < 0)
        {
            if (curMode == gameModeSelection.Standard)
                curMode = gameModeSelection.King;
            else if (curMode == gameModeSelection.King)
                curMode = gameModeSelection.Soccer;
            else if (curMode == gameModeSelection.Soccer)
                curMode = gameModeSelection.Oddball;
            else if (curMode == gameModeSelection.Oddball)
                curMode = gameModeSelection.Headhunter;
            else if (curMode == gameModeSelection.Headhunter)
                curMode = gameModeSelection.Standard;
        }
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
