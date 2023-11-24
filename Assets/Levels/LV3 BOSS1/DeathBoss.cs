using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class DeathBoss : MonoBehaviour
{
    int HeadCount;
    public GameObject Exit;
    public AImovement movement;
    public Rigidbody body;
    public float Bhealth;
    public AudioSource sound;

    private void Start()
    {
        GameData.Instance.BossHealth = Bhealth;
    }

    // Update is called once per frame
    void Update()
    {
        HeadCount = GameObject.FindGameObjectsWithTag("Head").Length;
        if(HeadCount == 0)
        {
            transform.tag = "Untagged";
            Destroy(movement);
            body.mass = 0;
            body.constraints = RigidbodyConstraints.None;
            Ded1();
        }
    }

    public void Ded1()
    {
        if(sound != null) sound.volume = 0;
        Exit.SetActive(true);
    }
}
