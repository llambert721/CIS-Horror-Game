using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Written by Logan Lambert

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image fadeImage; // Drag your UI Image here in the inspector
    public float fadeDuration = 1f; // Duration of the fade
    public GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<RadioCountManager>().IsTimerEnded())
        {
            monster.SetActive(false);
            FadeToBlack();
        }
    }

    public void FadeToBlack()
    {
        StartCoroutine(Fade(0f, 1f)); // Fade from transparent to black
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
        SceneManager.LoadScene("WinScreen");
    }
}
