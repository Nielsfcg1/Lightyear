using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VSync
{
	on,
	off
}

public class VSyncSettings : MonoBehaviour
{
	public VSync vSync;

	public void OnEnable ()
	{
		switch (vSync)
		{
			case VSync.on:
			QualitySettings.vSyncCount = 1;
			break;

			case VSync.off:
			QualitySettings.vSyncCount = 0;
			break;
		}
	}
}
