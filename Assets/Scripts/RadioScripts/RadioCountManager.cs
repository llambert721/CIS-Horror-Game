using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioCountManager : MonoBehaviour
{
    public int count = 0;
    public GameObject textMeshObject;
    public GameObject messageObject;
    public float totalTime = 300f;
    public bool timerEnded = false;
    public float fadeDuration = 2f;

    private TextMeshProUGUI textMesh;
    private TextMeshProUGUI message;

    void Start()
    {
        textMesh = textMeshObject.GetComponent<TextMeshProUGUI>();
        message = messageObject.GetComponent<TextMeshProUGUI>();

        messageObject.SetActive(true);
        message.enabled = false;
    }

    public void AddRadio()
    {
        count++;
    }

    public bool IsRadioPartsCollected()
    {
        if (count >= 3)
        {
            return true;
        }

        return false;
    }

    public void StartEndGame()
    {
        message.text = "Survive and make it to the beach...";
        message.enabled = true;

        StartCoroutine(FadeOutAfterDelay(5f));
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (totalTime > 0)
        {
            // Update timer each frame
            totalTime -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);

            // Format time based on remaining minutes
            if (minutes > 0)
            {
                // Display without leading zero for minutes
                textMesh.text = string.Format("{0}:{1:00}", minutes, seconds);
            }
            else
            {
                // Display with leading zero for seconds when under 1 minute
                textMesh.text = string.Format("0:{0:00}", seconds);
            }

            yield return null;
        }

        // Set timer to 0 and display "00:00" when done
        textMesh.text = "";
        timerEnded = true;
    }

    IEnumerator FadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color originalColor = message.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            message.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        message.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);  // Ensure it's fully transparent at the end
    }

    public bool IsTimerEnded()
    {
        return timerEnded;
    }
}
