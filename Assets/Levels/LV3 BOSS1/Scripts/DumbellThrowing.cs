using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbellThrowing : MonoBehaviour
{
    public GameObject Dumbell;
    public float Power;
    public DBLHolder LookForDBL;
    public AudioSource DumbellSound;
    public GameObject muzzleFlash;

    public float shotDelay;

    // Update is called once per frame
    void Update()
    {
        shotDelay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (CountDumbells() <= 0) return;
            else if (shotDelay < 1f) return;
            else
            {
                shotDelay = 0;
                GameData.Instance.isCBossInv = false;
                GameData.Instance.Objects.Remove("Dumbell");

                muzzleFlash.SetActive(true);
                Invoke("DisableMuzzleFlash", 0.1f);
                Quaternion rotation = transform.rotation;
                GameObject shot = Instantiate(Dumbell, transform.position, rotation);
                shot.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * Power);
                if (DumbellSound != null) DumbellSound.Play();

                Destroy(shot, 10.0f);
            }
        }
    }

    void DisableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    public int CountDumbells()
    {
        int count = 0;
        foreach(string obj in GameData.Instance.Objects)
        {
            if (obj == "Dumbell")
            {
                count++;
            }
        }
        return count;
    }

}
