using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioCountManager : MonoBehaviour
{
    public int count = 0;
    public GameObject textMeshObject;
    public float totalTime = 300f;
    public bool timerEnded = false;

    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = textMeshObject.GetComponent<TextMeshProUGUI>();
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

    public bool IsTimerEnded()
    {
        return timerEnded;
    }
}
