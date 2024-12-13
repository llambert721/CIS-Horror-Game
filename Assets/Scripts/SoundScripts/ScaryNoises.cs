// Written by Logan Lambert

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryNoises : MonoBehaviour
{
    public AudioSource[] sources;
    public AudioClip[] clips;
    public float maxTime = 40f;
    public float minTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (sources.Length < 1 || clips.Length < 1)
        {
            Debug.LogWarning("Either no sources or clips assigned to scary noises");
        }

        StartCoroutine(ScaryNoiseEnumerator());
    }

    private IEnumerator ScaryNoiseEnumerator()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            int sourceIndex = Random.Range(0, sources.Length);
            int clipIndex = Random.Range(0, clips.Length);

            AudioSource source = sources[sourceIndex];
            AudioClip clip = clips[clipIndex];

            source.PlayOneShot(clip);
        }
    }
}
