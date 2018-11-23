﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_PickUp : MonoBehaviour
{
	AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlayPickUpSound ()
	{
		audioSource.Play ();
	}
}