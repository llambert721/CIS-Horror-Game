using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    private Transform playerCamera;
    private TextMeshProUGUI textMesh;
    private bool isHovering = false;
    public LayerMask pickupLayer;
    public float pickupRange = 3f;
    public KeyCode keybind = KeyCode.E;
    public GameObject textMeshObject;
    public GameObject monsterObject; // so it gets more angry as the player progresses


    void Start()
    {
        textMesh = textMeshObject.GetComponent<TextMeshProUGUI>();
        playerCamera = Camera.main.transform; // gets camera transform for ray casting
    }

    void Update()
    {
        isHovering = false;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        string colliderName = gameObject.name;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer)) // if raycast hits item with layer applied and keybind held down run PickupItem()
        {
            isHovering = true;
            colliderName = hit.collider.gameObject.name;
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(keybind))
                {
                    PickupItem(gameObject);
                }
            }
        }

        DisplayText(colliderName);
    }

    public virtual void PickupItem(GameObject obj) // logs pickup to console and deletes object
    {
        Debug.Log("Picked up " + obj.name);



        Destroy(obj);
    }

    void DisplayText(string name) // Display pickup info tip to screen
    {
        name = name.ToLower();
        textMesh.text = "Press 'E' to pickup " + name;

        if (isHovering)
        {
            Debug.Log("Is Hovering");
            textMesh.enabled = true;
        }
        else
        {
            Debug.Log("Is NOT hovering");
            textMesh.enabled = false;
        }
    }
}
