using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource[] audioSources; // For multiple audio sources
    private const string VolumeKey = "GameVolume"; // PlayerPrefs key

    void Start()
    {
        // Load saved volume or default to 0.75 if not set
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, 0.75f);

        // Set initial volume
        SetVolume(volumeSlider.value);

        // Add listener for when slider value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Set volume for all audio sources
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }

        // For global audio (like AudioListener)
        AudioListener.volume = volume;

        // Save the volume setting
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}
