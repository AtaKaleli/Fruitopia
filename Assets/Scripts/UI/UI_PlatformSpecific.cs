using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlatformSpecific : MonoBehaviour
{
    public GameObject joystick;       // Reference to the Variable Joystick
    public GameObject jumpButton;     // Reference to the Jump Button
    public GameObject pauseButton;     // Reference to the Pause Button

    void Start()
    {
        //Application.isEditor
        // Disable mobile controls if running in the Editor or on non-mobile platforms
        if (!Application.isMobilePlatform)
        {
            joystick.SetActive(false);
            jumpButton.SetActive(false);
            pauseButton.SetActive(false);
        }
        else
        {
            // Enable mobile controls on mobile platforms
            joystick.SetActive(true);
            jumpButton.SetActive(true);
            pauseButton.SetActive(true);
        }
    }
}
