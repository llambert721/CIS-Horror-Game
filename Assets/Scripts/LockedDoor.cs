// Written by Amin Umer
// 11/6/2024
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Animator Door;
    public GameObject openText;

    public AudioSource doorSound;


    public bool inReach;




    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }





    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorOpens();
        }

        else
        {
            DoorCloses();
        }




    }
    void DoorOpens()
    {
        Debug.Log("It Opens");
        Door.SetBool("Open", true);
        Door.SetBool("Closed", false);
        doorSound.Play();

    }

    void DoorCloses()
    {
        Debug.Log("It Closes");
        Door.SetBool("Open", false);
        Door.SetBool("Closed", true);
    }


}