using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RestartScript : MonoBehaviour
{
    public UIDocument UIDocument;

    private VisualElement loadUI;
    private VisualElement loadImage;
    private Label loadPercentage;

    public Sprite image;

    private AsyncOperation loadingOperation;

    public string sceneName;

    private void Start()
    {
        loadUI = UIDocument.rootVisualElement.Q<VisualElement>("LoadUI");
        loadImage = loadUI.Q<VisualElement>("LoadImage");
        loadPercentage = loadUI.Q<Label>("LoadPercentage");

        loadImage.style.backgroundImage = new StyleBackground(image);

        loadImage.style.backgroundImage = image.texture;

        StartCoroutine(LoadSceneWithProgress(sceneName));
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
}
