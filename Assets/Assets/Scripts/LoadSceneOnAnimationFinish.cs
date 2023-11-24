using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.PlayerLoop;
using TMPro;

public class LoadSceneOnAnimationFinish : MonoBehaviour
{
    private Animation animationComponent;
    private bool animationFinished = false;

    public UnityEngine.UI.Slider holdSlider;
    public TMPro.TMP_Text presSpaceText;

    private void Start()
    {
        presSpaceText.color = new Color(presSpaceText.color.r, presSpaceText.color.g, presSpaceText.color.b, 0);

        GameData.Instance.Pause(false);

        animationComponent = GetComponent<Animation>();
        if (animationComponent == null)
        {
            Debug.LogError("Animation component not found!");
            return;
        }

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
        if (!animationFinished)
        {
            animationFinished = true;
            // Load a new scene when the animation finishes
            SceneManager.LoadScene("Level 1 Cutscene 1");
        }
    }

    //hold SPACE to skip animation
    private void Update()
    {
        //fade in text when any button is pressed
        if (Input.anyKey)
        {
            presSpaceText.color = new Color(presSpaceText.color.r, presSpaceText.color.g, presSpaceText.color.b, 1);
        }
        else
        {
            presSpaceText.color = new Color(presSpaceText.color.r, presSpaceText.color.g, presSpaceText.color.b, presSpaceText.color.a - 1 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            holdSlider.value += 0.5f * Time.deltaTime;
            if (holdSlider.value >= 1)
            {
                SceneManager.LoadScene("Level 1 Cutscene 1");
            }
        }
        else
        {
            holdSlider.value -= 0.5f * Time.deltaTime;
        }
    }
}
