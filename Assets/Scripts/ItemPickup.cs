using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Transform playerCamera;
    public LayerMask pickupLayer;
    public float pickupRange = 3f;
    public KeyCode keybind = KeyCode.E;

    void Start()
    {
        playerCamera = Camera.main.transform; // gets camera transform for ray casting
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer)) // if raycast hits item with layer applied and keybind held down run PickupItem()
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(keybind))
                {
                    PickupItem(gameObject);
                }
            }
        }
    }

    public virtual void PickupItem(GameObject obj) // logs pickup to console and deletes object
    {
        Debug.Log("Picked up " + obj.name);

        Destroy(obj);
    }
}
