using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnterTrigger : Interactions
{
    public UIDocument UIDocument;
    private VisualElement loadUI;
    private VisualElement loadImage;
    private Label loadPercentage;

    public bool RequiresKey = false;
    public string KeyName = "";
    public string CantTriggerText = "";
    public string TriggerText = "";

    public string NextLevel;

    public bool usesLoadUI = false;
    public Sprite image;

    private AsyncOperation loadingOperation;

    public override void Start()
    {
        base.Start();

        loadUI = UIDocument.rootVisualElement.Q<VisualElement>("LoadUI");
        loadImage = UIDocument.rootVisualElement.Q<VisualElement>("LoadImage");
        loadPercentage = UIDocument.rootVisualElement.Q<Label>("LoadPercentage");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActiveState()
    {
        if (GameData.Instance.Objects.Contains(KeyName) || GameData.Instance.alldoorsopenCheat || RequiresKey == false)
        {
            text.text = TriggerText;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (usesLoadUI)
                {
                    loadUI.style.display = DisplayStyle.Flex;
                    loadImage.style.backgroundImage = new StyleBackground(image);
                    StartCoroutine(LoadScene(NextLevel));
                }

                else
                {
                    SceneManager.LoadScene(NextLevel);
                }
            }
        }
        else
        {
            text.text = CantTriggerText;
        }
    }

    IEnumerator LoadScene(string sceneName)
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

}
