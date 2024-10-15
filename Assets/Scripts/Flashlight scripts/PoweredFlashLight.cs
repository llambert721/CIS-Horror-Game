using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlashLightState
{
    Off, // When the flashlight is off
    On, // When the flashilight is on
    Dead // When there is no battery left 
}


[RequireComponent(typeof(AudioSource))]
public class PoweredFlashLight : MonoBehaviour
{

    [Header("Options")]

    [Tooltip("The Speed that the battery is lost at")]  [Range(0.0f,2f)] [SerializeField] float batteryLossTick = 0.5f;

    [Tooltip("This is the amount of battery the player starts with.")] [SerializeField] int startBattery = 100;

    [Tooltip("This is the current amount of battery the player has.")] public int currentBattery;

    [Tooltip("The current state of the flashlight.")]public FlashLightState state;

    [Tooltip("Is the flashlight on?")]private bool flashlightIsOn;

    [Tooltip("The key that you need to press to turn on/off the flashlight.")] [SerializeField] KeyCode ToggleKey = KeyCode.F;

    [Header("References")]

    [Tooltip("Light that will be shown if the flashlight is on.")] [SerializeField] GameObject FlashlightLight;

    [Tooltip("Sounds that will be played when the flashlight is toggled")] [SerializeField] AudioClip FlashlightOn_FX, FlashlightOff_FX;
    private void Start()
    {
        currentBattery = startBattery; //Set the current battery to the start battery when the game starts

        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick); // Losses the battery at a set interval of time
    }
    private void Update()
    {
        if (Input.GetKeyDown(ToggleKey)) ToggleFlashlight(); // Toggles the flashlight

        // Handling the light that will be shown.
        if (state == FlashLightState.Off) FlashlightLight.SetActive(false);
        else if (state == FlashLightState.Dead) FlashlightLight.SetActive(false);
        else if (state == FlashLightState.On) FlashlightLight.SetActive(true);
        
        // Handling the battery being dead
        if (currentBattery <= 0)
        {
            currentBattery = 0;
            state = FlashLightState.Dead;
            flashlightIsOn = false; // Automatically turns flashlight off
        }
    }

    public void GainBattery(int amount) // Handles the gaining of battery
    {
        if (currentBattery == 0)
        {
            state = FlashLightState.On; // Makes flashlight Auto turn on once it recieves a battery while at 0
            flashlightIsOn = true;
        }


        if (currentBattery + amount > startBattery)
            currentBattery = startBattery; // Automatically cause battery to reach the cap  
        else 
            currentBattery += amount; // Adds the amount of battery to our current battery 

    }

    public void LoseBattery() // Handles the loss of battery
    {
        if (state == FlashLightState.On) currentBattery--; // subtracts the battery by 1, if the flashlight is on

    }
    
    public void ToggleFlashlight() // Toggles the on/off state of the flashlight
    {
        flashlightIsOn = !flashlightIsOn;

        if ( state == FlashLightState.Dead) flashlightIsOn = false; // Automatically overrides script if there is no battery

        // Handles the audio and action that happen when the flashlight is toggled
        if (flashlightIsOn)
        {
            if(FlashlightOn_FX != null) GetComponent<AudioSource>().PlayOneShot(FlashlightOn_FX); // Will play flashlight on sound

            state = FlashLightState.On; // Turn the flashlight on
        }
        else
        {
            if (FlashlightOff_FX != null) GetComponent<AudioSource>().PlayOneShot(FlashlightOff_FX); // Will play flashlight off sound

            state = FlashLightState.Off; // Turns the flashlight off
        }



    }




}