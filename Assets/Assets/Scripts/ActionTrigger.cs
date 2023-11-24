using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ActionTrigger : Interactions
{
    public string actionName;
    public GameObject Triggered;

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
        text.text = actionName;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Triggered.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
