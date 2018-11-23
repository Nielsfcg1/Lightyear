using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonFunction
{
	start,
	home,
	exit
}

public class MenuButton : MonoBehaviour
{
	public ButtonFunction buttonFunction;

	public void OnButtonClick ()
	{
		switch (buttonFunction)
		{
			case ButtonFunction.start:
			SceneManager.LoadScene ("Game");
			break;

			case ButtonFunction.home:
			SwipeSystem.canSwipe = true;
			TileBehaviour.canClick = true;
			SceneManager.LoadScene ("Main Menu");
			break;

			case ButtonFunction.exit:
			Application.Quit ();
			break;
		}
	}
}
