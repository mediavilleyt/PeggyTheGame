using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Adjust this to control the rotation speed

    // Update is called once per frame
    void Update()
    {
        // Rotate the propeller around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
