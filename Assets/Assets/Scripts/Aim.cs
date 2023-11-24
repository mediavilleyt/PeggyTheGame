using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private bool aiming;

    // Start is called before the first frame update
    void Start()
    {
        aiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            if (aiming) aiming = false;
            else aiming = true;
        }


        if (aiming)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(0, 0.5f, 0.3f), ref velocity, 0.2f);
        }
        else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, new Vector3(0.5f, 0.6f, 0.4f), ref velocity, 0.2f);
        }
    }

}
