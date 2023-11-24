using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    public UIDocument uidocument;
    private VisualElement menuUiRoot;
    private VisualElement menuUi;
    private VisualElement selectUi;
    private VisualElement settingsUi;
    private VisualElement loadUi;
    private VisualElement loadImage;
    private Label loadPercentage;

    public Sprite[] loadImages;

    private Button newGame;
    private Button Continue;
    private Button select;
    private Button settings;
    private Button quit;

    public Animator menuAnimator;

    private AsyncOperation loadingOperation;

    private void Start()
    {
        Time.timeScale = 1f;
        GameData.Instance.Pause(true);

        menuUiRoot = uidocument.rootVisualElement;

        menuUi = menuUiRoot.Q<VisualElement>("MenuUI");
        settingsUi = menuUiRoot.Q<VisualElement>("SettingsUI");
        selectUi = menuUiRoot.Q<VisualElement>("SelectUI");
        loadUi = menuUiRoot.Q<VisualElement>("LoadUI");
        loadImage = menuUiRoot.Q<VisualElement>("LoadImage");
        loadPercentage = menuUiRoot.Q<Label>("LoadPercentage");

        newGame = menuUiRoot.Q<Button>("NewGameButton");
        Continue = menuUiRoot.Q<Button>("ContinueButton");
        select = menuUiRoot.Q<Button>("SelectButton");
        settings = menuUiRoot.Q<Button>("SettingsButton");
        quit = menuUiRoot.Q<Button>("QuitButton");

        // Try to load the saved level name from a file
        string saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Peggy TG");
        Directory.CreateDirectory(saveFolderPath);

        string saveFilePath = Path.Combine(saveFolderPath, "savedLevel.txt");

        if (File.Exists(saveFilePath))
        {
            GameData.Instance.CurrentScene = File.ReadAllText(saveFilePath);
        }
        else
        {
            GameData.Instance.CurrentScene = "Level 1 Intro"; // Default level name if the file doesn't exist
        }

        if (GameData.Instance.CurrentScene == "Level 1 Intro" || GameData.Instance.CurrentScene == null)
        {
            Continue.style.display = DisplayStyle.None;
        }
        else
        {
            Continue.style.display = DisplayStyle.Flex;
        }

        newGame.clicked += NewGame;
        Continue.clicked += ContinueGame;
        select.clicked += Select;
        settings.clicked += Settings;
        quit.clicked += Quit;

        SetLoadImage(GameData.Instance.CurrentScene);
    }
    
    void NewGame()
    {
        GameData.Instance.CurrentScene = "Level 1 Intro";
        StartCoroutine(LoadNewGameEI());
    }

    void ContinueGame()
    {
        StartCoroutine(LoadGameEI());
    }

    void Select()
    {
        menuUi.style.display = DisplayStyle.None;
        selectUi.style.display = DisplayStyle.Flex;
    }

    void Settings()
    {
        StartCoroutine(SettingsEI());
    }

    void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadNewGameEI()
    {
        yield return FadeOutMenu(1);
        menuAnimator.SetBool("Start", true);
        yield return new WaitForSeconds(3);
        //Load new scene
        SceneManager.UnloadSceneAsync("Main Menu");
        SceneManager.LoadScene(GameData.Instance.CurrentScene);
    }

    public IEnumerator LoadGameEI()
    {
        yield return FadeOutMenu(1);
        menuAnimator.SetBool("Start", true);
        yield return new WaitForSeconds(3);
        //Load new scene
        SceneManager.UnloadSceneAsync("Main Menu");
        SetLoadImage(GameData.Instance.CurrentScene);
        StartCoroutine(LoadSceneWithProgress(GameData.Instance.CurrentScene));
        loadUi.style.display = DisplayStyle.Flex;
    }

    private IEnumerator LoadSceneWithProgress(string sceneName)
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f); // Normalize the progress

            loadPercentage.text = $"Loading: {Mathf.Round(progress) * 100}%";

            // Simulate an artificial delay to slow down the loading process
            float artificialDelayTime = 0.5f; // Adjust this value to control the delay duration
            float elapsedTime = 0f;

            while (elapsedTime < artificialDelayTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (progress >= 1f)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator SettingsEI()
    {
        yield return FadeOutMenu(1.1f);
        menuAnimator.SetBool("Settings", true);
        yield return new WaitForSeconds(1);
        yield return FadeInSettings(1.1f);
    }

    IEnumerator FadeOutMenu(float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            menuUi.style.opacity = 1 - (i / duration);
            yield return null;
        }

        menuUi.style.display = DisplayStyle.None;
    }

    IEnumerator FadeInSettings(float duration)
    {
        settingsUi.style.display = DisplayStyle.Flex;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            settingsUi.style.opacity = (i / duration);
            yield return null;
        }
    }

    //retrieve load image
    void SetLoadImage(string sceneName)
    {
        switch (sceneName)
        {
            case "Level 1 Intro":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[0]);
                break;
            case "Level 2-1":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[1]);
                break;
            case "BOSS1":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[2]);
                break;
            case "Level 4-1":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[3]);
                break;
            case "BOSS2":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[4]);
                break;
            case "TOWN1":
                loadImage.style.backgroundImage = new StyleBackground(loadImages[5]);
                break;
        }
    }
}
