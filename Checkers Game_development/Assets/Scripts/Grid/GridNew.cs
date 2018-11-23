using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNew : MonoBehaviour
{
	public GameObject [,] grid;
	[HideInInspector] public int sizeX;
	[HideInInspector] public int sizeZ;

	void Awake ()
	{
		LoadGrid ();
	}

	void LoadGrid ()
	{
		grid = new GameObject[sizeX, sizeZ];

		for (int i = 0; i < transform.childCount; i++)
		{
			TileBehaviour tileBehaviour = transform.GetChild (i).GetComponent<TileBehaviour> ();

			grid [tileBehaviour.row, tileBehaviour.col] = tileBehaviour.gameObject;

			if (tileBehaviour.name == "StartTile")
			{
				AvatarButton avatarButton = GameObject.Find ("Avatar Button").GetComponent<AvatarButton> ();
				avatarButton.SpawnAvatar (tileBehaviour.gameObject);
				avatarButton.initAvatarLocation = tileBehaviour.gameObject;
				TileBehaviour.avatarLocation = tileBehaviour.gameObject;
			}
		}
	}
}
