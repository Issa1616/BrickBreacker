using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f);

        AudioListener.volume = volume;
        volumeSlider.value = volume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }
}