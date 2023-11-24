using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneBoss1 : MonoBehaviour
{
    public GameObject Player;

    public GameObject Boss;
    public AImovement BossMovement;
    public Rigidbody Rigidbody;

    public BossuI bossUI;

    public bool StartCutScene;

    float timer;

    public GameObject blackUI;
    public Image blackScreen;
    public Image blackBars;

    public DBLHolder dblHolder;

    public GameObject HealthUI;


    void Start()
    {
        bossUI.enabled = false;
        HealthUI.SetActive(false);
        GameData.Instance.Objects.RemoveAll(item => item == "Dumbell");     
        GameData.Instance.Objects.Add("Military Grade Dumbell Launcher");
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCutScene && timer < 6)
        {
            blackScreen.CrossFadeAlpha(0, 1, false);
            timer += Time.deltaTime;
            GameData.Instance.CanMove = false;
            BossMovement.CanMove = false;
            Rigidbody.useGravity = false;
            dblHolder.enabled = false;
            Player.transform.LookAt(Boss.transform.position);
            Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, new Vector3(0, 1, 0), 3f * Time.deltaTime);
            Quaternion Lookat = Quaternion.LookRotation(new Vector3(Player.transform.position.x, 1, Player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Lookat, 0.3f * Time.deltaTime);
        }
        else
        {
            StartCoroutine(EndCutscene());
        }
    }

    IEnumerator EndCutscene()
    {
        StartCutScene = false;
        blackBars.CrossFadeAlpha(0, 1, false);
        blackUI.SetActive(false);
        HealthUI.SetActive(true);
        bossUI.enabled = true;
        GameData.Instance.CanMove = true;
        BossMovement.CanMove = true;
        Rigidbody.useGravity = true;
        dblHolder.enabled = true;
        yield return null;
    }
}
