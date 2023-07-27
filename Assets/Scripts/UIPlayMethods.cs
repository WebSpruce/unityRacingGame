using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayMethods : MonoBehaviour
{
    [Header("Summary Window")]
    [SerializeField] private GameObject summary;
    [SerializeField] private GameObject historyObject;
    [SerializeField] private TextMeshProUGUI result;


    [Header("Pause Window")]
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject settingsObject;

    [Header("For all")]
    [SerializeField] private GameObject spawnPoint;

    private Transform player;

    public static UIPlayMethods instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    public void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Settings()
    {
        //settingsObject.SetActive(true);
        SceneManager.LoadScene(2);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
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

        if (summary.activeSelf)
        {
            summary.SetActive(false);
        }
        else if(pauseObject.activeSelf)
        {
            pauseObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void History()
    {
        historyObject.SetActive(true);
    }
}
