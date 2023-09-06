using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineSkinRotate : MonoBehaviour
{
    void Start()
    {
        CinemachineCore.GetInputAxis = GetAxis;
    }

    private float GetAxis(string axisName)
    {
        if(axisName == "Mouse X")
        {
            if (Input.GetMouseButton(0))
            {
                return Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        }else if(axisName == "Mouse Y")
        {
            if (Input.GetMouseButton(0))
            {
                return Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }
        return Input.GetAxis(axisName);
    }

    
}
