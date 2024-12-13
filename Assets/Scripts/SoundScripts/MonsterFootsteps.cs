// Written by Logan Lambert

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFootsteps : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;

    private Dictionary<AudioClip[], int> lastPlayedIndices = new Dictionary<AudioClip[], int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Footstep()
    {
        if (Physics.Raycast(rayStart.position, Vector3.down, out hit, range, layerMask))
        {
            PlayRandomClip(audioClips);
        }
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
