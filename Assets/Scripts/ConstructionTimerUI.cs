using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction;
    private Image _constructionProgressImg;
    private void Awake()
    {
        _constructionProgressImg = transform.Find("Mask").Find("Image").GetComponent<Image>();
    }

    private void Update()
    {
        _constructionProgressImg.fillAmount = buildingConstruction.GetConstructionTimeNormalized();
    }
}
