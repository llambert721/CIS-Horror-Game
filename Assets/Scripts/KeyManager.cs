using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public bool hasKey;
    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;
    }

    public void UpdateKey()
    {
        hasKey = true;
    }
}
