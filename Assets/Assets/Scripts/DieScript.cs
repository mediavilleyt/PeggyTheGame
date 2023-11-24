using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DieScript : MonoBehaviour
{
    public Animator Playeranimation;
    public PlayerMovement Pl1;
    public PlayerLook Pl2;
    public DBLHolder Pl3;

    public UnityEngine.UI.Slider HealthPeg;
    public UnityEngine.UI.Slider HealthBoss;

    public AudioSource audio;

    public UIDocument UIDocuments;

    private VisualElement deathScreen;

    public GameObject bossCanvas1;
    public GameObject bossCanvas2;

    private void Start()
    {
        deathScreen = UIDocuments.rootVisualElement.Q<VisualElement>("DeathPannel");
        deathScreen.style.display = DisplayStyle.None;

        GameData.Instance.Ded = false;
        audio.volume = 1;
        Pl1.enabled = true;
        Pl2.enabled = true;
        Pl3.enabled = true;
        Playeranimation.SetBool("Died", false);
        GameData.Instance.Pause(false);
    }

    void Update()
    {
        HealthPeg.value = GameData.Instance.Helth;
        HealthBoss.value = GameData.Instance.BossHealth;

        if (GameData.Instance.Helth <= 0)
        {
            GameData.Instance.Ded = true;
        }

        if (GameData.Instance.Ded)
        {
            audio.volume = 0;
            Pl1.enabled = false;
            Pl2.enabled = false;
            Pl3.enabled = false;
            Playeranimation.SetBool("Died", true);
            audio.Stop();
            Invoke("FadeOutToDeathScreen", 2);
        }
    }

    public void FadeOutToDeathScreen()
    {
        bossCanvas1.SetActive(false);
        bossCanvas2.SetActive(false);
        deathScreen.style.display = DisplayStyle.Flex;
        GameData.Instance.Pause(true);
        Time.timeScale = 0f;
    }
}
