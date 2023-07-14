using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryMethods : MonoBehaviour
{
    [SerializeField] private GameObject historyObject;
    List<ResultValues> results = new List<ResultValues>();
    void Start()
    {
        Debug.Log(PlayerMovement.instance.filename);
        results = FileHandler.ReadFromJSON<ResultValues>(PlayerMovement.instance.filename);
        foreach(var item in results)
        {
            Debug.Log($"Your result: {item.result} - {item.date}");
        }
    }
    void Update()
    {
        
    }
    public void Back()
    {
        historyObject.SetActive(false);
    }
}
