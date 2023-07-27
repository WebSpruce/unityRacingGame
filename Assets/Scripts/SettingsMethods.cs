using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMethods : MonoBehaviour
{
    [Header("Titles")]
    private Slider[] sliders;
    [SerializeField] private TextMeshProUGUI musicTitle;
    [SerializeField] private TextMeshProUGUI pointsTitle;
    private void Awake()
    {

        sliders = GameObject.FindObjectsOfType<Slider>();
        if (PlayerPrefs.HasKey("MusicVolumeValue"))
        {
            sliders[0].value = PlayerPrefs.GetFloat("MusicVolumeValue");
            musicTitle.text = $"Music Volume: {sliders[0].value.ToString()}";
        }else if (PlayerPrefs.HasKey("PointsVolumeValue"))
        {
            sliders[1].value = PlayerPrefs.GetFloat("PointsVolumeValue");
            pointsTitle.text = $"Points Volume: {sliders[1].value.ToString()}";
        }
    }
    void Start()
    {
        Debug.Log($"sliders: {PlayerPrefs.GetFloat("MusicVolumeValue")} {PlayerPrefs.GetFloat("PointsVolumeValue")}");
    }
    public void musicVolumeChanged()
    {
        PlayerPrefs.SetFloat("MusicVolumeValue", sliders[0].value);
        musicTitle.text = $"{sliders[0].value}";
    }
    public void pointVolumeChanged()
    {
        PlayerPrefs.SetFloat("PointsVolumeValue", sliders[1].value);
        pointsTitle.text = $"{sliders[1].value}";
    }
    public void Save()
    {
        SceneManager.LoadScene(1);
    }
}
