using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AntiAliasing
{
	eightX,
	fourX,
	twoX,
	disabled
}

public class AntiAliasingSettings : MonoBehaviour
{
	public AntiAliasing antiAliasing;

	public void OnEnable ()
	{
		switch (antiAliasing)
		{
			case AntiAliasing.eightX:
			QualitySettings.antiAliasing = 8;
			break;

			case AntiAliasing.fourX:
			QualitySettings.antiAliasing = 4;
			break;

			case AntiAliasing.twoX:
			QualitySettings.antiAliasing = 2;
			break;

			case AntiAliasing.disabled:
			QualitySettings.antiAliasing = 0;
			break;
		}
	}
}
