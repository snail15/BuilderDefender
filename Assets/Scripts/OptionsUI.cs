using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
   [SerializeField] private SoundManager soundManager;
   private TextMeshProUGUI _soundVolumeText;
   private void Awake()
   {
      _soundVolumeText = transform.Find("CurrentSoundVolumeText").GetComponent<TextMeshProUGUI>();
      transform.Find("SoundIncreaseButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         soundManager.IncreaseVolume();
         UpdateText();
      }));
      transform.Find("SoundDecreaseButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         soundManager.DecreaseVolume();
         UpdateText();
      }));
      transform.Find("MusicIncreaseButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         
      }));
      transform.Find("MusicDecreaseButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         
      })); 
      transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         
      }));
   }

   private void Start()
   {
      UpdateText();
   }

   private void UpdateText()
   {
      _soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
   }
}
