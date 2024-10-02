using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private LevelConnection connection;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private int levelToLoad;

    private void Start()
    {
        if (connection == LevelConnection.ActiveConnection)
        {
            FindObjectOfType<PlayerMovement>().transform.position = spawnPoint.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerMovement>();
            LevelConnection.ActiveConnection = connection;
            Invoke("LoadNextLevel", 1f);
            
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
