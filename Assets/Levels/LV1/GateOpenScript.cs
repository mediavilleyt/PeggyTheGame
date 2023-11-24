using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateOpenScript : Interactions
{
    public bool chainCut;

    public string objectNeeded;
    public string triggerText;
    public string cantTriggerText;
    public Animation gateOpen;
    public GameObject chains;

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
        if (chainCut)
        {
            text.text = "Open the gate";

            if (Input.GetKeyDown(KeyCode.E))
            {
                gateOpen.Play();
                GameData.Instance.Objects.Remove(objectNeeded);
                Destroy(this.gameObject);

            }
        }

        if (GameData.Instance.Objects.Contains(objectNeeded))
        {
            if (chainCut)
            {
                return;
            }

            text.text = triggerText;

            if (Input.GetKeyDown(KeyCode.E))
            {
                chains.SetActive(false);
                chainCut = true;
            }
        }

        else
        {
            if (chainCut)
            {
                return;
            }

            text.text = cantTriggerText;
        }
    }
}
