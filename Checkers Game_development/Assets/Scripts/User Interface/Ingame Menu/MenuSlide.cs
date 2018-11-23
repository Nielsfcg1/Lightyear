using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSlide : MonoBehaviour
{
	public Toggle toggle;

	public Animator animator;
	public AnimationClip slideIn;
	public AnimationClip slideOut;

	public GameObject buttons;
	public GameObject options;

	bool reset = true;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			if (!toggle.isOn && animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
				toggle.isOn = true;
			else if (toggle.isOn && animator.GetCurrentAnimatorStateInfo (0).IsName ("SlideIn"))
			{
				toggle.isOn = false;
				reset = true;
			}
		}

		if (reset)
		{
			if (!toggle.isOn && animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
			{
				buttons.SetActive (true);
				options.SetActive (false);
				reset = false;
			}
		}
	}

	public void ToggleMenu ()
	{
		if (toggle.isOn && animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
		{
			SwipeSystem.canSwipe = false;
			TileBehaviour.canClick = false;
			animator.SetTrigger ("SlideIn");
		}
		else if (!toggle.isOn && animator.GetCurrentAnimatorStateInfo (0).IsName ("SlideIn"))
		{
			SwipeSystem.canSwipe = true;
			TileBehaviour.canClick = true;
			animator.SetTrigger ("SlideOut");
			reset = true;
		}
	}

	public void Toggle ()
	{
		if (toggle.isOn)
			toggle.isOn = false;
		else
			toggle.isOn = true;
	}
}
