using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public Terrain terrain;

    public AudioClip grass;
    public AudioClip gravel;
    public AudioClip sand;
    public AudioClip building;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;

    public void FootStep()
    {
        if (Physics.Raycast(rayStart.position, Vector3.down, out hit, range, layerMask))
        {
            // Check for tagged objects first
            if (hit.collider.CompareTag("Building"))
            {
                PlayFootStepSoundL(building);
                return;
            }

            Vector3 terrainPos = hit.point - terrain.transform.position;
            TerrainData terrainData = terrain.terrainData;
            int mapX = Mathf.RoundToInt(terrainPos.x / terrainData.size.x * terrainData.alphamapWidth);
            int mapZ = Mathf.RoundToInt(terrainPos.z / terrainData.size.z * terrainData.alphamapHeight);

            float[,,] alphaMap = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
            int dominantTexture = 0;
            float maxOpacity = 0f;

            for (int i = 0; i < terrainData.alphamapLayers; i++)
            {
                if (alphaMap[0, 0, i] > maxOpacity)
                {
                    maxOpacity = alphaMap[0, 0, i];
                    dominantTexture = i;
                }
            }

            // Play sound based on the detected terrain texture
            switch (dominantTexture)
            {
                case 0:
                    PlayFootStepSoundL(sand);
                    break;
                case 1:
                    PlayFootStepSoundL(grass);
                    break;
                case 2:
                    PlayFootStepSoundL(gravel);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayFootStepSoundL(AudioClip audioClip)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audioClip);
    }

    private void Update()
    {
        FootStep();
    }
}
