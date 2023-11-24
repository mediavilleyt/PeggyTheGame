using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public int MaxAmount;
    public float Radius;
    public float Interval;

    int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            if (counter == MaxAmount && MaxAmount != 0) break;
            counter ++;
            float X = Random.Range(-Radius, Radius);
            float Z = Random.Range(-Radius, Radius);
            Instantiate(ObjectToSpawn, new Vector3(X, transform.position.y, Z), transform.rotation);
        }
        yield return null;
    }

}
