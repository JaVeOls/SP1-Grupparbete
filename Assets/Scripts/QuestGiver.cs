using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textPopUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                GetComponent<EndDialogue>().EndDialogueText();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(false);
    
        }


    }


}
