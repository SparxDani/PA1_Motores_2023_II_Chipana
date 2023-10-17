using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_Mixer", menuName = "ScriptableObjects/Audio/SO_MixerChannel")]
public class SOaudio : ScriptableObject
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private string channelVolume;
    [SerializeField] private float currentVolume;

    public void UpdateVolume(Slider slider)
    {
        currentVolume = slider.value;
        mixer.SetFloat(channelVolume, Mathf.Log10(currentVolume) * 20f);
    }
    public void MuteChannel()
    {
        mixer.SetFloat(channelVolume, -80);
    }
    
}
