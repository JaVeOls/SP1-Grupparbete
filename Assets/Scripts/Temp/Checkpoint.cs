using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject dialogueBox, checkboxText;

    private Animator anim;
    private Collider2D coll;

    public Transform respawnPoint;

    [SerializeField] private bool checkpoint;

    private void Start()
    {
        if (checkpoint)
        {
            anim = GetComponent<Animator>();
        }
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement.UpdateCheckpoint(respawnPoint.position);
            if (checkpoint)
            {
                anim.SetTrigger("Flag");
                dialogueBox.SetActive(true);
                checkboxText.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (checkpoint)
        {
            dialogueBox.SetActive(false);
            checkboxText.SetActive(false);
        }
        coll.enabled = false;
    }
}
