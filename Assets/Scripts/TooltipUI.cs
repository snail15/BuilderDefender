using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
   
   public static TooltipUI Instance { get; private set; }
   
   [SerializeField] private RectTransform canvasRectTransform;
   private TextMeshProUGUI _textMeshPro;
   private RectTransform _backgroundRectTransform;
   private RectTransform _rectTransform;
   private TooltipTimer _tooltipTimer;
   private void Awake()
   {
      Instance = this;
      _textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
      _backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
      _rectTransform = GetComponent<RectTransform>();
      Hide();
   }

   private void Update()
   {
      HandleFollowMouse();
      
      if (_tooltipTimer != null)
      {
         _tooltipTimer.Timer -= Time.deltaTime;
         if (_tooltipTimer.Timer <= 0)
         {
            Hide();
         }
      }
      
   }

   private void HandleFollowMouse()
   {
      Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

      if (anchoredPosition.x + _backgroundRectTransform.rect.width > canvasRectTransform.rect.width) //right
      {
         anchoredPosition.x = canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
      }

      if (anchoredPosition.x - _backgroundRectTransform.rect.width <= 0) // left
      {
         anchoredPosition.x = 0;
      }
      if (anchoredPosition.y + _backgroundRectTransform.rect.height > canvasRectTransform.rect.height) //top
      {
         anchoredPosition.y = canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;
      }

      if (anchoredPosition.y - _backgroundRectTransform.rect.height <= 0)
      {
         anchoredPosition.y = 0;
      }
      
      _rectTransform.anchoredPosition = anchoredPosition;
   }

   private void SetText(string tooltipText)
   {
      _textMeshPro.SetText(tooltipText);
      _textMeshPro.ForceMeshUpdate();

      Vector2 textSize = _textMeshPro.GetRenderedValues(false);
      Vector2 padding = new Vector2(8, 8);
      _backgroundRectTransform.sizeDelta = textSize + padding;
   }

   public void Show(string tooltipText, TooltipTimer timer = null)
   {
      _tooltipTimer = timer;
      gameObject.SetActive(true);
      SetText(tooltipText);
      HandleFollowMouse();
   }

   public void Hide()
   {
      gameObject.SetActive(false);
   }

   public class TooltipTimer
   {
      public float Timer;
   }

}
