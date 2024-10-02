using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDialogue : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;

    [SerializeField] private Transform textPopUpT;

    [SerializeField] private GameObject crystal0Text;
    [SerializeField] private GameObject crystal1Text;
    [SerializeField] private GameObject crystal2Text;

    public void EndDialogueText()
    {
        if (skeleton.GetComponent<Skeleton>().crystalCount == 0)
        {
            crystal0Text.transform.SetParent(textPopUpT);
        }

        if (skeleton.GetComponent<Skeleton>().crystalCount == 1)
        {
            crystal1Text.transform.SetParent(textPopUpT);
        }

        if (skeleton.GetComponent<Skeleton>().crystalCount == 2)
        {
            crystal2Text.transform.SetParent(textPopUpT);
        }
    }
}
