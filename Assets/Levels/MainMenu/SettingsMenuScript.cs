using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SettingsMenuScript : MonoBehaviour
{

    public UIDocument uidocument;
    private VisualElement menuUiRoot;
    private VisualElement menuUi;
    private VisualElement settingsUi;

    public Animator menuAnimator;

    private Button resolutionLeft;
    private Label resolutionText;
    private Button resolutionRight;
    private int[] resolutionY = { 720, 1080, 1440, 2160 };
    private int[] resolutionX = { 1280, 1920, 2560, 3840 };
    private int resolutionIndex = 1;

    private Button VsyncLeft;
    private Label VsyncText;
    private Button VsyncRight;
    private int[] Vsync = { 0, 1, 2 };
    private int vsyncIndex = 1;

    private Button qualityLeft;
    private Label qualityText;
    private Button qualityRight;
    private int[] quality = { 0, 1, 2, 3};
    private int qualityIndex = 3;

    private Button fullscreenLeft;
    private Label fullscreenText;
    private Button fullscreenRight;
    private int[] fullscreen = { 0, 1 };
    private int fullscreenIndex = 1;

    private Button targetFramerateLeft;
    private Label targetFramerateText;
    private Button targetFramerateRight;
    private int[] targetFramerate = { 30, 60, 120, 300};
    private int targetFramerateIndex = 1;

    private Button musicLeft;
    private Label musicText;
    private Button musicRight;
    private float musicIndex = 1;
    public AudioMixer musicMixer;

    private Button effectsLeft;
    private Label effectsText;
    private Button effectsRight;
    private float effectsIndex = 1;
    public AudioMixer effectsMixer;

    private Button back; 
    
    private GameSettings settings;

    private string settingsFilePath;

    private void Awake()
    {
        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Peggy TG");
        Directory.CreateDirectory(folderPath); // Create the folder if it doesn't exist

        settingsFilePath = Path.Combine(folderPath, "GameSettings.json");
        LoadSettings();
    }

    private void Start()
    {
        menuUiRoot = uidocument.rootVisualElement;

        menuUi = menuUiRoot.Q<VisualElement>("MenuUI");
        settingsUi = menuUiRoot.Q<VisualElement>("SettingsUI");

        resolutionLeft = menuUiRoot.Q<Button>("ResolutionLeft");
        resolutionText = menuUiRoot.Q<Label>("ResolutionText");
        resolutionRight = menuUiRoot.Q<Button>("ResolutionRight");

        VsyncLeft = menuUiRoot.Q<Button>("VsyncLeft");
        VsyncText = menuUiRoot.Q<Label>("VsyncText");
        VsyncRight = menuUiRoot.Q<Button>("VsyncRight");

        qualityLeft = menuUiRoot.Q<Button>("QualityLeft");
        qualityText = menuUiRoot.Q<Label>("QualityText");
        qualityRight = menuUiRoot.Q<Button>("QualityRight");

        fullscreenLeft = menuUiRoot.Q<Button>("FullscreenModeLeft");
        fullscreenText = menuUiRoot.Q<Label>("FullscreenText");
        fullscreenRight = menuUiRoot.Q<Button>("FullscreenModeRight");

        targetFramerateLeft = menuUiRoot.Q<Button>("TargetFramerateLeft");
        targetFramerateText = menuUiRoot.Q<Label>("TargetFramerateText");
        targetFramerateRight = menuUiRoot.Q<Button>("TargetFramerateRight");

        musicLeft = menuUiRoot.Q<Button>("MusicLeft");
        musicText = menuUiRoot.Q<Label>("MusicText");
        musicRight = menuUiRoot.Q<Button>("MusicRight");

        effectsLeft = menuUiRoot.Q<Button>("EffectsLeft");
        effectsText = menuUiRoot.Q<Label>("EffectsText");
        effectsRight = menuUiRoot.Q<Button>("EffectsRight");

        back = menuUiRoot.Q<Button>("BackButton");

        LoadSettings();
        ApplyResolution();
        ApplyVsyncChange();
        ApplyQualityChange();
        ApplyFullscreenChange();
        ApplyTargetFramerateChange();
        ApplyMusicChange();
        ApplyEffectsChange();

        resolutionLeft.clicked += () => Resolution("left");
        resolutionRight.clicked += () => Resolution("right");

        VsyncLeft.clicked += () => VsyncChange("left");
        VsyncRight.clicked += () => VsyncChange("right");

        qualityLeft.clicked += () => QualityChange("left");
        qualityRight.clicked += () => QualityChange("right");

        fullscreenLeft.clicked += () => FullscreenChange("left");
        fullscreenRight.clicked += () => FullscreenChange("right");

        targetFramerateLeft.clicked += () => TargetFramerateChange("left");
        targetFramerateRight.clicked += () => TargetFramerateChange("right");

        musicLeft.clicked += () => MusicChange("left");
        musicRight.clicked += () => MusicChange("right");

        effectsLeft.clicked += () => EffectsChange("left");
        effectsRight.clicked += () => EffectsChange("right");

        back.clicked += Back;
    }
    
    void Resolution(string direction)
    {
        if(direction == "left")
        {
            resolutionIndex--;
            if(resolutionIndex < 0)
            {
                resolutionIndex = 0;
            }
        }
        else if(direction == "right")
        {
            resolutionIndex++;
            if(resolutionIndex > 3)
            {
                resolutionIndex = 3;
            }
        }   
        UpdateSettings();
        ApplyResolution();
    }

    void ApplyResolution()
    {
        resolutionText.text = resolutionX[resolutionIndex] + "x" + resolutionY[resolutionIndex];
        Screen.SetResolution(resolutionX[resolutionIndex], resolutionY[resolutionIndex], Screen.fullScreen);
    }

    void VsyncChange(string direction)
    {
        if (direction == "left")
        {
            vsyncIndex--;
            if (vsyncIndex < 0)
            {
                vsyncIndex = 0;
            }
        }
        else if (direction == "right")
        {
            vsyncIndex++;
            if (vsyncIndex > 2)
            {
                vsyncIndex = 2;
            }
        }
        UpdateSettings();
        ApplyVsyncChange();
    }

    void ApplyVsyncChange()
    {
        switch (vsyncIndex)
        {
            case 0:
                VsyncText.text = "Off";
                break;
            case 1:
                VsyncText.text = "On";
                break;
            case 2:
                VsyncText.text = "Half";
                break;
        }
        QualitySettings.vSyncCount = Vsync[vsyncIndex];
    }

    void QualityChange(string direction)
    {
        if (direction == "left")
        {
            qualityIndex--;
            if (qualityIndex < 0)
            {
                qualityIndex = 0;
            }
        }
        else if (direction == "right")
        {
            qualityIndex++;
            if (qualityIndex > 3)
            {
                qualityIndex = 3;
            }
        }
        UpdateSettings();
        ApplyQualityChange();
    }

    void ApplyQualityChange()
    {
        switch (qualityIndex)
        {
            case 0:
                qualityText.text = "Low";
                break;
            case 1:
                qualityText.text = "Medium";
                break;
            case 2:
                qualityText.text = "High";
                break;
            case 3:
                qualityText.text = "Ultra";
                break;
        }
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    void FullscreenChange(string direction)
    {
        if (direction == "left")
        {
            fullscreenIndex--;
            if (fullscreenIndex < 0)
            {
                fullscreenIndex = 0;
            }
        }
        else if (direction == "right")
        {
            fullscreenIndex++;
            if (fullscreenIndex > 1)
            {
                fullscreenIndex = 1;
            }
        }
        UpdateSettings();
        ApplyFullscreenChange();
    }

    void ApplyFullscreenChange()
    {
        switch (fullscreenIndex)
        {
            case 0:
                fullscreenText.text = "Off";
                break;
            case 1:
                fullscreenText.text = "On";
                break;
        }
        Screen.fullScreen = fullscreen[fullscreenIndex] == 1 ? true : false;
    }

    void TargetFramerateChange(string direction)
    {
        if (direction == "left")
        {
            targetFramerateIndex--;
            if (targetFramerateIndex < 0)
            {
                targetFramerateIndex = 0;
            }
        }
        else if (direction == "right")
        {
            targetFramerateIndex++;
            if (targetFramerateIndex > 3)
            {
                targetFramerateIndex = 3;
            }
        }
        UpdateSettings();
        ApplyTargetFramerateChange();
    }

    void ApplyTargetFramerateChange()
    {
        switch (targetFramerateIndex)
        {
            case 0:
                targetFramerateText.text = "30";
                break;
            case 1:
                targetFramerateText.text = "60";
                break;
            case 2:
                targetFramerateText.text = "120";
                break;
            case 3:
                targetFramerateText.text = "Unlimited";
                break;
        }
        Application.targetFrameRate = targetFramerate[targetFramerateIndex];
    }

    void MusicChange(string direction)
    {
        if (direction == "left")
        {
            musicIndex -= 0.1f;
            if (musicIndex < 0)
            {
                musicIndex = 0;
            }
        }
        else if (direction == "right")
        {
            musicIndex += 0.1f;
            if (musicIndex > 1)
            {
                musicIndex = 1;
            }
        }
        UpdateSettings();
        ApplyMusicChange();
    }

    void ApplyMusicChange()
    {
        musicText.text = Mathf.RoundToInt(musicIndex * 100) + "%";
        if (musicIndex != 0)
        {
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(musicIndex) * 20);
        }
        else
        {
            musicMixer.SetFloat("MusicVolume", -80);
        }
    }

    void EffectsChange(string direction)
    {
        if (direction == "left")
        {
            effectsIndex -= 0.1f;
            if (effectsIndex < 0)
            {
                effectsIndex = 0;
            }
        }
        else if (direction == "right")
        {
            effectsIndex += 0.1f;
            if (effectsIndex > 1)
            {
                effectsIndex = 1;
            }
        }
        UpdateSettings();
        ApplyEffectsChange();
    }

    void ApplyEffectsChange()
    {
        effectsText.text = Mathf.RoundToInt(effectsIndex * 100) + "%";
        if (effectsIndex != 0)
        {
            effectsMixer.SetFloat("EffectsVolume", Mathf.Log10(effectsIndex) * 20);
        }
        else
        {
            effectsMixer.SetFloat("EffectsVolume", -80);
        }
    }

    void Back()
    {
        SaveSettings();
        StartCoroutine(BackEI());
    }

    IEnumerator BackEI()
    {
        yield return FadeOutSettings(1.1f);
        menuAnimator.SetBool("Settings", false);
        yield return new WaitForSeconds(1f);
        yield return FadeInMenu(1.1f);
    }

    IEnumerator FadeOutSettings(float duration)
    {
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            settingsUi.style.opacity = 1 - (i / duration);
            yield return null;
        }

        settingsUi.style.display = DisplayStyle.None;
    }

    IEnumerator FadeInMenu(float duration)
    {
        menuUi.style.display = DisplayStyle.Flex;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            menuUi.style.opacity = (i / duration);
            yield return null;
        }
    }

    private void LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            settings = JsonUtility.FromJson<GameSettings>(json);
            ApplySettings();
        }
        else
        {
            settings = new GameSettings();
            settings.resolutionIndex = 1;
            settings.vsyncIndex = 1;
            settings.qualityIndex = 3;
            settings.fullscreenIndex = 1;
            settings.targetFramerateIndex = 1;
            settings.musicIndex = 1;
            settings.effectsIndex = 1;
            ApplySettings();
            SaveSettings();
        }
    }

    private void ApplySettings()
    {
        resolutionIndex = settings.resolutionIndex;
        vsyncIndex = settings.vsyncIndex;
        qualityIndex = settings.qualityIndex;
        fullscreenIndex = settings.fullscreenIndex;
        targetFramerateIndex = settings.targetFramerateIndex;
        musicIndex = settings.musicIndex;
        effectsIndex = settings.effectsIndex;
    }

    private void UpdateSettings()
    {
        settings.resolutionIndex = resolutionIndex;
        settings.vsyncIndex = vsyncIndex;
        settings.qualityIndex = qualityIndex;
        settings.fullscreenIndex = fullscreenIndex;
        settings.targetFramerateIndex = targetFramerateIndex;
        settings.musicIndex = musicIndex;
        settings.effectsIndex = effectsIndex;

        SaveSettings();
    }

    private void SaveSettings()
    {
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(settingsFilePath, json);
    }
}

[System.Serializable]
public class GameSettings
{
    public int resolutionIndex;
    public int vsyncIndex;
    public int qualityIndex;
    public int fullscreenIndex;
    public int targetFramerateIndex;
    public float musicIndex;
    public float effectsIndex;
}