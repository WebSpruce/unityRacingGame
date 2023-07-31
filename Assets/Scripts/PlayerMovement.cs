using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
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
    [SerializeField] public bool isOnTrack = false;
    [SerializeField] private GameObject summary;
    [SerializeField] private GameObject pauseObject;
    [Header("Player Audio")]
    [SerializeField] private AudioSource audioSourcePoint;

    private Rigidbody playerRB;
    private Vector2 inputVector2Values;
    private List<ResultValues> resultsList = new List<ResultValues>();

    public static PlayerMovement instance;
    public string filename = "historyOfResults.json";
    public GameObject[] allPoints;
    public bool[] hasPoint;

    void Awake()
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

        if (isStarted && isOnTrack)
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
        if (other.gameObject.CompareTag("Point") && isStarted && isOnTrack)
        {
            int firstEmptyIndex = Array.IndexOf(hasPoint, false);
            hasPoint[firstEmptyIndex] = true;
            other.gameObject.SetActive(false);
            audioSourcePoint.Play();
        }

        if (other.gameObject.CompareTag("TrackStart") && !isStarted)
        {
            Debug.Log("START TIME");
            isStarted = true;
        }
        if (other.gameObject.CompareTag("additionalTrack") && isStarted)
        {
            isOnTrack = true;
        }
        if (other.gameObject.CompareTag("Track") && isStarted && !isOnTrack)
        {
            isOnTrack = true;
            Debug.Log("changed floor");
        }
        if (other.gameObject.CompareTag("TrackStop") && isStarted && isOnTrack && hasPoint.All(x => x))
        {
            isStarted = false;
            isOnTrack = false;
            Debug.Log($"STOP - {timer}");
            summary.SetActive(true);

            resultsList.Add(new ResultValues(timer.ToString(), DateTime.Now));
            Debug.Log(resultsList[0].result);
            FileHandler.SaveToJSON<ResultValues>(resultsList, filename);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            isStarted = false;
            isOnTrack = false;
            Debug.Log("wall");
        }
    }

}