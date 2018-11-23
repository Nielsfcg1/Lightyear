using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableOrb : MonoBehaviour
{
	PlaceOrb placeOrb;

	[HideInInspector] public List <Transform> orbs = new List<Transform> ();
	int index = 0;

	void Start ()
	{
		placeOrb = transform.parent.parent.GetComponent<PlaceOrb> ();
	}

	public void Select ()
	{
		placeOrb.selectedOrb = orbs [index].gameObject;
		placeOrb.placeableOrb = this;
		placeOrb.canPlace = true;
	}

	public void SetIndex ()
	{
		index++;
		transform.GetChild (0).GetComponent<Text> ().text = orbs.Count -1 + "X";

		//Reset the placeable orb when all of its orbs are placed
		if (index >= orbs.Count)
		{
			index = 0;
			transform.GetChild (0).GetComponent<Text> ().text = orbs.Count + "X";
			gameObject.SetActive (false);
		}
	}
}
