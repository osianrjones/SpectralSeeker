using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource[] audioSources;
    private const string VolumeKey = "GameVolume";

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, 0.75f);

        SetVolume(volumeSlider.value);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }

        AudioListener.volume = volume;

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}
