using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaceParts : MonoBehaviour
{
    private Renderer[] childRenderers;
    private RadioCountManager radioCountManager;
    private KeyCode keyBind = KeyCode.E;
    private bool isHovering = false;
    private TextMeshProUGUI textMesh;
    private bool hasPlacedParts = false; // Tracks if the parts have been placed
    private bool hasInteractedWithoutParts = false; // Tracks if the player tried to interact without parts

    public LayerMask pickupLayer;
    public GameObject textMeshObject;

    // Start is called before the first frame update
    void Start()
    {
        radioCountManager = FindAnyObjectByType<RadioCountManager>();
        textMesh = textMeshObject.GetComponent<TextMeshProUGUI>();

        // Disable the Renderer components of all child GameObjects
        childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer childRend in childRenderers)
        {
            childRend.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // Adjust this if necessary

        if (Physics.Raycast(ray, out hit, 3f, pickupLayer))
        {
            // Check if the ray hit this GameObject
            if (hit.collider.gameObject == gameObject)
            {
                if (hasPlacedParts)
                {
                    // If parts are placed, do not show text again
                    textMesh.enabled = false;
                    return;
                }

                if (!hasInteractedWithoutParts)
                {
                    // Show initial interaction message
                    textMesh.text = "Press 'E' to place radio parts";
                    textMesh.enabled = true;
                }

                isHovering = true;

                if (Input.GetKeyDown(keyBind))
                {
                    if (CanPartsBePlaced())
                    {
                        textMesh.enabled = false;
                        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
                        foreach (Renderer childRend in childRenderers)
                        {
                            childRend.enabled = true;
                        }

                        hasPlacedParts = true;
                    }
                    else
                    {
                        textMesh.text = "You dont have all parts";
                        hasInteractedWithoutParts = true;
                    }

                    
                }
                return;
            }
        }
        if (isHovering)
        {
            textMesh.enabled = false; // Hide text when not looking at the object
            isHovering = false;

            if (!hasPlacedParts)
            {
                // Reset text only if parts haven't been placed
                hasInteractedWithoutParts = false;
            }
        }
    }

    private bool CanPartsBePlaced()
    {
        if (!radioCountManager.IsRadioPartsCollected())
        {
            return false;
        }

        return true;
    }

    private void DisplayText(string text)
    {
        textMesh.text = text;

        if (isHovering)
        {
            textMesh.enabled = true;
        }
        else
        {
            textMesh.enabled = false;
        }
    }
}
