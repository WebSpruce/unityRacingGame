using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMethods : MonoBehaviour
{
    [Header("Titles")]
    private Slider[] sliders;
    [SerializeField] private TextMeshProUGUI allSoundsTitle;
    [SerializeField] private TextMeshProUGUI musicTitle;
    [SerializeField] private TextMeshProUGUI pointsTitle;
    [SerializeField] private AudioMixer myAudioMixer;
    private float value, value2, value0;
    void Start()
    {
        sliders = GameObject.FindObjectsOfType<Slider>();
        
        myAudioMixer.GetFloat("AllSounds", out value0);
        myAudioMixer.GetFloat("musicVolume", out value);
        myAudioMixer.GetFloat("pointsVolume", out value2);
        Debug.Log(value + " and " + value2 + " and " + value0);

        sliders[0].value = value2;
        sliders[1].value = value;
        sliders[2].value = value0;
        allSoundsTitle.text = $"All Sounds Volume:{(value0).ToString("#")}%";
        musicTitle.text = $"Music Volume:{(value).ToString("#")}%";
        pointsTitle.text = $"Points Volume:{(value2).ToString("#")}%";
    }
    public void allSoundsVolumeChanged(Slider slider)
    {
        AudioController.allVolume = slider.value;
        allSoundsTitle.text = $"All Sounds Volume: {(slider.value).ToString("#")}%";
    }
    public void musicVolumeChanged(Slider slider)
    {
        AudioController.musicVolume = slider.value;
        musicTitle.text = $"Music Volume: {(slider.value).ToString("#")}%";
    }
    public void pointVolumeChanged(Slider slider)
    {
        AudioController.pointsVolume = slider.value;
        pointsTitle.text = $"Points Volume: {(slider.value).ToString("#")}%";
    }
    public void Save()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
