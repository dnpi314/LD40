using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathTimer : MonoBehaviour 
{
  float timer = 30;
  Text text;
	void Start () 
	{
    text = GetComponent<Text>();
	}
	
	void Update () 
	{
    if (PlayerControls.underwater)
    {
      timer -= Time.deltaTime;
    }
    else
    {
      timer = 30;
    }

    text.text = string.Format("Breath: {0}", (int)timer);
	}
}
