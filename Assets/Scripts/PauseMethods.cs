using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMethods : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject settingsObject;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    public void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
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
        player.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y+1, spawnPoint.transform.position.z);
        pauseObject.SetActive(false);
        PlayerMovement.instance.isStarted = false;
        PlayerMovement.instance.isOnTrack = false;
        PlayerMovement.instance.timer = 0;
        Time.timeScale = 1;
    }
    public void Settings()
    {
        settingsObject.SetActive(true);
    }
}
