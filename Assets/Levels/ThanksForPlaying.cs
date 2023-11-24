using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThanksForPlaying : MonoBehaviour
{
    public Image BlackScreen;
    public TMP_Text credits;
    public string[] creditsNames;

    private void Start()
    {
        StartCoroutine(FadeOutThenIn());
    }

    IEnumerator FadeOutThenIn()
    {
        float fadeDuration = 1.0f;
        Color startColor = new Color(0, 0, 0, 1);
        Color endColor = new Color(0, 0, 0, 0);

        // Fade Out
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            BlackScreen.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        BlackScreen.color = endColor; // Ensure it's fully transparent

        // Display Credits
        foreach (string creditName in creditsNames)
        {
            credits.text = creditName;
            Color textStartColor = new Color(1, 1, 1, 0);
            Color textEndColor = new Color(1, 1, 1, 1);

            // Fade In
            if (creditName != creditsNames[0])
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
                {
                    credits.color = Color.Lerp(textStartColor, textEndColor, t);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(2);

            // Fade Out
            if(creditName != creditsNames[creditsNames.Length - 1])
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
                {
                    credits.color = Color.Lerp(textEndColor, textStartColor, t);
                    yield return null;
                }
            }   
        }

        // Fade In
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            BlackScreen.color = Color.Lerp(endColor, startColor, t);
            yield return null;
        }

        BlackScreen.color = startColor; // Ensure it's fully opaque

        // Return to Main Menu
        SceneManager.LoadScene("Main Menu");
    }
}
