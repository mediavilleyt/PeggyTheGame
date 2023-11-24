using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CheatMenu : MonoBehaviour
{
    public TMP_InputField inputField;

    public UIDocument UIDocument;

    private UnityEngine.UIElements.Button levelSelect;

    public AudioSource normalMusic;
    public AudioSource cheatMusic;

    public MainMenuUI mainMenuUI;

    bool cheat1disable = false;
    bool cheat2disable = false;
    bool cheat3disable = false;

    private void Start()
    {
        levelSelect = UIDocument.rootVisualElement.Q<UnityEngine.UIElements.Button>("SelectButton");
        levelSelect.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputField.gameObject.activeSelf)
            {
                inputField.gameObject.SetActive(false);
            }
            else
            {
                inputField.gameObject.SetActive(true);
            }
        }

        if(inputField.text == "pookie" && cheat1disable == false)
        {
            levelSelect.style.display = DisplayStyle.Flex;
            cheat1disable = true;
            inputField.gameObject.SetActive(false);
        }

        if (inputField.text == "rockstart" && cheat2disable == false)
        {
            normalMusic.Stop();
            cheatMusic.Play();
            cheat2disable = true;
            inputField.gameObject.SetActive(false);
        }

        if(inputField.text == "lockpick" && cheat3disable == false)
        {
            GameData.Instance.alldoorsopenCheat = true;
            cheat3disable = true;
            inputField.gameObject.SetActive(false);
        }

        if (inputField.text.EndsWith("lol"))
        {
            GameData.Instance.Objects = new List<string>();
            string[] strings = inputField.text.Split(' ');

            GameData.Instance.Objects = new List<string>();

            if (strings.Length == 3)
            {
                SceneManager.LoadScene(strings[0] + " " + strings[1]);//TOWN1 lol
            }

            else
            {
                SceneManager.LoadScene(strings[0]);//TOWN1
            }
        }
    }
}
