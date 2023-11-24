using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public CharacterController CharacterController;

    Vector3 GravityEffect;
    float Gravity = -50;
    public float AttackTimer = 0;
    bool CanLosHelth;

    public bool LimitArea;

    private void Start()
    {
        GameData.Instance.Helth = 100;
        GameData.Instance.CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        AttackTimer = AttackTimer - 1 * Time.deltaTime;

        if(AttackTimer <= 0)
        {
            CanLosHelth = true;
        }

        if (GameData.Instance.CanMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Speed *= 1.5f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Speed /= 1.5f;
            }

            Vector3 Movement = transform.right * Time.deltaTime * Speed * Input.GetAxis("Horizontal") + transform.forward * Time.deltaTime * Speed * Input.GetAxis("Vertical");
            CharacterController.Move(Movement);

            GravityEffect.y = Gravity * Time.deltaTime;
            CharacterController.Move(GravityEffect);
        }
        if (LimitArea)
        {
            if (transform.position.y > 20 || transform.position.y < -4) transform.position = new Vector3(0, 10, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Head")
        {
            if(CanLosHelth == true)
            {
                GameData.Instance.Helth -= 20;
                CanLosHelth = false;
                AttackTimer = 4;
            }
        }

        if(collision.transform.tag == "Laser")
        {
            GameData.Instance.Helth -= 5;
        }

        Debug.Log(collision.gameObject.name);
    }

}
