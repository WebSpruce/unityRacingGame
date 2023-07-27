using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicAudioSource;
    public AudioSource pointsAudioSource;
    private void Awake()
    {
        Debug.Log($"volumes: {musicAudioSource.volume} and v: {pointsAudioSource.volume}");
        if (PlayerPrefs.HasKey("MusicVolumeValue"))
        {
            musicAudioSource.volume = PlayerPrefs.GetFloat("musicAudioSource");
        } 
        else 
        {
            PlayerPrefs.SetFloat("MusicVolumeValue", musicAudioSource.volume);
        }
        if (PlayerPrefs.HasKey("PointsVolumeValue"))
        {
            pointsAudioSource.volume = PlayerPrefs.GetFloat("PointsVolumeValue");
        }
        else
        {
            PlayerPrefs.SetFloat("PointsVolumeValue", pointsAudioSource.volume);
        }
    }
}
