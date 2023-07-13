using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummaryMethods : MonoBehaviour
{
    private Transform player;
    [SerializeField] private GameObject summary;
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
    }
    void Start()
    {
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
        player.position = new Vector3(0.34f, 3.282778f, -2.26f);
        summary.SetActive(false);
        PlayerMovement.instance.isStarted = false;
        PlayerMovement.instance.isOnTrack = false;
        PlayerMovement.instance.timer = 0;
    }
    public void History()
    {

    }
}
