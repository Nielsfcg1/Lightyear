using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionButtons : MonoBehaviour
{
	public Animator previousButton;
	public Animator nextButton;

	public void SetAnimation (string state)
	{
		previousButton.SetTrigger (state);
		nextButton.SetTrigger (state);
	}
}
