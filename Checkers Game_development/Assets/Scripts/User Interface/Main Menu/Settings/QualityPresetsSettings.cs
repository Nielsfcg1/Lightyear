using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality
{
	high,
	medium,
	low,
}

public class QualityPresetsSettings : MonoBehaviour
{
	public Quality quality;

	public void OnEnable ()
	{
		switch (quality)
		{
			case Quality.high:
			QualitySettings.SetQualityLevel (5);
			break;

			case Quality.medium:
			QualitySettings.SetQualityLevel (3);
			break;

			case Quality.low:
			QualitySettings.SetQualityLevel (1);
			break;
		}
	}
}
