using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
   private float _constructionTimer;
   private float _constructionTimerMax;
   private BuildingTypeSO _buildingType;
   private BoxCollider2D _boxCollider;


   private void Awake()
   {
      _boxCollider = GetComponent<BoxCollider2D>();
   }

   private void Update()
   {
      _constructionTimer -= Time.deltaTime;
      if (_constructionTimer <= 0f)
      {
         Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
         Destroy(gameObject);
      }
   }

   private void SetBuildingType(BuildingTypeSO buildingType)
   {
      _constructionTimerMax = buildingType.ConstructionTimerMax;
      _buildingType = buildingType;
      _constructionTimer = _constructionTimerMax;

      _boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
      _boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
   }
   
   public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
   {
      Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
      Transform buildingConstructionTransform =  Instantiate(pfBuildingConstruction, position, Quaternion.identity);

      BuildingConstruction buildingConstruction =  buildingConstructionTransform.GetComponent<BuildingConstruction>();
      buildingConstruction.SetBuildingType(buildingType);

      return buildingConstruction;
   }
}
