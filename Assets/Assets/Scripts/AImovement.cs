using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    public string Name;

    GameObject player;
    public float speed;
    public float TurnSpeed;

    public bool CanMove;

    public bool LimitArea;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            //movement
            Quaternion Lookat = Quaternion.LookRotation(new Vector3(player.transform.position.x, 1, player.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Lookat, TurnSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (LimitArea)
        {
            if (transform.position.y > 20 || transform.position.y < -4) transform.position = new Vector3(0, 10, 0);
        }
    }
}
