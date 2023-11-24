using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public UIDocument UIDocument;

    private VisualElement root;

    private VisualElement menuUI;
    private VisualElement selectUI;

    private VisualElement loadUi;
    private VisualElement loadImage;
    private Label loadPercentage;

    private Button backButton;
    private Button leftButton;
    private Button rightButton;
    private Button playButton;

    private Label levelNameLabel;
    private VisualElement levelImage;

    public string[] levelNames;
    public Sprite[] levelImages;
    private int levelIndex;
    private AsyncOperation loadingOperation;

    private void Start()
    {
        root = UIDocument.rootVisualElement;

        menuUI = root.Q<VisualElement>("MenuUI");
        selectUI = root.Q<VisualElement>("SelectUI");

        loadUi = root.Q<VisualElement>("LoadUI");
        loadImage = root.Q<VisualElement>("LoadImage");
        loadPercentage = root.Q<Label>("LoadPercentage");

        backButton = root.Q<Button>("SelectBackButton");
        leftButton = root.Q<Button>("SelectLeftButton");
        rightButton = root.Q<Button>("SelectRightButton");
        playButton = root.Q<Button>("SelectPlayButton");

        levelNameLabel = root.Q<Label>("LevelName");
        levelImage = root.Q<VisualElement>("IMG");

        backButton.clicked += BackButtonClicked;
        leftButton.clicked += LeftButtonClicked;
        rightButton.clicked += RightButtonClicked;
        playButton.clicked += PlayButtonClicked;

        SetDisplay();

        Debug.Log(levelNameLabel.text);
    }

    private void BackButtonClicked()
    {
        menuUI.style.display = DisplayStyle.Flex;
        selectUI.style.display = DisplayStyle.None;
    }

    private void LeftButtonClicked()
    {
        if(levelIndex > 0)
        {
            levelIndex--;
            SetDisplay();
        }
    }

    private void RightButtonClicked()
    {
        if (levelIndex < levelNames.Length - 1)
        {
            levelIndex++;
            SetDisplay();
        }
    }

    void SetDisplay()
    {
        levelNameLabel.text = levelNames[levelIndex];
        levelImage.style.backgroundImage = new StyleBackground(levelImages[levelIndex]);
    }

    private void PlayButtonClicked()
    {
        switch (levelNames[levelIndex])
        {
            case "Level 1":
                StartCoroutine(LoadSceneWithProgress("Level 1 Intro"));
                SetLoadImage("Level 1 Intro");
                break;
            case "Level 2":
                StartCoroutine(LoadSceneWithProgress("Level 2-1"));
                SetLoadImage("Level 2-1");
                break;
            case "Boss 1":
                StartCoroutine(LoadSceneWithProgress("BOSS1"));
                SetLoadImage("BOSS1");
                break;
        }
    }

    private IEnumerator LoadSceneWithProgress(string sceneName)
    {
        menuUI.style.display = DisplayStyle.None;
        selectUI.style.display = DisplayStyle.None;
        loadUi.style.display = DisplayStyle.Flex;
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

    //retrieve load image
    void SetLoadImage(string sceneName)
    {
        switch (sceneName)
        {
            case "Level 1 Intro":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[0]);
                break;
            case "Level 2-1":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[1]);
                break;
            case "BOSS1":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[2]);
                break;
            case "Level 4-1":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[3]);
                break;
            case "BOSS2":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[4]);
                break;
            case "TOWN1":
                loadImage.style.backgroundImage = new StyleBackground(levelImages[5]);
                break;
        }
    }
}
