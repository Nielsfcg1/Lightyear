using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSystem : MonoBehaviour
{
	public AvatarMovement avatarMovement;

	RaycastHit hit;

	public Material defaultMaterial;
	public Material selectedMaterial;

	public List <Transform> selectedTiles = new List<Transform> ();

	[HideInInspector] public List <GameObject> tilesToTurn = new List<GameObject> ();

	bool dragging;

	public float pressTime;
	float timer;
	float timerReset = 0;

	[HideInInspector] public Transform lastTile;

	bool alreadyPlaying;

	[HideInInspector] public static bool canSwipe = false;
	public bool swipeing;

	public AudioSource flipSound;

	bool avatarTile = false;

	void Update ()
	{
		if (Input.GetMouseButton (0) && canSwipe && timer <= pressTime)
		{
			timer += Time.deltaTime;

			if (timer > pressTime)
			{
				dragging = true;
				timer = pressTime;
				//timer = timerReset;
			}
		}
		else if (Input.GetMouseButtonUp (0))
			timer = timerReset;

		//Fire a raycast and add every tile that is hit by the raycast to the selectedTiles list.
		if (avatarMovement.gameObject.activeInHierarchy && dragging)
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
			if (selectedTiles.Count >= 2)
				lastTile = selectedTiles [selectedTiles.Count - 1];

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.tag == "Tile")
				{
					if (selectedTiles.Count == 0)
					{
						if (hit.transform.position == avatarMovement.transform.position)
						{
							avatarTile = true;
							RotateTile (hit.transform);
							selectedTiles.Add (hit.transform);
						}
					}
					else if (hit.transform != lastTile && selectedTiles.Count > 1 && avatarTile)
					{
						RotateTile (hit.transform);
						selectedTiles.Add (hit.transform);
					}
					else if (!selectedTiles.Contains (hit.transform) && avatarTile)
					{
						RotateTile (hit.transform);
						selectedTiles.Add (hit.transform);
					}
				}
			}
		}

		//Add the selected tiles to the path that the avatar should walk
		if (Input.GetMouseButtonUp (0))
		{
			if (dragging)
			{
				dragging = false;
				avatarTile = false;

				if (selectedTiles.Count > 0)
				{
					ResetSelectedTiles ();
					selectedTiles.RemoveAt (0);
					lastTile = null;
					CheckNextTile ();
				}
			}
		}			
	}

	//Check if the avatar can move towards the second tile (Move over an orb)
	public void CheckNextTile ()
	{
		if (selectedTiles.Count > 1)
		{
			swipeing = true;
			TileBehaviour tileBehaviour = selectedTiles [1].GetComponent<TileBehaviour> ();
			tileBehaviour.OnMouseDown ();

			if (tileBehaviour.tileToCheck == null || tileBehaviour.tileToCheck.GetComponent<TileBehaviour> ().orbs.Count == 0 || tileBehaviour.wrongColor)
			{
				Debug.LogError ("You cannot walk towards this tile");
				ResetSelectedTiles ();
				selectedTiles.Clear ();
				lastTile = null;
				avatarMovement.CheckLanding ();
			}
			else
			{
				selectedTiles.RemoveAt (1);
				selectedTiles.RemoveAt (0);
			}
		}
		else
		{
			selectedTiles.Clear ();
			lastTile = null;
			avatarMovement.CheckLanding ();
		}
	}

	public void ResetSelectedTiles ()
	{
		for (int i = 0; i < selectedTiles.Count; i++)
		{
			selectedTiles [i].GetComponent<Renderer> ().materials[0].color = defaultMaterial.color;
			//selectedTiles [i].GetComponent<Renderer> ().materials[1].color = defaultMaterial.color;
		}
	}

	void RotateTile (Transform tile)
	{
		//play the animation
		Animator animator;
		animator = tile.GetComponent<Animator> ();

//		if (tile != lastTile)//!selectedTiles.Contains (tile))
//		{
//			//Check which side the tile has to turn
//			if (selectedTiles.Count >= 1)
//			{
//				//if the position.x is smaller than this tile, we are swiping left
//				if (tile.transform.position.x > selectedTiles[selectedTiles.Count -1].position.x && !alreadyPlaying)
//				{
//					//if next tile is on the right side, rotate from left to right
//					animator.SetTrigger ("RotateLeftRight");
//					alreadyPlaying = true;
//				}
//				else if (tile.transform.position.x < selectedTiles[selectedTiles.Count -1].position.x && !alreadyPlaying)
//				{
//					//if next tile is on the left side, rotate from right to left
//					animator.SetTrigger ("RotateRightLeft");
//					alreadyPlaying = true;
//				}
//				if (tile.transform.position.z > selectedTiles[selectedTiles.Count -1].position.z && !alreadyPlaying)
//				{
//					//if next tile is below, rotate from up to down
//					animator.SetTrigger ("RotateUpDown");
//					alreadyPlaying = true;
//				}
//				if (tile.transform.position.z < selectedTiles[selectedTiles.Count -1].position.z && !alreadyPlaying)
//				{
//					//if next tile is above, rotate from down to up
//					animator.SetTrigger ("RotateDownUp");
//					alreadyPlaying = true;
//				}
//			}
//			else
//				animator.SetTrigger ("RotateLeftRight");
//		}

		//if animation is done playing
		tile.GetComponent<Renderer> ().materials[0].color = selectedMaterial.color;
		//flipSound.Play ();
		//tile.GetComponent<Renderer> ().materials[1].color = selectedMaterial.color;

		alreadyPlaying = false;
	}
}
