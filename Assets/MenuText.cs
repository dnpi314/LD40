using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuText : MonoBehaviour 
{
  Text text;

	void Start () 
	{
    text = GetComponent<Text>();
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
	}
	
	void Update () 
	{
    text.text = Scenes.message;
	}

  public void StartGame()
  {
    Scenes.StartGame();
  }
}
