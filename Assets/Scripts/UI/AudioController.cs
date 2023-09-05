using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer myAudioMixer;
    [Range(-80, 20)]
    [SerializeField] public static float allVolume = 0f;
    [Range(-80, 20)]
    [SerializeField] public static float musicVolume = 0.3f;
    [Range(-80, 20)]
    [SerializeField] public static float pointsVolume = 0.4f;
    private void Awake()
    {
        myAudioMixer.SetFloat("AllSounds", allVolume);
        myAudioMixer.SetFloat("musicVolume", musicVolume);
        myAudioMixer.SetFloat("pointsVolume", pointsVolume);
        float val1, val2, val0;
        myAudioMixer.GetFloat("AllSounds", out val0);
        myAudioMixer.GetFloat("musicVolume", out val1);
        myAudioMixer.GetFloat("pointsVolume", out val2);
    }
}
