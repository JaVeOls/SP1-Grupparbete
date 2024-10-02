using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private PlayerName _playerNameSave;
    [SerializeField] private SavingApples _savingApples;
    [SerializeField] private Crystals _crystals;

    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_InputField playerName;

    private void Start()
    {
        _savingApples.collectedApples = 0;
        _crystals.hasRedCrystal = false;
        _crystals.hasPurpleCrystal = false;
        _playerNameSave.playerName = "";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void SaveName()
    {
        _playerNameSave.playerName = playerName.text;

        if (_playerNameSave.playerName != "")
        {
            StartGame();
        }

    }
}
