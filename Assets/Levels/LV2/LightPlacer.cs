using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LightPlacer : Interactions
{
    public GameObject obj;
    public bool lightIsPlaced;

    public GameObject flashLight;
    public GameObject mazegateCollider;

    public Animator anim;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActiveState()
    {
        if (activeState)
        {
            if (lightIsPlaced)
            {
                text.text = "Pickup Flashlight";
            }
            else
            {
                text.text = "Place Flashlight";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (lightIsPlaced)
                {
                    anim.SetBool("Open", false);
                    anim.SetBool("Close", true);
                    lightIsPlaced = false;
                    flashLight.SetActive(true);
                    obj.SetActive(false);
                    mazegateCollider.SetActive(true);
                }
                else
                {
                    anim.SetBool("Close", false);
                    anim.SetBool("Open", true);
                    lightIsPlaced = true;
                    flashLight.SetActive(false);
                    obj.SetActive(true);
                    mazegateCollider.SetActive(false);
                }
            }
        }
    }
}
