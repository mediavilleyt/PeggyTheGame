using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDumbell : MonoBehaviour
{
    public GameObject obj;
    public string Name;

    public int Distance = 3;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < Distance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameData.Instance.Objects.Add(Name);
                GameData.Instance.Info = "You picked up " + Name;
                Destroy(obj);
            }
        }
    }
}
