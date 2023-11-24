using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetDumbellCount : MonoBehaviour
{
    TMP_Text counter;
    public DumbellThrowing dblscript;

    private void Start()
    {
        counter = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        counter.text = dblscript.CountDumbells().ToString();
    }
}
