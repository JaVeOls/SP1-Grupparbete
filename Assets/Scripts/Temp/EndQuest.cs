using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndQuest : MonoBehaviour
{
    [SerializeField] private Crystals _crystals;

    [SerializeField] private GameObject skeleton;
    [SerializeField] private GameObject questGiver;

    void Update()
    {
        if (skeleton.GetComponent<Skeleton>().crystalCount >= 1)
        {
            skeleton.SetActive(true);
        }
        else
        {
            skeleton.SetActive(false);
        }


    }
}
