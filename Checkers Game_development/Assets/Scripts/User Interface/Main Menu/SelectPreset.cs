using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPreset : MonoBehaviour
{
	public List<GameObject> presets = new List<GameObject> (); 

	int currentSetting;
	int setting;

	public void ChangeSetting (int direction)
	{
		GetCurrentSetting ();

		if (currentSetting == 0 && direction == -1)
			setting = presets.Count-1;
		else if (currentSetting == presets.Count-1 && direction == 1)
			setting = 0;
		else
			setting = (currentSetting + direction);

		presets [currentSetting].SetActive (false);
		presets [setting].SetActive (true);
	}

	void GetCurrentSetting ()
	{
		for (int i = 0; i < presets.Count; i++)
		{
			if (presets [i].activeInHierarchy)
			{
				currentSetting = i;
			}
		}
	}
}
