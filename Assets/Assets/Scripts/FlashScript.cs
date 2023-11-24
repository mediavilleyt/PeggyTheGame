using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{
    public GameObject FL;
    bool isFlashEnabled;

    // Start is called before the first frame update
    void Start()
    {
        FL.SetActive(false);
        isFlashEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (isFlashEnabled)
            {
                case false:
                    isFlashEnabled = true;
                    FL.SetActive(true); 
                    break;

                case true:
                    isFlashEnabled = false;
                    FL.SetActive(false); 
                    break;
            }
        }
    }
}
