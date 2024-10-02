using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivePortal : MonoBehaviour
{
    [SerializeField] private Crystals _crystals;

    private void Update()
    {
        if (_crystals.purpleCrystal == true)
        {
            gameObject.SetActive(false);
        }
    }
    
}
