using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "crystals", menuName = "ScriptableObjects/crystals")]
public class Crystals : ScriptableObject
{
    public bool hasRedCrystal;
    public bool hasPurpleCrystal;

    public Sprite redCrystal;
    public Sprite purpleCrystal;
}
