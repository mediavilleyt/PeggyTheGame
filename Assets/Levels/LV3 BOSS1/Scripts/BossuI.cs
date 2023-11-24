using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BossuI : MonoBehaviour
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

    public GameObject bossCanvas1;

    private VisualElement deathPannel;
    private VisualElement deathQuitPannel;

    private Button restartButton;
    private Button deathQuitButton;

    private Button deathToMenu;
    private Button deathToDesktop;
    private Button deathToGame;

    public bool killPeggy;

    public string restartScene;

    private void Start()
    {
        pausePannel = UIDocument.rootVisualElement.Q<VisualElement>("PausePannel");
        quitPannel = UIDocument.rootVisualElement.Q<VisualElement>("QuitPannel");


        resumeButton = pausePannel.Q<Button>("ResumeButton");
        quitButton = pausePannel.Q<Button>("QuitButton");

        toMenu = quitPannel.Q<Button>("QuitToMenu");
        toDesktop = quitPannel.Q<Button>("QuitToDesktop");
        toGame = quitPannel.Q<Button>("Cancel");

        deathPannel = UIDocument.rootVisualElement.Q<VisualElement>("DeathPannel");
        deathQuitPannel = UIDocument.rootVisualElement.Q<VisualElement>("DeathQuitPannel");

        restartButton = deathPannel.Q<Button>("RestartButton");
        deathQuitButton = deathPannel.Q<Button>("DeathQuitButton");

        deathToMenu = deathQuitPannel.Q<Button>("DeathQuitToMenu");
        deathToDesktop = deathQuitPannel.Q<Button>("DeathQuitToDesktop");
        deathToGame = deathQuitPannel.Q<Button>("DeathCancel");

        subs = UIDocument.rootVisualElement.Q<Label>("Subs");

        resumeButton.clicked += ResumeButton;
        quitButton.clicked += QuitButton;

        toMenu.clicked += ToMenu;
        toDesktop.clicked += ToDesktop;
        toGame.clicked += ToGame;

        restartButton.clicked += RestartButton;
        deathQuitButton.clicked += DeathQuitButton;

        deathToMenu.clicked += DeathToMenu;
        deathToDesktop.clicked += DeathToDesktop;
        deathToGame.clicked += DeathToGame;
    }

    private void Update()
    {
        subs.text = GameData.Instance.Subs;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameData.Instance.Helth == 0)
            {
                return;
            }

            if (isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }

        if (killPeggy)
        {
            GameData.Instance.Helth = 0;
        }
    }

    private void PauseButton()
    {
        isPaused = true;
        bossCanvas1.SetActive(false);
        playercontrols.enabled = false;
        Time.timeScale = 0f; // Pause the game time
        pausePannel.style.display = DisplayStyle.Flex;
        GameData.Instance.Pause(true);
    }

    private void ResumeButton()
    {
        isPaused = false;
        bossCanvas1.SetActive(true);
        playercontrols.enabled = true;
        Time.timeScale = 1f; // Resume the game time
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

    private void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(restartScene);
    }

    private void DeathQuitButton()
    {
        deathPannel.style.display = DisplayStyle.None;
        deathQuitPannel.style.display = DisplayStyle.Flex;
    }

    private void DeathToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void DeathToDesktop()
    {
        Application.Quit();
    }

    private void DeathToGame()
    {
        deathQuitPannel.style.display = DisplayStyle.None;
        deathPannel.style.display = DisplayStyle.Flex;
    }
}
