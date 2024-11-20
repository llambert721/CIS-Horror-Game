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
            FindObjectOfType<RadioCountManager>().StartEndGame();
        }

        base.PickupItem(obj);
    }
}
