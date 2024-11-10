using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject key;
    public List<Transform> spawnPoints;
    public bool hasKey;

    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;

        Transform spawnPoint = PickSpawnPoint();
        Spawn(spawnPoint, key);
    }

    public void UpdateKey()
    {
        hasKey = true;
    }

    public Transform PickSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);

        return spawnPoints[randomIndex];
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
