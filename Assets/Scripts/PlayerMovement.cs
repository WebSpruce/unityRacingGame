using System;
using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody playerRB;
    private Vector2 inputVector2Values;

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
}
