using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
   private void Awake()
   {
      transform.Find("PlayButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         GameSceneManager.Load(GameSceneManager.Scene.GameScene);
      }));
      transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener((() =>
      {
         Application.Quit();
      }));
   }
   
}
