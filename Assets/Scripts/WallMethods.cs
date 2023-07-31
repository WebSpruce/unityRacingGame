using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMethods : MonoBehaviour
{
    [Header("Movement of the wall")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float movementAplitude = 5;
    private Vector3 startPosition;
    private Vector3 movePosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        movePosition.x = startPosition.x + Mathf.Sin(Time.time * movementSpeed) * movementAplitude;
        transform.position = new Vector3(movePosition.x, transform.position.y, transform.position.z);
    }
}
