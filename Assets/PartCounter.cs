using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartCounter : MonoBehaviour 
{
  Text text;

	void Start () 
	{
    text = GetComponent<Text>();
	}
	
	void Update () 
	{
    text.text = string.Format("Parts Collected: {0}/5", PlayerControls.engineParts);
    if (PlayerControls.engineParts >= 5)
    {
      text.text = "Repair Engine!";
    }
	}
}
