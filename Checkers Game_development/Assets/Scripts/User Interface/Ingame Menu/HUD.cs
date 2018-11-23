using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
	public static HUD instance;

	void Awake ()
	{
		instance = this;
	}

	public void ToggleHUD (bool state, GameObject exception)
	{
		for (int i = 0; i < transform.childCount; i++) 
		{
			if (transform.GetChild (i).gameObject != exception)
				transform.GetChild (i).gameObject.SetActive (state);
		}
	}
}
