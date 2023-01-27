using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;
    private Button _button;
    
    private void Awake()
    {
        
        transform.Find("Button").GetComponent<Button>().onClick.AddListener((() =>
        {
            int damagedAmount = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = damagedAmount / 2;

            ResourceAmount[] resourceAmounts = new ResourceAmount[]
                {new ResourceAmount {ResourceType = goldResourceType, Amount = repairCost}};
            
            if (ResourceManager.Instance.CanAfford(resourceAmounts))
            {
                ResourceManager.Instance.SpendResources(resourceAmounts);
                healthSystem.HealFull();
            }
            else
            {
                TooltipUI.Instance.Show("Can't afford repair cost!", new TooltipUI.TooltipTimer {Timer =  1.5f});
            }
            
        }));
        
    }
}
