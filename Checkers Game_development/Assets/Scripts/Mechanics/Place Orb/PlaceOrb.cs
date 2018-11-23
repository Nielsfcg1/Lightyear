using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceOrb : MonoBehaviour
{
	GameManager gameManager;
	[HideInInspector] public PlaceableOrb placeableOrb;

	[HideInInspector] public List <Transform> orbs = new List<Transform> ();
	public GameObject placeableOrbPrefab;
	GameObject popUp;

	[HideInInspector] public Transform level;

	[HideInInspector] public GameObject selectedOrb;

	[HideInInspector] public bool canPlace;

	RaycastHit hit;

    Vector3 orbPosition = new Vector3(0, 0.1f, 0);

    Vector3 doubleOrbPosition1 = new Vector3(-0.2f, 0.1f, 0f);
    Vector3 doubleOrbPosition2 = new Vector3(0.2f, 0.1f, 0f);
    Vector3 doubleOrbPosition3 = new Vector3(0f, 0.1f, -0.2f);


    List <string> placeableOrbs = new List<string> ();
	List <int> amounts = new List<int> ();

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		popUp = transform.GetChild (0).gameObject;

        level = gameManager.currentLevel;
		CheckLevel ();
	}

	void Update ()
	{
		if (canPlace)
		{
			if (Input.GetMouseButtonDown (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast(ray, out hit))
				{
					if (hit.transform.tag == "Tile")
					{
						Place (selectedOrb, hit.transform);
					}
				}
			}
		}
	}

	void CheckLevel ()
	{
		for (int t = 0; t < level.childCount; t++)
		{
			for (int o = 0; o < level.GetChild(t).childCount; o++)
			{
				if (level.GetChild(t).tag != "Orb")
				{
					if (!level.GetChild (t).GetChild (o).gameObject.activeInHierarchy)
					{
						orbs.Add (level.GetChild (t).GetChild (o).transform);
						level.GetChild (t).GetComponent<TileBehaviour> ().orbs.Remove (level.GetChild (t).GetChild (o).gameObject);
						level.GetChild (t).GetChild (o).SetParent (level);
						t--;
					}
				}
			}
		}

		AddOrbs ();
	}

	void AddOrbs ()
	{
		for (int i = 0; i < orbs.Count; i++) 
		{
			if (placeableOrbs.Contains(orbs[i].name))
			{
				int index = placeableOrbs.IndexOf (orbs [i].name);
				amounts [index]++;
				popUp.transform.GetChild (index).GetChild (0).GetComponent<Text> ().text = amounts[index] + "X";
				popUp.transform.GetChild (index).GetComponent<PlaceableOrb> ().orbs.Add (orbs [i].transform);
			}
			else
			{
				Transform newPlaceableOrb = Instantiate (placeableOrbPrefab.transform);
				newPlaceableOrb.name = orbs [i].name;
				newPlaceableOrb.SetParent (popUp.transform);
                newPlaceableOrb.localScale = Vector3.one;

				placeableOrbs.Add (newPlaceableOrb.name);

				newPlaceableOrb.GetComponent<Image> ().sprite = orbs [i].GetComponent<Orb> ().orbSprite;
				newPlaceableOrb.GetComponent<PlaceableOrb> ().orbs.Add (orbs[i].transform);

				amounts.Add (1);
				newPlaceableOrb.localPosition = new Vector3 (-700, (300 - (150 * amounts.Count)), 0);
				newPlaceableOrb.GetChild (0).GetComponent<Text> ().text = amounts[amounts.Count -1] + "X";
			}
		}
	}

	void Place (GameObject orb,Transform parent)
	{
		orb.transform.SetParent (parent);
		parent.GetComponent<TileBehaviour> ().orbs.Add (orb);

        //If the parent has 2 orbs
        if (parent.childCount == 2 && parent.GetChild(0).gameObject.activeInHierarchy)
        {
            parent.GetChild(0).localPosition = doubleOrbPosition1;
            orb.transform.localPosition = doubleOrbPosition2;
        }
        else if (parent.childCount == 3 && parent.GetChild(0).gameObject.activeInHierarchy)
        {
            parent.GetChild(0).localPosition = doubleOrbPosition1;
            parent.GetChild(1).localPosition = doubleOrbPosition2;
            orb.transform.localPosition = doubleOrbPosition3;
        }
        else
            orb.transform.localPosition = orbPosition;

        placeableOrb.SetIndex ();

		orb.SetActive (true);
		selectedOrb = null;
		placeableOrb = null;
		canPlace = false;
	}

	public void Reset ()
	{
		for (int i = 0; i < popUp.transform.childCount; i++)
		{
			popUp.transform.GetChild (i).gameObject.SetActive (true);
		}
	}
}
