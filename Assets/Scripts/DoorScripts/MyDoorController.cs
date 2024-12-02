using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyDoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isOpen = false;
    private bool isCooldown = false;

    [SerializeField] private float cooldownTime = 1.0f;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (isCooldown)
            return;
        if (!isOpen)
        {
            doorAnimator.Play("DoorOpen", 0, 0.0f);
            isOpen = true;
        }
        else
        {
            doorAnimator.Play("DoorClose", 0, 0.0f);
            isOpen = false;
        }
        StartCoroutine(DoorCooldown());
    }
    private System.Collections.IEnumerator DoorCooldown()
    {
        isCooldown = true; // Activate cooldown
        yield return new WaitForSeconds(cooldownTime); // Wait for the cooldown time
        isCooldown = false; // Deactivate cooldown
    }
}
