using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class BossUIScript : MonoBehaviour
{
    float Helt = 100;
    public float BossTotalHp;
    public Slider PegHPMeter;
    public Slider BossHPMeter;
    public GameObject Crosshair;
    public DBLHolder LookForDBL;

    void Update()
    {
        if (Helt != GameData.Instance.Helth || BossTotalHp != GameData.Instance.BossHealth)
        {
            UpdateUI(GameData.Instance.Helth, GameData.Instance.BossHealth);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && LookForDBL.ThingType != Things.Nothing && LookForDBL.ThingType != Things.Flash)
        {
            AimDown();
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            AimUp();
        }
    }

    public void UpdateUI(float hp, float Bhp)
    {
        PegHPMeter.value = 100 - hp;
        Helt = hp;

        float CurrentBossHealth = (Bhp / BossTotalHp) * 100;

        BossHPMeter.value = 100 - CurrentBossHealth;
    }

    public void AimDown()
    {
        Camera.main.fieldOfView = Mathf.Lerp(50,60,5*Time.deltaTime);
        Crosshair.SetActive(true);
        GameData.Instance.IsAiming = true;
    }

    public void AimUp()
    {
        Camera.main.fieldOfView = 60;
        Crosshair.SetActive(false);
        GameData.Instance.IsAiming = false;
    }
}
