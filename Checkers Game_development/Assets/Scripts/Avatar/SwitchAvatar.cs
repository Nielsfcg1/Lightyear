using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchAvatar : MonoBehaviour
{
	public List <GameObject> avatars = new List<GameObject> ();
	public static int currentAvatarIndex;
	Transform currentAvatar;

	Vector3 resetRotation = new Vector3 (0, 150, 0);
	Vector3 rotation = new Vector3 (0, 0, 0);

	public float sensitivity;
	bool dragging = false;

	float change;
	Vector3 lastPos = new Vector3 (0, 0, 0);

	Animator parentAnimator;
	Animator avatarAnimator;

	void Start ()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			avatars.Add(transform.GetChild (i).gameObject);
		}

		avatars [currentAvatarIndex].SetActive (true);
		currentAvatar = avatars [currentAvatarIndex].transform;

		parentAnimator = GetComponent<Animator> ();
		avatarAnimator = avatars [currentAvatarIndex].GetComponent<Animator> ();
	}

	public void Switch (int direction)
	{
		if (parentAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
		{
			currentAvatarIndex += direction;

			if (currentAvatarIndex < 0)
			{
				currentAvatarIndex = avatars.Count - 1;
			}
			else if (currentAvatarIndex > avatars.Count - 1)
			{
				currentAvatarIndex = 0;
			}

			//If left
			if (direction < 0)
			{
				parentAnimator.SetTrigger ("Left");
				avatarAnimator.SetTrigger ("EaseOutLeft");
				StartCoroutine (SetNewAvatar (direction, avatarAnimator, currentAvatar));
			}
			//If right
			else if (direction > 0)
			{
				parentAnimator.SetTrigger ("Right");
				avatarAnimator.SetTrigger ("EaseOutRight");
				StartCoroutine (SetNewAvatar (direction, avatarAnimator, currentAvatar));
			}
		}
	}

	IEnumerator SetNewAvatar (int direction, Animator animator, Transform oldAvatar)
	{
		yield return new WaitForSeconds (0.3f);
		oldAvatar.gameObject.SetActive (false);
		oldAvatar.localEulerAngles = resetRotation;

		currentAvatar = avatars [currentAvatarIndex].transform;
		avatarAnimator = avatars [currentAvatarIndex].GetComponent<Animator> ();

		currentAvatar.gameObject.SetActive (true);

		if (direction < 0)
		{
			avatarAnimator.SetTrigger ("EaseInLeft");
		}
		else if (direction > 0)
		{
			avatarAnimator.SetTrigger ("EaseInRight");
		}
	}

	public void Rotate ()
	{
		if (dragging)
		{
			//Check if we're swiping left or right
			Vector3 currentMousePos = Input.mousePosition;
			change = currentMousePos.x - lastPos.x;
			lastPos = currentMousePos;

			if (change > 0)
			{
				rotation.y = (-change) * sensitivity;
				currentAvatar.Rotate (rotation);
			}
			else if (change < 0)
			{
				rotation.y = (-change) * sensitivity;
				currentAvatar.Rotate (rotation);
			}
		}
	}

	public void PointerDown ()
	{
		lastPos = Input.mousePosition;
		dragging = true;
	}

	public void EndDrag ()
	{
		dragging = false;
	}
}
