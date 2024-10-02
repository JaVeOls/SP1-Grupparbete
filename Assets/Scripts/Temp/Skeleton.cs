using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Crystals _crystals;

    [SerializeField] private GameObject textPopUp;
    //[SerializeField] private Transform textPopUpT;

    [SerializeField] private GameObject redCrystal;
    [SerializeField] private GameObject purpleCrystal;
    //[SerializeField] private GameObject crystal1Text;

    public Image redCrystalImage;
    public Image purpleCrystalImage;

    public bool hasCrystal = false;

    public int crystalCount;

    private void Start()
    {
        redCrystalImage.sprite = _crystals.redCrystal;
        purpleCrystalImage.sprite = _crystals.purpleCrystal;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && hasCrystal == true)
        {
            _crystals.hasRedCrystal = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 && hasCrystal == true)
        {
            _crystals.hasPurpleCrystal = true;
        }

        if (_crystals.hasRedCrystal == true)
        {
            redCrystal.SetActive(true);
        }

        if (_crystals.hasPurpleCrystal == true)
        {
            purpleCrystal.SetActive(true);
        }

        if (_crystals.hasRedCrystal == false && _crystals.hasPurpleCrystal == false)
        {
            crystalCount = 0;
        }
        
        if((_crystals.hasRedCrystal == true && _crystals.hasPurpleCrystal == false) || 
            (_crystals.hasPurpleCrystal == true && _crystals.hasRedCrystal == false))
        {
            crystalCount = 1; 
        }

        if (_crystals.hasRedCrystal == true && _crystals.hasPurpleCrystal == true)
        {
            crystalCount = 2;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopUp.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                GetComponent<EndDialogue>().EndDialogueText();
            }
            if (!hasCrystal)
            {
                hasCrystal = true;
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
