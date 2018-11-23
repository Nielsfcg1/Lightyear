using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;

	public AudioSource [] allAudioSources;

	float beginMasterVolume;
	float difference;

	float endMusicVolume = 1;
	float endSFXVolume = 1;

	void Start ()
	{
		FindAllAudioSources ();
	}

	void FindAllAudioSources ()
	{
		allAudioSources = FindObjectsOfType (typeof(AudioSource)) as AudioSource [];
	}

	public void SetMasterVolume ()
	{
		for (int i = 0; i < allAudioSources.Length; i++)
		{
			allAudioSources [i].volume = masterVolumeSlider.value;
		}

		difference = beginMasterVolume / masterVolumeSlider.value;

		musicVolumeSlider.maxValue = masterVolumeSlider.value;
		sfxVolumeSlider.maxValue = masterVolumeSlider.value;

		musicVolumeSlider.value = endMusicVolume / difference; 
		sfxVolumeSlider.value = endSFXVolume / difference;
	}

	public void SetMusicVolume ()
	{
		for (int i = 0; i < allAudioSources.Length; i++)
		{
			if (allAudioSources[i].tag == "Music")
				allAudioSources [i].volume = musicVolumeSlider.value;
		}
	}

	public void SetSFXVolume ()
	{
		for (int i = 0; i < allAudioSources.Length; i++)
		{
			if (allAudioSources[i].tag == "SFX")
				allAudioSources [i].volume = sfxVolumeSlider.value;
		}
	}

	public void BeginDragMasterVolume ()
	{
		beginMasterVolume = masterVolumeSlider.value;
	}

	public void EndDragMasterVolume ()
	{
		endMusicVolume = musicVolumeSlider.value;
		endSFXVolume = sfxVolumeSlider.value;
	}

	public void EndDragMusicVolume ()
	{
		endMusicVolume = musicVolumeSlider.value;
	}

	public void EndDragSFXVolume ()
	{
		endSFXVolume = sfxVolumeSlider.value;
	}
}
