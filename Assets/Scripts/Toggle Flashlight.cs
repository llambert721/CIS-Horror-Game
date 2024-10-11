using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

    public AudioSource turnOn;
    public AudioSource turnOff;

    public bool on;
    public bool off;




    void Start()
    {
        off = true;
        flashlight.SetActive(false);
    }




    void Update()
    {
        if (off && Input.GetButtonDown("E"))
        {
            flashlight.SetActive(true);
            turnOn.Play();
            off = false;
            on = true;
        }
        else if (on && Input.GetButtonDown("E"))
        {
            flashlight.SetActive(false);
            turnOff.Play();
            off = true;
            on = false;
        }



    }
}

