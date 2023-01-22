using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
   private HealthSystem _healthSystem;
   private BuildingTypeSO _buildingType;
   private void Start()
   {
      _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
      _healthSystem = GetComponent<HealthSystem>();
      _healthSystem.SetHealthAmountMax(_buildingType.HealthAmountMax, true);
      _healthSystem.OnDied += OnDied;
   }

   private void Update()
   {
      // if (Input.GetKeyDown(KeyCode.T))
      // {
      //    _healthSystem.Damage(10);
      // }
   }

   private void OnDied(object sender, EventArgs e)
   {
      Destroy(gameObject);
   }
}
