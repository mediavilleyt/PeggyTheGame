using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
