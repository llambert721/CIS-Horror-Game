using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundEffectsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Function()
    {
        
    }
}
