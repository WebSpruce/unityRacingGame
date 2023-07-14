using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryMethods : MonoBehaviour
{
    [SerializeField] private GameObject historyObject;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Back()
    {
        historyObject.SetActive(false);
    }
}
