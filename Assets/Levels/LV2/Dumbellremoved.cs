using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbellremoved : MonoBehaviour
{
    public GameObject getsEnabled;

    //enable object if child gets removed
    private void Update()
    {
        if (transform.childCount == 0)
        {
            getsEnabled.SetActive(true);
        }
    }
}
