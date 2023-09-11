using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Methods : MonoBehaviour
{
    public GameObject[] points;
    public static Level3Methods instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
        points[0].SetActive(false);
        points[1].SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerMovement.instance.isStarted && !points[0].activeSelf)
            {
                points[0].SetActive(true);
                points[1].SetActive(true);
            }
        }
    }
}
