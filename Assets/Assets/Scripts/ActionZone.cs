using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    public string RequiredObject;
    public string GetObject;
    public GameObject StuffThatGetsRemoved;
    public GameObject StuffThatActivates;

    GameObject Player;

    public string MessageIfHasRequired;
    public string MessageIfHasNotRequired;
    public int distance;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < distance || Vector3.Distance(transform.position, Player.transform.position) < transform.localScale.z)
        {
            if (CheckIfHasRequiredObject())
            {
                GameData.Instance.Subs = MessageIfHasRequired + " [Left Mouse Button]";
                if (Input.GetMouseButtonDown(0))
                {
                    if (StuffThatActivates != null) StuffThatActivates.SetActive(true);
                    if (GetObject != "")
                    {
                        GameData.Instance.Info = "You received " + GetObject;
                        GameData.Instance.Objects.Add(GetObject);
                    }
                    if (StuffThatGetsRemoved != null) Destroy(StuffThatGetsRemoved);
                    Destroy(this);
                }
            }
            else
            {
                GameData.Instance.Subs = MessageIfHasNotRequired;
            }
        }
    }

    bool CheckIfHasRequiredObject()
    {
        bool end = false;
        if (RequiredObject == "") return true;
        foreach (string obj in GameData.Instance.Objects)
        {
            if (obj == RequiredObject)
            {
                end = true;
            }
        }
        return end;
    }
}
