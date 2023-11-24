using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Cutscene1 : MonoBehaviour
{
    public Animation animationComponent;

    //load Level 1-1 when animation is done
    private void Start()
    {
        // Attach an event handler to the animation's clip event
        AnimationClip clip = animationComponent.clip;
        if (clip != null)
        {
            AnimationEvent newEvent = new AnimationEvent
            {
                time = clip.length, // Event time at the end of the animation
                functionName = "OnAnimationFinish" // Call this function when the event occurs
            };
            clip.AddEvent(newEvent);
        }
    }

    private void OnAnimationFinish()
    {
        // Load a new scene when the animation finishes
        SceneManager.LoadScene("Level 1-1");
    }
}
