using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AvatarButtonState
{
	inactive,
	active,
	reset
}

public class AvatarButton : MonoBehaviour
{
	//Scripts
	public GameManager gameManager;
	public SwipeSystem swipeSystem;
	public AvatarMovement avatarMovement;
	public PlaceOrb placeOrb;

	public Image avatarButtonImage;
	public Button button;

	public AvatarButtonState avatarButtonState;

	public GameObject avatar;

	public GameObject initAvatarLocation;

	public Sprite [] buttonSprites;

	public AudioSource landSound;

	Vector3 startRotation = new Vector3 (0, 180, 0);

	//This function is called when the avatar button is pressed
	public void AvatarButtonPress ()
	{
		switch (avatarButtonState)
		{
			case AvatarButtonState.inactive:
			avatarButtonState = AvatarButtonState.active;
		
			ChangeSprites(buttonSprites [6], buttonSprites [7], buttonSprites [8]);
			break;

			case AvatarButtonState.active:
			avatarButtonState = AvatarButtonState.inactive;

			ChangeSprites(buttonSprites[0], buttonSprites [1], buttonSprites [2]);
			break;

			case AvatarButtonState.reset:
			avatarButtonState = AvatarButtonState.inactive;

			ChangeSprites(buttonSprites [0], buttonSprites [1], buttonSprites [2]);
			Reset ();
			break;
		}
	}

	void ChangeSprites (Sprite buttonImage, Sprite highlighted, Sprite pressed)
	{
		avatarButtonImage.sprite = buttonImage;
		SpriteState spriteState;
		spriteState.highlightedSprite = highlighted;
		spriteState.pressedSprite = pressed;

		button.spriteState = spriteState;
	}

	public void SpawnAvatar (GameObject location)
	{
		//avatar.transform.localRotation = Quaternion.identity;
		avatar.transform.position = location.transform.position;
		avatar.transform.SetParent (location.transform);
		avatar.transform.localEulerAngles = startRotation;
		avatar.SetActive (true);

		avatarButtonState = AvatarButtonState.reset;

		ChangeSprites(buttonSprites [3], buttonSprites [4], buttonSprites [5]);

        avatarMovement.avatarAnimator.SetTrigger("Landing");
		landSound.Play ();

		SwipeSystem.canSwipe = true;
    }

	//This function resets the whole level
	public void Reset ()
	{
		//gameManager.moves = 0;
		//gameManager.UpdateMoveCounter ();
		gameManager.completedColors = 0;
		gameManager.UpdateColorCounter (gameManager.completedColors, gameManager.colorAmount);

		foreach (GameObject orb in gameManager.allOrbs)
		{
			if (!placeOrb.orbs.Contains (orb.transform))
			{
				orb.SetActive (true);
				orb.GetComponent<Orb> ().orbType = -1;
			}
			else
			{
				orb.SetActive (false);
				orb.transform.SetParent (placeOrb.level);
				orb.GetComponent<Orb> ().orbType = -1;
			}
		}

		placeOrb.Reset ();
			
		gameManager.ResetOrbs ();

		if (initAvatarLocation != null)
		{
			avatar.SetActive (false);
			avatar.transform.SetParent (avatar.transform.parent.parent.parent);
			avatar.transform.position = Vector3.zero;
			avatar.transform.localEulerAngles = startRotation;
			avatar.transform.GetChild(SwitchAvatar.currentAvatarIndex).transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			SpawnAvatar (initAvatarLocation);
			TileBehaviour.avatarLocation = initAvatarLocation;

			SwipeSystem.canSwipe = true;
			TileBehaviour.canClick = true;
		}
		else
		{
			avatar.SetActive (false);
			avatar.transform.SetParent (avatar.transform.parent.parent.parent);
			avatar.transform.position = Vector3.zero;
			avatar.transform.localEulerAngles = startRotation;
			avatar.transform.GetChild(SwitchAvatar.currentAvatarIndex).transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

			SwipeSystem.canSwipe = false;
			TileBehaviour.canClick = true;
		}

		avatarMovement.Reset ();
		swipeSystem.selectedTiles.Clear ();
		swipeSystem.lastTile = null;
	}
}