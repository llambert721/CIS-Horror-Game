using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSoundsScript : MonoBehaviour
{
    public List<AudioClip> soundsList;
    public AudioSource audioSource;
    public GameObject player;
    public GameObject monster;

    public float maxDistance = 20f;
    public float minInterval = .3f;
    public float maxInterval = 2f;

    private Coroutine heartbeatCoroutine;
    private int currentClipIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (soundsList.Count > 0 && audioSource != null)
        {
            heartbeatCoroutine = StartCoroutine(HeartbeatLoop());
        }
        else
        {
            Debug.LogWarning("HeartbeatManager: Missing AudioSource or Heartbeat Clips.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HeartbeatLoop()
    {
        while (true)
        {
            float distance = Vector3.Distance(player.transform.position, monster.transform.position);

            if (distance <= maxDistance)
            {
                // Adjust heartbeat interval based on distance
                float t = Mathf.InverseLerp(maxDistance, 0, distance);
                float heartbeatInterval = Mathf.Lerp(maxInterval, minInterval, t);

                // Play a random heartbeat sound if not already playing
                if (!audioSource.isPlaying)
                {
                    AudioClip randomClip = soundsList[currentClipIndex];
                    audioSource.clip = randomClip;
                    audioSource.Play();

                    // Move to the next clip, looping back to the start if needed
                    currentClipIndex = (currentClipIndex + 1) % soundsList.Count;
                }

                // Wait for either the heartbeat interval or the clip length, whichever is longer
                float waitTime = Mathf.Max(heartbeatInterval, audioSource.clip.length);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                // If monster is out of range, stop playback and wait a bit before checking again
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void OnDestroy()
    {
        if (heartbeatCoroutine != null)
        {
            StopCoroutine(heartbeatCoroutine);
        }
    }
}
