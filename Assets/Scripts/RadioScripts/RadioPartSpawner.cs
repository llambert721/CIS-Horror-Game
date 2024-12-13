// Written by Logan Lambert

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
            Spawn(spawnPoints[i], radioParts[i]);
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

    void Spawn(Transform spawnPoint, GameObject spawnedObject)
    {
        spawnedObject.transform.position = spawnPoint.position;

        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit))
        {
            // Move the object to sit flush with the table based on the hit point
            Vector3 newPosition = hit.point;
            newPosition.y += spawnedObject.GetComponent<Collider>().bounds.extents.y;
            spawnedObject.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("No table detected below the spawn point!");
        }
    }
}
