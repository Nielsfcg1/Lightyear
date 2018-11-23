using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resolution
{
	fullHD,
	hDReady,
	ED,
	LD
}

public class ResolutionSettings : MonoBehaviour
{
	public Resolution resolution;
	bool fullScreen = false;

	public void OnEnable ()
	{
		if (Screen.fullScreen == true)
			fullScreen = true;
		else
			fullScreen = false;
			
		switch (resolution)
		{
			case Resolution.fullHD:
			Screen.SetResolution (1920, 1080, fullScreen);
			break;

			case Resolution.hDReady:
			Screen.SetResolution (1280, 720, fullScreen);
			break;

			case Resolution.ED:
			Screen.SetResolution (640, 480, fullScreen);
			break;

			case Resolution.LD:
			Screen.SetResolution (480, 360, fullScreen);
			break;
		}
	}
}
