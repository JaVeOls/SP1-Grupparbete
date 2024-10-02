using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    //[SerializeField] private Transform spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.transform.position = spawnPosition.position;
            other.transform.position = playerMovement.checkpointPos;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
