using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPartSpawner : MonoBehaviour
{
    public List<GameObject> radioParts;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (radioParts.Count != 3 || spawnPoints.Count != 6)
        {
            Debug.LogError("You need exactly 3 radio parts and 6 spawn points.");
            return;
        }

        SpawnRadioParts();
    }

    void SpawnRadioParts()
    {
        ShuffleSpawnPoints();

        for (int i = 0; i < 3; i++)
        {
            radioParts[i].transform.position = spawnPoints[i].position;
        }

    }

    // Fisher-Yates Shuffle Algorithm to shuffle the spawn points list
    void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }
}
