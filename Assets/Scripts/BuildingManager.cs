using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }


    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }


    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;
    [SerializeField] private Building hqBuilding;
    private void Awake() {
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        
    }

    private void Start() {
        mainCamera = Camera.main;
        var healthSystem = hqBuilding.GetComponent<HealthSystem>();
        healthSystem.OnDied += OnHQDied;

    }

    private void OnHQDied(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) { //Make sure it's not over other building
            if (activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.ConstructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.ConstructionResourceCostArray);
                        //Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("Can't afford the building", new TooltipUI.TooltipTimer {Timer = 2f});
                    }
                   
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer {Timer = 2f} );
                }

            }
        }
        
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, 
            new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType }
        );
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "Area not clear!";
            return false;
        }


        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                // Has a BuildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingType) {
                    // Theres already a building of this type within the construction radius!
                    errorMessage = "Too close to another building of same type";
                    return false;
                }
            }
        }

        float maxConstructionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray) {
            // Colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                // Its a building!
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Building too far away!";
        return false;
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }


}
