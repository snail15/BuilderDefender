using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

   public event EventHandler OnHQDestroyed;
   
   private HealthSystem _healthSystem;
   private BuildingTypeSO _buildingType;
   private Transform _buildingDemolishBtn;
   private Transform _buildingRepairBtn;

   private void Awake()
   {
      _buildingDemolishBtn = transform.Find("pfBuildingDemolishButton");
      _buildingRepairBtn = transform.Find("pfBuildingRepairButton");
      HideDemolishBtn();
      HideRepairBtn();
   }

   private void Start()
   {
      _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
      _healthSystem = GetComponent<HealthSystem>();
      _healthSystem.SetHealthAmountMax(_buildingType.HealthAmountMax, true);
      _healthSystem.OnDied += OnDied;
      _healthSystem.OnDamaged += OnDamaged;
      _healthSystem.OnHealed += OnHealed;
     

   }
   private void OnHealed(object sender, EventArgs e)
   {
      if (_healthSystem.IsFullHealth())
      {
         HideRepairBtn();
      }
   }
   private void OnDamaged(object sender, EventArgs e)
   {
      ShowRepairhBtn();
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
      // if (_buildingType.nameString.Equals("HQ"))
      // {
      //    OnHQDestroyed?.Invoke(this,EventArgs.Empty);
      // }
      Destroy(gameObject);
   }

   private void OnMouseEnter()
   {
     ShowDemolishBtn();
   }

   private void OnMouseExit()
   {
      HideDemolishBtn();
   }

   private void ShowDemolishBtn()
   {
      if (_buildingDemolishBtn != null)
      {
         _buildingDemolishBtn.gameObject.SetActive(true);
      }
   }

   private void HideDemolishBtn()
   {
      if (_buildingDemolishBtn != null)
      {
         _buildingDemolishBtn.gameObject.SetActive(false);
      }
   }
   private void ShowRepairhBtn()
   {
      if (_buildingRepairBtn != null)
      {
         _buildingRepairBtn.gameObject.SetActive(true);
      }
   }

   private void HideRepairBtn()
   {
      if (_buildingRepairBtn != null)
      {
         _buildingRepairBtn.gameObject.SetActive(false);
      }
   }
}
