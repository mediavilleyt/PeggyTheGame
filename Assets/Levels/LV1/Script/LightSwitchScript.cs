using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    public GameObject Bar1;
    public GameObject Bar2;
    public GameObject Bar3;

    private void OnEnable()
    {
        Bar1.SetActive(true);
        Bar2.SetActive(true);
        Bar3.SetActive(true);

        Destroy(this);
    }
}
