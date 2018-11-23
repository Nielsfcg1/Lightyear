using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenMode
{
	fullscreen,
	windowed,
}

public class ScreenSettings : MonoBehaviour
{
	public ScreenMode screen;

	public void OnEnable ()
	{
		switch (screen)
		{
			case ScreenMode.fullscreen:
			Screen.fullScreen = true;
			break;

			case ScreenMode.windowed:
			Screen.fullScreen = false;
			break;
		}
	}
}
