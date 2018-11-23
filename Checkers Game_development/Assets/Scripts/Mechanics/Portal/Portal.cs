using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public Transform exit;

	void Start ()
	{
		foreach (GameObject portal in GameObject.FindGameObjectsWithTag ("Portal"))
		{
			if (portal != gameObject)
				exit = portal.transform;
		}
	}

	public void Teleport (Transform avatar)
	{
		avatar.SetParent(exit.parent);
		avatar.localPosition = Vector3.zero;
		TileBehaviour.avatarLocation = exit.parent.gameObject;
	}
}
