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
   private SpriteRenderer _spriteRenderer;
   private BuildingTypeHolder _buildingTypeHolder;
   private Material _constructionMaterial;
   private static readonly int Progress1 = Shader.PropertyToID("_Progress");

   private void Awake()
   {
      _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
      _boxCollider = GetComponent<BoxCollider2D>();
      _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
      _constructionMaterial = _spriteRenderer.material;
   }

   private void Update()
   {
      _constructionTimer -= Time.deltaTime;
      _constructionMaterial.SetFloat(Progress1, GetConstructionTimeNormalized());
      if (_constructionTimer <= 0f)
      {
         Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
         SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
         Destroy(gameObject);
      }
   }

   private void SetBuildingType(BuildingTypeSO buildingType)
   {
      _constructionTimerMax = buildingType.ConstructionTimerMax;
      _buildingType = buildingType;
      _constructionTimer = _constructionTimerMax;

      _spriteRenderer.sprite = buildingType.sprite;
      _boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
      _boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;

      _buildingTypeHolder.buildingType = buildingType;
   }
   
   public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
   {
      Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
      Transform buildingConstructionTransform =  Instantiate(pfBuildingConstruction, position, Quaternion.identity);

      BuildingConstruction buildingConstruction =  buildingConstructionTransform.GetComponent<BuildingConstruction>();
      buildingConstruction.SetBuildingType(buildingType);

      return buildingConstruction;
   }

   public float GetConstructionTimeNormalized()
   {
      return 1 - _constructionTimer / _constructionTimerMax;
   }
}
