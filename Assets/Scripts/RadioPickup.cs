using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPickup : ItemPickup
{
    public override void PickupItem(GameObject obj)
    {
        FindObjectOfType<RadioCountManager>().AddRadio();

        if (FindObjectOfType<RadioCountManager>().IsRadioPartsCollected())
        {
            Debug.Log("You won");
        }

        base.PickupItem(obj);
    }
}
