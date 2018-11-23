using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	Animator animator;

	public GameObject background;
	public GameObject backgroundMirror;

	public GameObject menuButtons;
	public GameObject options;
	public GameObject characterSelection;
	public GameObject particleSystem;
	public GameObject directionalLight;
	public GameObject title;


	bool checkState;
	float timer;

	void Start ()
	{
		animator = GetComponent<Animator> ();	
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && checkState == false)
		{
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("RotateIn"))
				RotateCamera (false);
		}

		if (checkState)
		{
			timer += Time.deltaTime;

			if (timer >= animator.GetCurrentAnimatorStateInfo (0).length)
			{
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
				{
					backgroundMirror.SetActive (false);
					menuButtons.SetActive (true);
					characterSelection.SetActive (true);
					title.SetActive (true);

					timer = 0;
					checkState = false;
				}
				else if (animator.GetCurrentAnimatorStateInfo (0).IsName ("RotateIn"))
				{
					background.SetActive (false);
					options.SetActive (true);
					timer = 0;
					checkState = false;
				}
			}
		}
	}

	public void RotateCamera (bool direction)
	{
		switch (direction)
		{
			case true:
			backgroundMirror.SetActive (true);
			menuButtons.SetActive (false);
			characterSelection.SetActive (false);
			title.SetActive (false);

			directionalLight.transform.eulerAngles = new Vector3 (50, 150, 0);
			particleSystem.transform.position = new Vector3 (0, 0, -25);

			animator.SetTrigger ("RotateIn");
			checkState = true;
			break;

			case false:
			background.SetActive (true);
			options.SetActive (false);

			directionalLight.transform.eulerAngles = new Vector3 (50, -30, 0);
			particleSystem.transform.position = new Vector3 (0, 0, 10);

			animator.SetTrigger ("RotateOut");
			checkState = true;
			break;
		}
	}
}
