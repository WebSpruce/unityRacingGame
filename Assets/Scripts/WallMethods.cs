using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMethods : MonoBehaviour
{
    [Header("Movement of the wall")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float movementAplitude = 5;
    private Vector3[] startPosition = new Vector3[2];
    private Vector3[] movePosition = new Vector3[2];
    private GameObject[] gameObjects;
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Wall");
        startPosition[0] = gameObjects[0].transform.position;
        startPosition[1] = gameObjects[1].transform.position;
    }
    void Update()
    {
        movePosition[0].x = startPosition[0].x + Mathf.Sin(Time.time * movementSpeed) * movementAplitude;
        gameObjects[0].transform.position = new Vector3(movePosition[0].x, gameObjects[0].transform.position.y, gameObjects[0].transform.position.z);

        movePosition[1].z = startPosition[1].z + Mathf.Sin(Time.time * movementSpeed) * movementAplitude;
        gameObjects[1].transform.position = new Vector3(gameObjects[1].transform.position.x, gameObjects[1].transform.position.y, movePosition[1].z);
    }
}
