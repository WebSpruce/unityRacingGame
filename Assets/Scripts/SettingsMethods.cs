using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMethods : MonoBehaviour
{
    [SerializeField] private GameObject settingsObject;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Save()
    {
        settingsObject.SetActive(false);
    }
}
