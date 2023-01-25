using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField] private Building building;
    private Button _button;
    
    private void Awake()
    {
        _button = transform.Find("Button").GetComponent<Button>();
        _button.onClick.AddListener((() =>
        {
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount amount in buildingType.ConstructionResourceCostArray)
            {
                ResourceManager.Instance.AddResource(amount.ResourceType, Mathf.FloorToInt(amount.Amount * 0.6f));
            }
            Destroy(building.gameObject);
        }));
        
    }
}
