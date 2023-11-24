using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameData
{
    public bool CanMove;

    public List<string> Objects;
    public string Subs;
    public string Info;
    public string Hints;
    public string CurrentScene;
    public float Helth;
    public float BossHealth;
    public bool Ded;
    public bool IsAiming;
    public bool inputWasCorrect = false;
    public bool isCBossInv = false;
    public bool alldoorsopenCheat = false;

    public void Pause(bool isPaused)
    {
        if (isPaused) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
        else { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
    }

    private static GameData instance;

    public static GameData Instance
    {
        get
        {
            if (instance == null) instance = new GameData();
            return instance;
        }
    }

    private GameData() { }
}
