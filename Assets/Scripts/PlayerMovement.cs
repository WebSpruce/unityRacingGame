using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Information")]
    [SerializeField] private float MovementSpeed = 0.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private InputActionReference movement;
    [SerializeField] public float timer = 0;
    [Header("Player placement")]
    [SerializeField] public bool isStarted = false;
    [Header("Windows")]
    [SerializeField] private GameObject summary;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject newRecord;
    [Header("Player Audio")]
    [SerializeField] public AudioSource audioSourcePoint;
    [Space(30)]

    private Rigidbody playerRB;
    private Vector2 inputVector2Values;
    private List<ResultValues> resultsList = new List<ResultValues>();


    public static PlayerMovement instance;

    [HideInInspector]
    public MeshFilter playerMF;
    [HideInInspector]
    public string filename;

    public GameObject[] allPoints;
    public bool[] hasPoint;

    void Awake()
    {
        if (PlayerPrefs.GetString("MeshFilter") != null)
        {
            string meshFilterDefault = PlayerPrefs.GetString("MeshFilter", "Meshes/Mesh0");
            playerMF = GetComponent<MeshFilter>();
            playerMF.mesh = (Mesh)Resources.Load(meshFilterDefault, typeof(Mesh));
        }
        else
        {
            PlayerPrefs.SetString($"MeshFilter", "Meshes/Mesh0");
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(this);
        }

        playerRB = GetComponent<Rigidbody>();

        allPoints = GameObject.FindGameObjectsWithTag("Point");
        hasPoint = new bool[allPoints.Length];
    }
    private void Update()
    {
        inputVector2Values = movement.action.ReadValue<Vector2>();

        playerRB.AddForce(new Vector3(inputVector2Values.x, 0, inputVector2Values.y) * MovementSpeed, ForceMode.Impulse);

        //rotation to direction of movement
        Vector3 movement3 = new Vector3(inputVector2Values.x, 0, inputVector2Values.y).normalized;
        if (movement3 != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement3);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
            playerRB.MoveRotation(targetRotation);
        }

        if (isStarted)
        {
            timer += 1 * Time.deltaTime;
            GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>().text = timer.ToString();
        }
        else
        {
            timer = 0;
            GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>().text = "0:0000";
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) && pauseObject.activeSelf)
        {
            pauseObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !pauseObject.activeSelf)
        {
            pauseObject.SetActive(true);
            Time.timeScale = 0;
        }


        //fall checker
        if (transform.position.y < -10)
        {
            UIPlayMethods.instance.ResetLevel();
        }
    }
    private void OnEnable()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        filename = $"historyOfResults{sceneIndex-1}.json";
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.interaction is TapInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce - 2), ForceMode.Impulse);
        }
        if (callbackContext.interaction is HoldInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce), ForceMode.Impulse);
        }
        if (callbackContext.interaction is MultiTapInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce), ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrackStart") && !isStarted)
        {
            Debug.Log("START TIME");
            isStarted = true;
        }
        if (other.gameObject.CompareTag("TrackStop") && isStarted && hasPoint.All(x => x))
        {
            isStarted = false;
            Debug.Log($"STOP - {timer}");
            summary.SetActive(true);

            resultsList.Add(new ResultValues(timer.ToString(), DateTime.Now));

            float min = float.MaxValue;
            List<ResultValues> savedResultValues = FileHandler.ReadFromJSON<ResultValues>(filename);
            foreach(var value in savedResultValues)
            {
                if (min > float.Parse(value.result))
                {
                    min = float.Parse(value.result);
                }
            }

            if (min > timer)
            {
                newRecord.SetActive(true);
            }

            FileHandler.SaveToJSON<ResultValues>(resultsList, filename);
        }
    }

}