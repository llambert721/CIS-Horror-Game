using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : ItemPickup
{
    public override void PickupItem(GameObject obj)
    {
        FindAnyObjectByType<KeyManager>().UpdateKey();

        base.PickupItem(obj);
    }
}
