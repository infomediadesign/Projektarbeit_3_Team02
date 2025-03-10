using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        // Initialize sliders with saved values or default to max
        if (MusicManager.Instance != null)
        {
            musicSlider.value = MusicManager.Instance.GetVolume() * 10f; // Scale from 0-1 to 0-10
        }

        if (SoundManager.Instance != null)
        {
            sfxSlider.value = SoundManager.Instance.GetVolume() * 10f; // Scale from 0-1 to 0-10
        }

        // Add listeners to handle slider value changes
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (MusicManager.Instance != null)
        {
            float normalizedValue = value / 10f; // Normalize slider value from 0-10 to 0-1
            MusicManager.Instance.SetVolume(normalizedValue);
            PlayerPrefs.SetFloat("MusicVolume", normalizedValue);
        }
    }

    private void OnSFXVolumeChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            float normalizedValue = value / 10f; // Normalize slider value from 0-10 to 0-1
            SoundManager.Instance.SetVolume(normalizedValue);
            PlayerPrefs.SetFloat("SFXVolume", normalizedValue);
        }
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
