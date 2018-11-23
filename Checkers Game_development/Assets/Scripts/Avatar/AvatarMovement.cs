using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
	public SwipeSystem swipeSystem;
	public List <Transform> path = new List<Transform> ();
	[HideInInspector] public GameObject tileWithOrb;
	[HideInInspector] public int index;

	public float speed;
	float step;

	Transform currentTile;

	[HideInInspector] public Animator avatarAnimator;

	bool playAnimation = true;
	bool canLand = false;

	public AudioSource jumpSound;
	public AudioSource landSound;

	bool checkDistance = true;
	float distance = 2f;
	float currentDistance;

	void Awake ()
	{
		avatarAnimator = transform.GetChild (SwitchAvatar.currentAvatarIndex).GetComponent<Animator> ();
		avatarAnimator.gameObject.SetActive (true);
	}

	void Update ()
	{
		step = speed * Time.deltaTime;

		if (path.Count > 0)
		{
			Move ();
		}

		//If we're able to land, check if our last animation has been finished, then enable clicking again
		if (canLand)
		{
			if (avatarAnimator.GetCurrentAnimatorStateInfo (0).IsName ("IdleStandingAnimation"))
			{
				SwipeSystem.canSwipe = true;
				TileBehaviour.canClick = true;

				playAnimation = true;
				canLand = false;
			}
		}
	}

	void Move ()
	{
		//Play the jump animation
		if (playAnimation)
		{
			//If the swipe system contains tiles, play the swipe animation
			if (swipeSystem.swipeing)
			{
				avatarAnimator.SetTrigger ("SwipeStart");
			}
			else
			{
				avatarAnimator.SetTrigger ("Jump");
			}

			jumpSound.Play ();
	
			playAnimation = false;
		}

		if (TileBehaviour.canClick)
		{
			SwipeSystem.canSwipe = false;
			TileBehaviour.canClick = false;
		}
			
		Vector3 targetPosition = path [0].position;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);

		Vector3 targetDir = targetPosition - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, 100, 0.0f);
		transform.rotation = Quaternion.LookRotation (newDir);

		if (checkDistance)
		{
			currentDistance = Vector3.Distance (transform.position, targetPosition);

			if (currentDistance <= distance / 2)
			{
				tileWithOrb.transform.GetChild (index).GetComponent<Orb> ().OrbBehaviour ();
				checkDistance = false;
			}
		}

		if (transform.position == targetPosition)
		{
			currentTile = path [0].transform;

			transform.SetParent (path[0].transform);

			path.RemoveAt (0);

			checkDistance = true;

			if (currentTile.childCount > 0 && currentTile.GetChild(0).tag == "Portal")
			{
				currentTile.GetChild(0).GetComponent<Portal> ().Teleport (transform);
			}

			CheckLanding ();
		}
	}

	public void CheckLanding ()
	{
		if (swipeSystem.swipeing)
		{
			//If there are still nodes in the swipesystem, run the function again
			if (swipeSystem.selectedTiles.Count > 0)
			{
				TileBehaviour.canClick = true;
				swipeSystem.CheckNextTile ();
			}
			else
			{
				swipeSystem.swipeing = false;
				avatarAnimator.SetTrigger ("SwipeLand");
				landSound.Play ();
				canLand = true;
			}
		}
		else
			canLand = true;
	}

	public void Reset ()
	{
		path.Clear ();
		playAnimation = true;
		checkDistance = true;
		canLand = false;
	}
}
