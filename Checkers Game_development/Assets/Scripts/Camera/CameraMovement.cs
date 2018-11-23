using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public GameObject popUp;

	Transform mainCamera;
	Transform currentTile;
	public Transform avatar;

	bool zoomIn;
	bool zoomOut;

	Vector3 normalPosition = new Vector3 (3.5f, 4.25f, -2.35f);
	Vector3 normalRotation = new Vector3 (40, 0, 0);

	Vector3 avatarRotation = new Vector3 (0, 180, 0);

	public float speed;

	void Start ()
	{
		mainCamera = transform;
		popUp = GameObject.Find ("Heads Up Display").transform.GetChild (4).GetChild (0).gameObject;
	}

	void Update ()
	{
		if (zoomIn)
		{
			Zoom (true);
		}

		if (zoomOut)
		{
			Zoom (false);
		}
	}

	void Zoom (bool direction)
	{
		if (direction == true)
		{
			Vector3 position = new Vector3 (currentTile.position.x, 0.75f, (currentTile.position.z - 0.75f));
			Vector3 rotation = new Vector3 (25, 25, 0);
			mainCamera.GetComponent<Camera> ().orthographic = false;
			mainCamera.position = Vector3.MoveTowards (mainCamera.position, position, speed);
			mainCamera.eulerAngles = Vector3.MoveTowards (mainCamera.eulerAngles, rotation, (speed * 6));

			if (mainCamera.position == position &&
				mainCamera.eulerAngles == rotation)
			{
				zoomIn = false;
				popUp.SetActive (true);
			}
		}
		else if (direction == false)
		{
			mainCamera.position = Vector3.MoveTowards (mainCamera.position, normalPosition, speed);
			mainCamera.eulerAngles = Vector3.MoveTowards (mainCamera.eulerAngles, normalRotation, (speed * 6));

			if (mainCamera.position == normalPosition &&
				mainCamera.eulerAngles == normalRotation)
			{
				mainCamera.GetComponent<Camera> ().orthographic = true;
				zoomOut = false;
				SwipeSystem.canSwipe = true;
				TileBehaviour.canClick = true;
				HUD.instance.ToggleHUD (true, null);
			}
		}
	}

	public void Open ()
	{
		avatar.localEulerAngles = avatarRotation;
		currentTile = avatar.parent;

		SwipeSystem.canSwipe = false;
		TileBehaviour.canClick = false;

		HUD.instance.ToggleHUD (false, popUp.transform.parent.gameObject);

		zoomIn = true;
	}

	public void Close ()
	{
		popUp.SetActive (false);
		zoomOut = true;
	}
}
