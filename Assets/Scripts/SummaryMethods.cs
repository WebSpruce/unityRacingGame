using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryMethods : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject summary;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject historyObject;
    [SerializeField] private TextMeshProUGUI result;

    public static SummaryMethods instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }else if (instance != null)
        {
            Destroy(this);
        }

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        result.text = PlayerMovement.instance.timer.ToString();
    }
    public void Quit()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public void ResetLevel()
    {
        Time.timeScale = 0;
        player.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1, spawnPoint.transform.position.z - 1f);

        for (int i = 0; i < PlayerMovement.instance.allPoints.Length; i++) { PlayerMovement.instance.allPoints[i].SetActive(true); PlayerMovement.instance.hasPoint[i] = false; }

        PlayerMovement.instance.isStarted = false;
        PlayerMovement.instance.isOnTrack = false;
        PlayerMovement.instance.timer = 0;

        summary.SetActive(false);
        Time.timeScale = 1;
    }
    public void History()
    {
        historyObject.SetActive(true);
    }
}
