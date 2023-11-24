using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEANDIESOMG : MonoBehaviour
{
    public string[] Dialogue;
    int count = 0;
    float timer = 0;
    public ParticleSystem boom;
    bool played = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (count != Dialogue.Length) GameData.Instance.Subs = Dialogue[count];
        if (Input.GetMouseButtonDown(0) && count < Dialogue.Length && timer > 0.5f)
        {
            count++;
            timer = 0;
        }
        if (count == Dialogue.Length && !played)
        {
            boom.Play();
            played = true;
        }
        if (count == Dialogue.Length && timer > 1)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
