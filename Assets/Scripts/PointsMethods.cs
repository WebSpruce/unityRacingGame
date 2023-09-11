using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMethods : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && PlayerMovement.instance.isStarted)
        {
            int firstEmptyIndex = Array.IndexOf(PlayerMovement.instance.hasPoint, false);
            gameObject.SetActive(false);
            if (firstEmptyIndex != -1)
            {
                PlayerMovement.instance.hasPoint[firstEmptyIndex] = true;
            }
            
            PlayerMovement.instance.audioSourcePoint.Play();
        }
    }
}
