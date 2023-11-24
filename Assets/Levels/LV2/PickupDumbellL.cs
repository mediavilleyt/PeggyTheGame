using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupDumbellL : MonoBehaviour
{
    public Animator L2Anim;
    public GameObject Player;

    private void Awake()
    {
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.GetComponent<PlayerLook>().enabled = false;
        pickedUpDumbell();
    }

    public void pickedUpDumbell()
    {
        L2Anim.SetBool("DoorIsOpened", true);
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<PlayerLook>().enabled = true;
    }
}
