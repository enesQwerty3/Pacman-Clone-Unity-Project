using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private AudioSource Sound;
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private string KeyName;
    
    void Start()
    {
        if(!PlayerPrefs.HasKey(KeyName))
        {
            PlayerPrefs.SetFloat(KeyName, 0.5f);
            LoadVolume();
        }
        else
            LoadVolume();
            
    }

    public void SetVolume()
    {
        if(Sound != null)
        {
            Sound.volume = VolumeSlider.value;
            saveVolume();
        }
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat(KeyName, VolumeSlider.value);
    }

    private void LoadVolume()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat(KeyName, VolumeSlider.value);
    }
}
