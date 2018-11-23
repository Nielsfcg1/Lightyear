using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
	public DataTracker dataTracker;

	public RectTransform xPBar;
	public Text currentXPText;

	public static float currentXP;
	public float maxXP;

	float restXp;

	float rewardXP;

	int tiers = 5;

	bool doAddXP;

	float currentPosX;
	float currentWidth;

	float newPosX;
	float newWidth;

	float posXAmount;
	float widthAmount;

	float speed = 0.5f;

	void Update ()
	{
		if (doAddXP)
		{
			AnimateXPBar ();
		}
	}

	void AnimateXPBar ()
	{
		//print ("PosX to be added: " + posXAmount);
		//print ("Width to be added: " + widthAmount);

		xPBar.offsetMax += new Vector2 (1, 0) * (Time.deltaTime * (widthAmount) * speed);
		xPBar.offsetMin -= new Vector2 (1, 0) * (Time.deltaTime * (widthAmount) * speed);
		xPBar.anchoredPosition += new Vector2 (1, 0) * (Time.deltaTime * (posXAmount * 2) * speed);

		if (xPBar.anchoredPosition.x >= (currentPosX + posXAmount))
		{
			xPBar.offsetMax = new Vector2 (newWidth, 30.0f);
			xPBar.offsetMin = new Vector2 (0, 0);
			xPBar.anchoredPosition = new Vector3 (newPosX, 0);

			CheckProgressTrack ();
			doAddXP = false;
		}
	}

	public void AddXP (float XP)
	{
		float currentFillPercentage = ((currentXP / maxXP) * 100);

		currentPosX = -375 + ((375.0f / 100) * currentFillPercentage);
		currentWidth = 750 - (-2.0f * currentPosX);

		//Set the XP bar back to where it was
		xPBar.offsetMax = new Vector2 (currentWidth, 30.0f);
		xPBar.offsetMin = new Vector2 (0, 0);
		xPBar.anchoredPosition = new Vector3 (currentPosX, 0);

		//Add new XP
		currentXP += XP;

		//Fill the xp bar
		float newFillPercentage = (currentXP / maxXP) * 100;

		newPosX = -375 + ((375.0f / 100) * newFillPercentage);
		newWidth = 750 - (-2.0f * newPosX);

		posXAmount = newPosX - currentPosX;
		widthAmount = newWidth - currentWidth;

		//Enable the animation for the XP Bar
		doAddXP = true;

		//Change the text of the current XP;
		currentXPText.text = (Mathf.RoundToInt(currentXP) + " / " + maxXP).ToString ();
	}

	void CheckProgressTrack ()
	{
		//If the XP bar is at or passed a reward, get that reward
		rewardXP = maxXP / 5;

		//If the XP Bar is full, you have completed the track
		if (currentXP >= maxXP)
		{
			restXp = currentXP - maxXP;
		}

		for (int i = 1; i < tiers; i++)
		{
			if (currentXP >= rewardXP * i)
			{
				GetReward (i);
			}
		}
	}
		
	void GetReward (int tier)
	{
		print ("You have unlocked a reward for Tier " + tier + "!");

		//If you have completed the track, add new rewards to it
		if (restXp > 0)
			print ("Adding new rewards to the Progress Track");
	}
}
