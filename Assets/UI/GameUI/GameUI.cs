using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    public UIDocument UIDocument;
    private VisualElement pausePannel;
    private VisualElement quitPannel;

    private Button resumeButton;
    private Button quitButton;

    private Button toMenu;
    private Button toDesktop;
    private Button toGame;

    public PlayerLook playercontrols;

    private Label subs;

    public bool isPaused;

    private void Start()
    {
        pausePannel = UIDocument.rootVisualElement.Q<VisualElement>("PausePannel");
        quitPannel = UIDocument.rootVisualElement.Q<VisualElement>("QuitPannel");


        resumeButton = pausePannel.Q<Button>("ResumeButton");
        quitButton = pausePannel.Q<Button>("QuitButton");

        toMenu = quitPannel.Q<Button>("QuitToMenu");
        toDesktop = quitPannel.Q<Button>("QuitToDesktop");
        toGame = quitPannel.Q<Button>("Cancel");

        subs = UIDocument.rootVisualElement.Q<Label>("Subs");

        resumeButton.clicked += ResumeButton;
        quitButton.clicked += QuitButton;

        toMenu.clicked += ToMenu;
        toDesktop.clicked += ToDesktop;
        toGame.clicked += ToGame;
    }

    private void Update()
    {
        subs.text = GameData.Instance.Subs;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }
    }

    private void PauseButton()
    {
        playercontrols.enabled = false;
        Time.timeScale = 0f; // Pause the game time
        isPaused = true;
        pausePannel.style.display = DisplayStyle.Flex;
        GameData.Instance.Pause(true);
    }

    private void ResumeButton()
    {
        playercontrols.enabled = true;
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;
        pausePannel.style.display = DisplayStyle.None;
        GameData.Instance.Pause(false);
    }

    private void QuitButton()
    {
        pausePannel.style.display = DisplayStyle.None;
        quitPannel.style.display = DisplayStyle.Flex;
    }

    private void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void ToDesktop()
    {
        Application.Quit();
    }

    private void ToGame()
    {
        quitPannel.style.display = DisplayStyle.None;
        playercontrols.enabled = true;
        Time.timeScale = 1f; // Resume the game time
        isPaused = false;
        GameData.Instance.Pause(false);
    }
}
