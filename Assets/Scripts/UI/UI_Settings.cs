using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{

    [SerializeField] private string mixerParameter;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private float sliderMultiplier = 25;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Awake()
    {
        SetupVolumeSlider();
    }

    public void SetupVolumeSlider()
    {
        slider.onValueChanged.AddListener(SliderValue);
        slider.minValue = .001f;
        slider.value = PlayerPrefs.GetFloat(mixerParameter, slider.value);
    }

    private void SliderValue(float value)
    {
        sliderText.text = Mathf.RoundToInt(value * 100) + "%";
        audioMixer.SetFloat(mixerParameter, Mathf.Log10(value) * sliderMultiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(mixerParameter, slider.value);
    }
}
