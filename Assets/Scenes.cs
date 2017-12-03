using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes
{
  public static string message = "Welcome to 20,000 Leaks";

  public static void GameOver(string s)
  {
    message = s;
    SceneManager.LoadScene(0);
  }

  public static void StartGame()
  {
    SceneManager.LoadScene(1);
  }
}
