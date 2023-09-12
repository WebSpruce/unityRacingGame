using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMethods : MonoBehaviour
{
    [Header("Movement of the platform")]
    [SerializeField] float movementSpeed = 3.5f;
    [SerializeField] float movementAplitude = 5;
    private Vector3 startPosition = new Vector3();
    private Vector3 movePosition = new Vector3();
    [SerializeField] private GameObject player;
    void Start()
    {
        
        Debug.Log($"player: {player.name}");
        startPosition = gameObject.transform.position;
        Debug.Log($"start position: {startPosition}");
    }
    void Update()
    {
        movePosition.y = startPosition.y + Mathf.Sin(Time.time * movementSpeed) * movementAplitude;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, movePosition.y, gameObject.transform.position.z);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.parent = null;
        }
    }
}
