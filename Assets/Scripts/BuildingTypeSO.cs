using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public bool hasReourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] ConstructionResourceCostArray;
    public int HealthAmountMax;
    public float ConstructionTimerMax;

    public string GetConstructionResourceCostString()
    {
        string str = String.Empty;
        foreach (ResourceAmount amount in ConstructionResourceCostArray)
        {
            str += "<color=#" + amount.ResourceType.ColorHex + ">" + amount.ResourceType.NameShort + ": " + amount.Amount + "</color>" + " ";
        }

        return str;
    }

}
