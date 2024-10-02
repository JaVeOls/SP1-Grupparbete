using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject signYourNameText, dontDoItText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        signYourNameText.SetActive(false);
        dontDoItText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        signYourNameText.SetActive(true);
        dontDoItText.SetActive(false);
    }
}
