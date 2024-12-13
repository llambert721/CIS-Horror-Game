// Written by Logan Lambert

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : ItemPickup
{
    // overrides pickup method from ItemPickup
    // sets battery life to max on pickup
    public override void PickupItem(GameObject obj)
    {
        FindObjectOfType<PoweredFlashLight>().GainBattery(100);

        base.PickupItem(obj);
    }
}
