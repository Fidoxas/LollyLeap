using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider volumeSlider;
    [SerializeField] AudioMixer masterMixer;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(volumeSlider.value);
    }

    public void RefreshSlider(float value)
    {
        volumeSlider.value = value;
    }
}