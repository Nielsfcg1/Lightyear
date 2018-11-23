using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Mechanic
{
	none,
	firstPlay,
	swipe,
	multipleColors,
	chooseOrb,
	placeAvatar,
	portals,
	addElement,
	shiftRow,
	rotate
}

public class Explanation : MonoBehaviour
{
	CameraMovement cameraMovement;

	public Mechanic mechanic;

	public Text explanationText;
	RectTransform rectTransform;

	void Start ()
	{
		cameraMovement = GameObject.FindWithTag ("MainCamera").GetComponent<CameraMovement> ();
		rectTransform = explanationText.GetComponent<RectTransform> ();

		if (mechanic != Mechanic.none)
		{
			StartCoroutine (GetExplanation (mechanic));
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Z) && cameraMovement.avatar.gameObject.activeInHierarchy)
		{
			StartCoroutine (GetExplanation (mechanic));
		}
	}

	IEnumerator GetExplanation (Mechanic mechanic)
	{
		yield return new WaitForSeconds (0.1f);

		switch (mechanic)
		{
			case Mechanic.none:
			explanationText.text = "ABC\nDEF\nGHI\nJKL\nMNO\nPQR\nSTU\nVWX\nYZ\n123\n456\n789\n0";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;	

			case Mechanic.firstPlay:
			explanationText.text = 
				"\n" +
			"Welkom! Kun jij mij helpen met het verzamelen van alle stenen?\n\n" +
			"Verzamel de stenen door op het vak ernaast te klikken, zodat ik er overheen kan springen.\n\n" +
			"Met de knop onder in je scherm kun je mij weer terugzetten naar mijn beginpositie.\n\n" +
			"Gebruik deze knop als je vastloopt." +
			"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.swipe:
			explanationText.text =
				"\n" +
				"Gaat het klikken te langzaam?\n\n" +
				"Probeer eens het pad vanuit mij te slepen." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.multipleColors:
			explanationText.text = 
				"\n" +
				"Bij dit level zijn er meerdere kleuren die ik moet verzamelen.\n\n" +
				"Dit zal kleur voor kleur moeten.\n\n" +
				"Probeer eerst 1 kleur te verzamelen en daarna de volgende.\n\n" +
				"Het kan voorkomen dat er meerdere kleuren op 1 vak zitten. Je zal hier moeten kiezen welke kleur je wilt verzamelen." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.chooseOrb:
			explanationText.text = 
				"\n" +
				"Bij dit level zijn er paden die elkaar kruisen.\n\n" +
				"Aan het begin van twee paden moet je nu kiezen welke kleur je eerst wil verzamelen.\n\n" +
				"Vind het juiste pad en kies daarbij de juiste startkleur." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.placeAvatar:
			explanationText.text = 
//				"\n" +
//				"Huh waar ben ik?\n\n" +
//				"Ow, bij dit level moet je zelf een startpunt vinden.\n\n" +
//				"Kijk goed naar het level en beslis vanaf welk punt je wilt beginnen.\n\n" +
//				"Klik daarna op deze knop en dan op het vak waar je wil beginnen.\n\n" +
//				"Met deze knop kun je mij opnieuw plaatsen als je vast bent gelopen en vanaf een ander punt wil beginnen." +
//				"\n";
				"\n" +
				"Vanaf het level hierna zal je mij moeten plaatsen op een vak.\n\n" +
				"Kijk goed waar je moet beginnen en klik op de knop onder in je scherm en vervolgens op een vak." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.portals:
			explanationText.text = 
				"\n" +
				"Bij dit level zijn er portalen die het pad doorbreken.\n\n" +
				"Kijk goed waar ik uitkom als ik door een portaal stap." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.addElement:
			explanationText.text = 
				"\n" +
				"In dit level missen er enkele of meerdere objecten uit het pad.\n\n" +
				"Kijk goed op welke plekken er objecten missen en voeg ze toe.\n\n" +
				"Je kunt een object toevoegen door er op te klikken en vervolgens op een vak te klikken." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.shiftRow:
			explanationText.text = 
				"\n" +
				"In dit level is er iets raars aan de hand!\n\n" +
				"Er is namelijk een hele rij met objecten verschoven.\n\n" +
				"Kijk goed welke rij niet klopt en sleep deze naar de juiste positie." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;

			case Mechanic.rotate:
			explanationText.text = 
				"\n" +
				"In dit level is er een een gebied van 2 bij 2 vakken gedraaid.\n\n" +
				"Kijk goed welk gebied niet klopt en draai hem om het pad kloppend te maken." +
				"\n";
			cameraMovement.Open ();
			StartCoroutine(SetPosition ());
			break;
		}
	}

	IEnumerator SetPosition ()
	{
		yield return new WaitForSeconds (1f);
		rectTransform.anchoredPosition = new Vector3 (0, -rectTransform.offsetMax.y, 0);
		explanationText.color = Vector4.one;
	}
}
