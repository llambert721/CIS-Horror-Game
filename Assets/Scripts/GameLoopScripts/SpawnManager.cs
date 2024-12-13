// Written by Logan Lambert

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnTextObject;
    public GameObject player;
    public float fadeDuration = 2f;
    public List<GameObject> spawnList = new List<GameObject>();
    public GameObject worldLighting;

    private TextMeshProUGUI spawnText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnPoint = PickRandomSpawnPoint();
        spawnText = spawnTextObject.GetComponent<TextMeshProUGUI>();

        player.transform.position = spawnPoint.transform.position;

        worldLighting.SetActive(false);

        spawnText.enabled = true;
        spawnText.text = "Collect 3 Radio Parts...";

        StartCoroutine(FadeOutAfterDelay(5f));
    }

    IEnumerator FadeOutAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color originalColor = spawnText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spawnText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        spawnText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);  // Ensure it's fully transparent at the end
    }

    private GameObject PickRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnList.Count);

        return spawnList[randomIndex];
    }
}
