using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PickupTrigger : Interactions
{
    public GameObject obj;
    public string objectName;

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
        text.text = objectName;

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameData.Instance.Objects.Add(objectName);
            GameData.Instance.Info = "You picked up " + objectName;
            Destroy(obj);
            Destroy(this.gameObject);
        }
    }
}
