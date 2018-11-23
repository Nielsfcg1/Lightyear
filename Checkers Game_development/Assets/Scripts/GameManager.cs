using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Orb Information")]
    public List<GameObject> OrbColors;
    public int[] OrbsLeft;
    public int currentColorINT = -1;

    public List<GameObject> allOrbs = new List<GameObject>();
    public List<string> addedColors = new List<string>();

    [Header("Move Counter")]
	public Text moveCounter;
	public int moves;

	[Header("Color Counter")]
	public Text colorCounter;
	public int colorAmount;
	public int completedColors;

	public List <GameObject> levels = new List<GameObject> ();
	public static int level = 0;

	DataTracker dataTracker;
    public Transform currentLevel;

	public AudioSource completionSound;

    void Start()
    {
        dataTracker = GetComponent<DataTracker>();

        //levels[level].SetActive(true);
    }

    public void SetVariablesFromLevelGenerator()
    {
        SetOrbColors();

        colorAmount = OrbColors.Count;
		UpdateColorCounter (completedColors, colorAmount);
        OrbsLeft = new int[OrbColors.Count];

        SetOrbTypes();
    }

    //Set all orbtypes to corresponding number.
    public void SetOrbTypes()
    {
        for (int i = 0; i < OrbColors.Count; i++)
        {
            string x = OrbColors[i].name;// + "(Clone)";

			foreach (GameObject orb in allOrbs)
            {
                if (orb.GetComponent<Orb>().orbType < 0)
                {
                    if (orb.name == x)
                    {
                        orb.GetComponent<Orb>().orbType = i;
                        OrbsLeft[i]++;
                    }
                }
            }
        }
    }

    public void ResetOrbs()
    {
        for(int i = 0; i < OrbColors.Count; i++)
        {
            OrbsLeft[i] = 0;
        }

        currentColorINT = -1;
        SetOrbTypes();
    }

	public void UpdateMoveCounter ()
	{
		moveCounter.text = moves.ToString ();
	}

	public void UpdateColorCounter (int completedColorToAdd, int colorAmount)
	{
		colorCounter.text = (completedColors + completedColorToAdd) + "/" + colorAmount;

		if (completedColorToAdd != 0)
			completedColors++;

        if(completedColors == colorAmount)
        {
			completionSound.Play ();
			StartCoroutine (FinishLevel());
        }
	}

    //Checks which different color types there are in a level
    void SetOrbColors()
    {
        foreach (GameObject orb in allOrbs)
        {
            bool addOrb = true;

            for (int i = 0; i < addedColors.Count; i++)
            {
                if (addedColors[i] == orb.name)
                {
                    addOrb = false;
                }
            }

            if (addOrb)
            {
                OrbColors.Add(orb.gameObject);
                addedColors.Add(orb.name);
            }
        }

		colorAmount = OrbColors.Count;
		UpdateColorCounter (completedColors, colorAmount);
    }

	IEnumerator FinishLevel()
    {
		yield return new WaitForSeconds (2f);
		//Play the black hole animation

        //Load a new level
		if (level < levels.Count -1)
			level++;
		
		dataTracker.CalculateXP ();
    }

}
