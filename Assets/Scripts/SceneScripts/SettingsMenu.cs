// Written by Marc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Text selectedResolutionText;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        PopulateDropdown();
        resolutionDropdown.onValueChanged.AddListener(UpdateSelectedResolution);
    }
    public void PopulateDropdown()
    {
        // Get available resolutions
        Resolution[] resolutions = Screen.resolutions;

        // Clear existing options
        resolutionDropdown.ClearOptions();

        // Create a list of resolution options
        List<string> options = new List<string>();
        foreach (var res in resolutions)
        {
            // Add unique resolution strings
            string option = res.width + " x " + res.height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }
        }

        // Add options to the dropdown
        resolutionDropdown.AddOptions(options);
    }
    public void UpdateSelectedResolution(int index)
    {
        // Parse the selected resolution
        string[] resolution = resolutionDropdown.options[index].text.Split('x');
        int width = int.Parse(resolution[0].Trim());
        int height = int.Parse(resolution[1].Trim());

        // Change screen resolution
        Screen.SetResolution(width, height, Screen.fullScreen);

        // Update the selected resolution text
        selectedResolutionText.text = resolutionDropdown.options[index].text;
    }

    // Method to set the volume in the audio mixer
    public void SetVolume(float volume)
    {
        // Set the audio mixer parameter for volume
        audioMixer.SetFloat("volume", volume);
    }

    // Method to toggle fullscreen mode
    public void SetFullscreen(bool isFullscreen)
    {
        // Set the screen to fullscreen or windowed mode based on user input
        Screen.fullScreen = isFullscreen;
    }
}
