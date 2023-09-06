using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;
    [SerializeField] private CinemachineVirtualCamera cam3;
    void Start()
    {
        cam1.Priority = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cam1.Priority = 3;
            cam2.Priority = 2; 
            cam3.Priority = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam1.Priority = 2;
            cam2.Priority = 3;
            cam3.Priority = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cam1.Priority = 2;
            cam2.Priority = 2;
            cam3.Priority = 3;
        }

    }
}
