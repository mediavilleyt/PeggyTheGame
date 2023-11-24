using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float RotationY;
    private float RotationX;
    private float InputX;
    private float InputY;

    // Start is called before the first frame update
    void Start()
    {
        RotationX = GameObject.FindGameObjectWithTag("Player").transform.rotation.eulerAngles.y;
        GameData.Instance.Pause(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.Instance.CanMove)
        {
            InputX = Input.GetAxis("Mouse X") * 1f;
            InputY = Input.GetAxis("Mouse Y") * 1f;

            RotationY -= InputY;
            RotationX += InputX;

            RotationY = Mathf.Clamp(RotationY, -65f, 75f);

            transform.localEulerAngles = new Vector3(RotationY, RotationX, 0);
        }
    }
}
