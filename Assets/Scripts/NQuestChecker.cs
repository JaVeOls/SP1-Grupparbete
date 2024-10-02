using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NQuestChecker : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 1;
    [SerializeField] private int levelToLoad;

    private Animator anim;
    private bool levelIsLoadiing = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<NPlayerMovement>().cherriesCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke("LoadNextLevel", 3.0f);
                levelIsLoadiing = true;
            }
            else
            {
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !levelIsLoadiing)
        {
            if (dialogueBox != null)
            dialogueBox.SetActive(false);

            if (finishedText != null)
            finishedText.SetActive(false);

            if (unfinishedText != null)
            unfinishedText.SetActive(false);
        }
    }

}
//     //private void OnTriggerExit2D(Collider2D other)
//     {
//          //if(other.CompareTag("Player") && !levelIsLoadiing)
//         {
//             //dialogueBox.SetActive(false);
//             //finishedText.SetActive(false);
//             //unfinishedText.SetActive(false);
//         }
//     }
// }
