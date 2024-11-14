using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public Terrain terrain;

    public AudioClip[] grass;
    public AudioClip[] gravel;
    public AudioClip[] building;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;

    private Dictionary<AudioClip[], int> lastPlayedIndices = new Dictionary<AudioClip[], int>();

    public void FootStep()
    {
        if (Physics.Raycast(rayStart.position, Vector3.down, out hit, range, layerMask))
        {
            // Check for tagged objects first
            if (hit.collider.CompareTag("Building"))
            {
                Debug.Log("walking on building");
                PlayRandomClip(building);
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
                    Debug.Log("walking on grass");
                    PlayRandomClip(grass);
                    break;
                case 1:
                    Debug.Log("walking on sand");
                    PlayRandomClip(grass);
                    break;
                case 2:
                    Debug.Log("walking on gravel");
                    PlayRandomClip(gravel);
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
    private void PlayRandomClip(AudioClip[] clips)
    {
        if (clips.Length == 0) return;

        // Get the last index from the dictionary or set it to -1 if it doesn't exist
        int lastIndex = lastPlayedIndices.ContainsKey(clips) ? lastPlayedIndices[clips] : -1;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, clips.Length);
        } while (newIndex == lastIndex);

        // Update the dictionary with the new last index
        lastPlayedIndices[clips] = newIndex;

        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(clips[newIndex]);
    }
}
