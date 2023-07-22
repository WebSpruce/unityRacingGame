using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryMethods : MonoBehaviour
{
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
        result.text = PlayerMovement.instance.timer.ToString();
    }
}
