using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnOnTerrain : MonoBehaviour
{
    public Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (terrain != null)
        {
            Vector3 position = transform.position;
            position.y = terrain.SampleHeight(position) + terrain.transform.position.y;
            transform.position = position;
        }
    }
}
