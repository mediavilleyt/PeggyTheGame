using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectSwapTrigger : Interactions
{
    public string objectName;

    public GameObject newObject;
    public string requiredObject;
    public string CanPickupText = "";
    public string CantPickupText = "";

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
        if (GameData.Instance.Objects.Contains(requiredObject))
        {
            text.text = CanPickupText;

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameData.Instance.Objects.Remove(requiredObject);
                GameData.Instance.Objects.Add(objectName);
                GameData.Instance.Info = "You picked up " + objectName;
                Destroy(newObject);
                Destroy(this.gameObject);
            }
        }

        else
        {
            text.text = CantPickupText;
        }
    }
}
