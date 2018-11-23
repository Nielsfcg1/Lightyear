using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOrb : MonoBehaviour
{
	[HideInInspector] public List <Transform> orbs = new List<Transform> ();
	public List <Image> images = new List<Image> ();
	List <Sprite> sprites = new List<Sprite> ();
	List <Animator> animators = new List<Animator> ();

	public GameObject popUp;
	public MonoBehaviour tileBehaviour;
	public GameManager gameManager;

	public void ShowPopUp (MonoBehaviour script)
	{
		SwipeSystem.canSwipe = false;
		TileBehaviour.canClick = false;

		tileBehaviour = script;

		for (int i = 0; i < orbs.Count; i++)
		{
			sprites.Add (orbs [i].GetComponent<Orb> ().orbSprite);
			images [i].sprite = sprites [i];

			animators.Add (images [i].GetComponent<Animator> ());

			animators [i].GetComponent<Button> ().onClick.RemoveAllListeners ();

			int index = orbs [i].transform.GetSiblingIndex ();
			animators [i].GetComponent<Button> ().onClick.AddListener (() => Choose(index));
		}

		popUp.SetActive (true);

		if (animators.Count <= 2)
		{
			animators [0].SetTrigger ("MoveInLeft");
			animators [1].SetTrigger ("MoveInRight");
		}
		else if (animators.Count > 2)
		{
			animators [0].SetTrigger ("MoveInLeft");
			animators [1].SetTrigger ("MoveInCenter");
			animators [2].SetTrigger ("MoveInRight");
		}
	}

	public void Choose (int index)
	{
		if (animators.Count <= 2)
		{
			animators [0].SetTrigger ("MoveOutLeft");
			animators [1].SetTrigger ("MoveOutRight");
		}
		else if (animators.Count > 2)
		{
			animators [0].SetTrigger ("MoveOutLeft");
			animators [1].SetTrigger ("MoveOutCenter");
			animators [2].SetTrigger ("MoveOutRight");
		}

		//disable the popup after the last animation is done playing (length = 0.833 seconds)
		Invoke ("DisablePopUp", 0.833f);

		SwipeSystem.canSwipe = true;
		TileBehaviour.canClick = true;

		tileBehaviour.GetComponent<TileBehaviour>().index = index;
		tileBehaviour.GetComponent<TileBehaviour>().MoveTowardsTile (tileBehaviour.GetComponent<TileBehaviour>().targetLocation, tileBehaviour.GetComponent<TileBehaviour>().tileToCheck.transform.GetChild(index).GetComponent<Orb> ());
		gameManager.currentColorINT = tileBehaviour.GetComponent<TileBehaviour> ().tileToCheck.transform.GetChild (index).GetComponent<Orb> ().orbType;
		ClearOrbColors ();
	}

	public void ClearOrbColors ()
	{
		orbs.Clear ();
		sprites.Clear ();
		animators.Clear ();
	}

	void DisablePopUp ()
	{
		popUp.SetActive (false);
	}
}
