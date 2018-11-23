using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Textures
{
	high,
	medium,
	low
}

public class TexturesSettings : MonoBehaviour
{
	public Textures textures;

	public void OnEnable ()
	{
		switch (textures)
		{
			case Textures.high:
			QualitySettings.masterTextureLimit = 0;
			break;

			case Textures.medium:
			QualitySettings.masterTextureLimit = 1;
			break;

			case Textures.low:
			QualitySettings.masterTextureLimit = 2;
			break;
		}
	}
}
