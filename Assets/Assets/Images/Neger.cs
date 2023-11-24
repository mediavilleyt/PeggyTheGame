using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neger : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform.position);
    }
}
