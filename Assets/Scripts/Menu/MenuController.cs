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
    public GameObject teamSelectBreadcrumbs;
    public GameObject stageSelectBreadcrumbs;

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

    public enum stageSelection { Orig };
    public stageSelection curStage = stageSelection.Orig;

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
//		SceneManager.LoadScene(Resources.Load("Scenes/Game2D") as Scene);
    }

    /*
    public void DoMoveToScreen (Camera targetScreen)
    {
        StartCoroutine(ShiftCameraScene(targetScreen));
    }

    public IEnumerator ShiftCameraScene(Camera targetScreen)
    {
        mainCamera.transform.DOMove(targetScreen.transform.position, screenTransitionTime * 0.8f);
        mainCamera.transform.DORotate(targetScreen.transform.rotation.eulerAngles, screenTransitionTime * 0.8f);
    }
    */

    // Translates menu camera target menu screen; called from Button.cs
    public IEnumerator MoveTo (string input)
    {
        mainCamera.transform.DOLocalMove(mainCamera.transform.position + new Vector3(0, 0, -10), screenTransitionTime * 0.2f);
        yield return new WaitForSeconds(0.2f);
        switch (input)
        {
		case "Start":
			print ("Start");
                // Start match
				MatchInit mi = (Instantiate (Resources.Load ("Prefabs/MatchInit") as GameObject) as GameObject).GetComponent<MatchInit> ();
				mi.Initialize (curMode);
                SceneManager.LoadScene("Game2D");
                curScreen = screens.InGame;
                break;
            case "Menu":
                print("Menu");
                // Go to game select
                curScreen = screens.Menu;
                mainCamera.transform.DOMove(_menuCamPos, 0.8f);
                mainCamera.transform.DORotate(_menuCamRot.eulerAngles, 0.8f);
                break;
            case "GameSelect":
                print("Game Select");
                // Go to game select
                curScreen = screens.GameSelect;
                mainCamera.transform.DOMove(_gameSelectCamPos, 0.8f);
                mainCamera.transform.DORotate(_gameSelectCamRot.eulerAngles, 0.8f);
                break;
            case "TeamSelect":
                print("Team Select");
                // Go to team select
                curScreen = screens.TeamSelect;
                UpdateBreadcrumbs("Team Select");
                mainCamera.transform.DOMove(_teamSelectCamPos, 0.8f);
                mainCamera.transform.DORotate(_teamSelectCamRot.eulerAngles, 0.8f);
                break;
            case "StageSelect":
                print("Stage Select");
                // Go to stage select
                curScreen = screens.StageSelect;
                mainCamera.transform.DOMove(_stageSelectCamPos, 0.8f);
                mainCamera.transform.DORotate(_stageSelectCamRot.eulerAngles, 0.8f);
                break;
            case "Trials":
                print("Trials");
                // Go to trials
                curScreen = screens.Trials;
                mainCamera.transform.DOMove(_trialsCamPos, 0.8f);
                mainCamera.transform.DORotate(_trialsCamRot.eulerAngles, 0.8f);
                break;
            case "Settings":
                print("Settings");
                // Go to game settings
                curScreen = screens.Settings;
                mainCamera.transform.DOMove(_settingsCamPos, 0.8f);
                mainCamera.transform.DORotate(_settingsCamRot.eulerAngles, 0.8f);
                break;
            case "Credits":
                print("Credits");
                // Go to credits
                curScreen = screens.Credits;
                mainCamera.transform.DOMove(_settingsCamPos, 0.8f);
                mainCamera.transform.DORotate(_settingsCamRot.eulerAngles, 0.8f);
                break;
            case "Back":
                print("Back");
                // Go to main menu
                curScreen = screens.Menu;
                mainCamera.transform.DOMove(_menuCamPos, 0.8f);
                mainCamera.transform.DORotate(_menuCamRot.eulerAngles, 0.8f);
                break;
            case "Quit":
                print("Quit");
                // Exit application
                Application.Quit();
                break;
        }
        yield return new WaitForSeconds(1f);
        mainCamera.transform.DOLocalMove(mainCamera.transform.position + new Vector3(0, 0, 10), screenTransitionTime * 0.2f);
    }

    public void ChangeMode (string newMode, char direction)
    {
        // If going right
        if (direction == 'r')
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
        // If going left
        else if (direction == 'l')
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
    }

    public void UpdateBreadcrumbs(string scene)
    {
        switch (scene)
        {
            case "Team Select":
                // Updates game mode breadcrumb in team select screen
                teamSelectBreadcrumbs.transform.GetChild(1).GetComponent<TextMesh>().text = GetGameMode();
                break;
        }
    }

    // Called to return a string of the selected game mode's name
    public string GetGameMode()
    {
        string curScene = null;
        switch (curMode)
        {
            case gameModeSelection.Standard:
                curScene = "Standard";
                break;
            case gameModeSelection.King:
                curScene = "King";
                break;
            case gameModeSelection.Soccer:
                curScene = "Soccer";
                break;
            case gameModeSelection.Oddball:
                curScene = "Oddball";
                break;
            case gameModeSelection.Headhunter:
                curScene = "Headhunter";
                break;
        }
        return curScene;
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
