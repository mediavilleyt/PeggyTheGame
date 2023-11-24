using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBLHolder : MonoBehaviour
{
    public GameObject FL;
    public GameObject DBL = null;
    [HideInInspector]
    public Things ThingType = Things.Nothing;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ThingType == Things.Flash) Switch(Things.Nothing);
            else Switch(Things.Flash);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (DBL == null || !GameData.Instance.Objects.Contains("Military Grade Dumbell Launcher")) return;
            if (ThingType == Things.DumbellLauncher) Switch(Things.Nothing);
            else Switch(Things.DumbellLauncher);
        }
    }

    public void Switch(Things Thing)
    {
        ThingType = Thing;
        bool noDBL = false;
        if (DBL == null)
        {
            noDBL = true;
        }
        if (Thing == Things.Nothing)
        {
            FL.SetActive(false);
            if (!noDBL) DBL.SetActive(false);
        }
        else if (Thing == Things.Flash)
        {
            FL.SetActive(true);
            if (!noDBL) DBL.SetActive(false);
        }
        else if (Thing == Things.DumbellLauncher)
        {
            FL.SetActive(false);
            if (!noDBL) DBL.SetActive(true);
        }
    }
}

public enum Things
{
    Flash,
    DumbellLauncher,
    Nothing
}
