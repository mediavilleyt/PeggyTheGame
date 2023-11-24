using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.Utility;

public class Head : MonoBehaviour
{
    public int HP;
    public float attackInterval;
    public TheTrifecta Who;
    GameObject Player;

    float timer;
    float Rotation = 0;
    public AImovement movement;
    public GameObject AttackObject;

    public GameObject nextHead;
    public Material headMat;
    public ParticleSystem explosion;

    public GameObject[] components;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        this.GameObject().GetComponent<MeshRenderer>().material = headMat;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (HP <= 0)
        {
            Destroy(gameObject);

            explosion.Play();
            audioSource.Play();
            foreach (GameObject component in components)
            {
                component.SetActive(false);
            }

            if(Who != TheTrifecta.Lennert)
            {
                nextHead.GetComponent<Head>().enabled = true;
            }
        }
        if (timer >= attackInterval)
        {
            timer = 0;
            StartCoroutine(Attack());
        }

        if (Who != TheTrifecta.Tijl)
        {
            transform.LookAt(Player.transform.position);
        }
        if (Who == TheTrifecta.Tijl)
        {
            Quaternion Lookat = Quaternion.LookRotation(Player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Lookat, 1f * Time.deltaTime);
        }
    }
    public IEnumerator Attack()
    {
        switch (Who)
        {
            case TheTrifecta.Viktor:
                bool end = true;
                bool comeback = false;
                Vector3 OriginalPos = transform.localPosition;
                while (end)
                {
                    if (!comeback) transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(OriginalPos.x - 3, OriginalPos.y - 4, OriginalPos.z + 25), 30 * Time.deltaTime);
                    if (comeback) transform.localPosition = Vector3.MoveTowards(transform.localPosition, OriginalPos, 10 * Time.deltaTime);
                    if (transform.localPosition == new Vector3(OriginalPos.x - 3, OriginalPos.y - 4, OriginalPos.z + 25)) comeback = true;
                    if (transform.localPosition == OriginalPos) end = false;
                    yield return new WaitForEndOfFrame();
                }
                break;
            case TheTrifecta.Tijl:
                if (CheckHeads() != 2) break;
                yield return new WaitForSeconds(3);
                AttackObject.SetActive(true);
                yield return new WaitForSeconds(1);
                AttackObject.SetActive(false);
                break;
            case TheTrifecta.Lennert:
                if (CheckHeads() != 1) break;
                while (true)
                {
                    movement.speed += 0.01f * Time.deltaTime;
                    movement.TurnSpeed += 0.01f * Time.deltaTime;
                    Rotation += 10 * Time.deltaTime;
                    AttackObject.transform.localRotation = Quaternion.Euler(Rotation, 0, 0);
                    yield return new WaitForEndOfFrame();
                }
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameData.Instance.isCBossInv == true) return;
        if (collision.transform.CompareTag("Dumbell"))
        {
            switch (Who)
            {
                case TheTrifecta.Viktor:
                    HP -= 1;
                    GameData.Instance.BossHealth -= 1;
                    GameData.Instance.isCBossInv = true;
                    break;
                case TheTrifecta.Tijl:
                    if (CheckHeads() != 2) break;
                    HP -= 1;
                    GameData.Instance.BossHealth -= 1;
                    GameData.Instance.isCBossInv = true;
                    break;
                case TheTrifecta.Lennert:
                    if (CheckHeads() != 1) break;
                    HP -= 1;
                    GameData.Instance.BossHealth -= 1;
                    GameData.Instance.isCBossInv = true;
                    movement.speed = 1;
                    movement.TurnSpeed = 1;
                    break;
            }
            Debug.Log(GameData.Instance.BossHealth);

        }
    }

    private int CheckHeads()
    {
        GameObject[] heads = GameObject.FindGameObjectsWithTag("Head");
        return heads.Length;
    }

}
public enum TheTrifecta
{
    Viktor,
    Lennert,
    Tijl
}