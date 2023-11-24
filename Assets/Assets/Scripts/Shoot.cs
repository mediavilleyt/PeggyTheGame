using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //for shooting
    public GameObject projectile;
    public float velocity = 500F;

    //for full auto shooting
    public bool Auto;
    public float interval;
    bool started;

    //for sound
    public AudioSource shot;
    public void shotSFX() //plays sound
    {
        if (shot != null) shot.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Auto)
        {
            AutoFire();
        }
        else
        {
            NormalFire();
        }
        if (!started)
        {
            StopAllCoroutines();
        }
    }

    void NormalFire()
    {
        //shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }


    void AutoFire() //when the mouse is hold in, the IEnumerator will do a loop until it is let loose
    {
        if (Input.GetButton("Fire1") && started == false)
        {
            started = true;
            StartCoroutine(FullAuto());
        }
        else if (Input.GetButton("Fire1")){ }
        else
        {
            started = false;
        }
    }

    private IEnumerator FullAuto()
    {
        while (started)
        {
            yield return new WaitForSeconds(interval);
            if (started) Fire();
        }
        yield return null;
    }

    void Fire() //basics for shooting
    {
        //shooting
        Quaternion rotation = transform.rotation;
        rotation.Set(rotation.x, rotation.y, rotation.z, rotation.w);
        GameObject shot = Instantiate(projectile, transform.position, rotation);
        shot.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, velocity, 0));
        shotSFX();

        Destroy(shot, 10.0f);
    }


}
