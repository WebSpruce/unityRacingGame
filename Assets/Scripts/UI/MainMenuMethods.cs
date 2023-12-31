using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMethods : MonoBehaviour
{
    public static MainMenuMethods instance;
    public string previousSceneName = "MainMenuMethods";
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
    private void OnEnable()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3,LoadSceneMode.Single);
    }
    public void Settings()
    {
        previousSceneName = "MainMenuMethods";
        SceneManager.LoadScene(1);
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
}
