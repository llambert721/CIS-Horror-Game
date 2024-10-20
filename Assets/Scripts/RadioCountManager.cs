using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioCountManager : MonoBehaviour
{
    public int count = 0;

    public void AddRadio()
    {
        count++;
    }

    public bool IsRadioPartsCollected()
    {
        if (count == 3)
        {
            return true;
        }

        return false;
    }
}
