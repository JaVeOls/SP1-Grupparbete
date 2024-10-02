using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    [SerializeField] private PlayerName _playerNameSave;

    [SerializeField] private TMP_Text questGiverText;

    void Start()
    {
        questGiverText.text = _playerNameSave.playerName + ", Collecteth 10 apples f'r me and deposit them by the trophy";
    }
}
