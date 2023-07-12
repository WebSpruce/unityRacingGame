using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float timer = 0;

    private Rigidbody playerRB;
    private Vector2 inputVector2Values;
    [SerializeField] private bool isStarted = false;
    [SerializeField] private bool isOnTrack = false;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        inputVector2Values = movement.action.ReadValue<Vector2>();
        
        playerRB.AddForce(new Vector3(inputVector2Values.x, 0, inputVector2Values.y) * MovementSpeed, ForceMode.Impulse);

        //rotation to direction of movement
        Vector3 movement3 = new Vector3(inputVector2Values.x, 0 ,inputVector2Values.y).normalized;
        if(movement3 != Vector3.zero)
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
        
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.interaction is TapInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce-2), ForceMode.Impulse);
        }
        if (callbackContext.interaction is HoldInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce), ForceMode.Impulse);
        }
        if(callbackContext.interaction is MultiTapInteraction && callbackContext.performed)
        {
            playerRB.AddForce(Vector3.up * (jumpForce), ForceMode.Impulse);
        }
    }
    //private void OnTriggerEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("TrackStart") && !isStarted)
    //    {
    //        Debug.Log("START TIME");
    //        isStarted = true;
    //    }
    //    if (other.gameObject.CompareTag("Track") && !isStarted)
    //    {
    //        Debug.Log("NOPE");
    //    }
    //    if (other.gameObject.CompareTag("Track") && isStarted && !isOnTrack)
    //    {
    //        isOnTrack = true;
    //        Debug.Log("changed floor");
    //    }
    //    if (other.gameObject.CompareTag("TrackStop") && isStarted && isOnTrack)
    //    {
    //        Debug.Log($"STOP - {timer}");
    //        isStarted = false;
    //        isOnTrack = false;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TrackStart") && !isStarted)
        {
            Debug.Log("START TIME");
            isStarted = true;
        }
        if (other.gameObject.CompareTag("Track") && !isStarted)
        {
            Debug.Log("NOPE");
        }
        if (other.gameObject.CompareTag("Track") && isStarted && !isOnTrack)
        {
            isOnTrack = true;
            Debug.Log("changed floor");
        }
        if (other.gameObject.CompareTag("TrackStop") && isStarted && isOnTrack)
        {
            Debug.Log($"STOP - {timer}");
            isStarted = false;
            isOnTrack = false;
        }
        if (other.gameObject.CompareTag("Ground") )
        {
            isStarted = false;
            isOnTrack = false;
            Debug.Log("changed something");
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Track") && isStarted)
    //    {
    //        Debug.Log("STOP TIME");
    //        isStarted = false;
    //        isOnTrack = false;
    //    }
    //}
}
