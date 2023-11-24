using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starts : MonoBehaviour
{
    public string LevelName;
    Scene Currentscene;

    public string[] objectsToBeAdded;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        // Create the full path to the save file in Documents/Peggy TG
        string saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Peggy TG");
        Directory.CreateDirectory(saveFolderPath); // Create the directory if it doesn't exist

        string saveFilePath = Path.Combine(saveFolderPath, "savedLevel.txt");

        // Write the current level name to the file
        File.WriteAllText(saveFilePath, LevelName);

        GameData.Instance.Pause(false);

        GameData.Instance.Helth = 100;

        Currentscene = SceneManager.GetActiveScene();

        if (GameData.Instance.Objects == null) GameData.Instance.Objects = new List<string> { };
        GameData.Instance.Objects.AddRange(objectsToBeAdded);
        GameData.Instance.CurrentScene = LevelName;
    }
}

[System.Serializable]
public class LevelSetting
{
    public string LastLevel;
}
