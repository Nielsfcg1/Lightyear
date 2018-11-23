using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Positions
{
	public int[] positionsArray = new int[8];
}

public class LevelMaker : MonoBehaviour
{
	[HideInInspector] public Texture2D defaultTexture;
	[HideInInspector] public Texture2D startTileTexture;
	[HideInInspector] public Texture2D blueTexture;
	[HideInInspector] public Texture2D redTexture;
	[HideInInspector] public Texture2D yellowTexture;
	[HideInInspector] public Texture2D blueYellowTexture;
	[HideInInspector] public Texture2D redBlueTexture;
	[HideInInspector] public Texture2D yellowRedTexture;
	[HideInInspector] public Texture2D portalTexture;

	[Header("Prefabs that will be instantiated")]
	public GameObject tile;
	public GameObject piramid;
	public GameObject cube;
	public GameObject stone;
	public GameObject portal;

	//[Header("Game Manager and Level parent")]
	[HideInInspector] public GameManager gameManager;
	[HideInInspector] public GameObject level;

	[HideInInspector] public Positions[] positions = new Positions[8];

	[HideInInspector] public GUIStyle normal;
	[HideInInspector] public GUIStyle start;
	[HideInInspector] public GUIStyle blue;
	[HideInInspector] public GUIStyle red;
	[HideInInspector] public GUIStyle yellow;
	[HideInInspector] public GUIStyle blueYellow;
	[HideInInspector] public GUIStyle redBlue;
	[HideInInspector] public GUIStyle yellowRed;
	[HideInInspector] public GUIStyle portals;
	[HideInInspector] public GUIStyle [] guiStyles;

	//[Header("Grid Size")]
	[HideInInspector] public int gridWidth;
	[HideInInspector] public int gridHeight;

	[HideInInspector] public GameObject newLevel;

	public GameObject [,] grid;

	Vector3 tileRotation = new Vector3 (0, 45, 0);
	Vector3 tileScale = new Vector3 (0.75f, 0.75f, 0.75f);

	Vector3 orbPosition = new Vector3 (0, 0.25f, 0);
	Vector3 doubleOrbPosition1 = new Vector3 (0.15f, 0.25f, 0.15f);
	Vector3 doubleOrbPosition2 = new Vector3 (-0.15f, 0.25f, -0.15f);
	Vector3 orbScale = new Vector3 (0.25f, 0.25f, 0.25f);

	Vector3 portalPosition = new Vector3 (0, 0.1f, 0);
	Vector3 portalScale = new Vector3 (0.5f, 0.5f, 0.5f);

	[HideInInspector] public string saveLocation;
	[HideInInspector] public string levelName;

	void Awake ()
	{
		if (newLevel != null)
		{
			gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
			level = GameObject.Find ("Level");

			gameManager.levels.Add (newLevel);
			newLevel.transform.SetParent (level.transform);
		}
	}

	void OnApplicationQuit ()
	{
		if (newLevel != null)
			gameManager.levels.Remove (newLevel);
	}

	public void Reset ()
	{
		for (int x = 0; x < gridWidth; x ++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				positions [x].positionsArray[y] = 0;
			}
		}
	}

	public void BuildLevel ()
	{
		DestroyLevel ();

		newLevel = new GameObject ();
		newLevel.name = levelName;
		//newLevel.tag = "GridFolder";

		GridNew gridNewScript = newLevel.AddComponent<GridNew> ();

		grid = new GameObject[gridWidth, gridHeight];

		for (int y = 0; y < gridHeight; y++)
		{
			for (int x = 0; x < gridWidth; x ++)
			{
				GameObject newTile = Instantiate (tile, new Vector3 (x, 0, (gridHeight -1)- y), Quaternion.identity);
				newTile.transform.localEulerAngles = tileRotation;
				newTile.transform.localScale = tileScale;
				newTile.transform.SetParent (newLevel.transform);
				newTile.name = "Tile Location:  " + "X: " + x + "  Z: " + ((gridHeight -1) - y);
				newTile.tag = "Tile";

				grid [x, (gridHeight - 1) - y] = newTile; 

				TileBehaviour tileBehaviour = newTile.GetComponent<TileBehaviour> ();
				tileBehaviour.row = x;
				tileBehaviour.col = (gridHeight -1)- y;

				//If starttile
				if (positions [x].positionsArray [y] == 1)
				{
					newTile.name = "StartTile";
				}

				//If blue
				if (positions [x].positionsArray [y] == 2)
				{
					GameObject newStone = Instantiate (stone);
					newStone.transform.SetParent (newTile.transform);
					newStone.transform.localPosition = orbPosition;
					newStone.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newStone);
				}

				//If piramid
				if (positions [x].positionsArray [y] == 3)
				{
					GameObject newPiramid = Instantiate (piramid);
					newPiramid.transform.SetParent (newTile.transform);
					newPiramid.transform.localPosition = orbPosition;
					newPiramid.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newPiramid);
				}

				//If cube
				if (positions [x].positionsArray [y] == 4)
				{
					GameObject newCube = Instantiate (cube);
					newCube.transform.SetParent (newTile.transform);
					newCube.transform.localPosition = orbPosition;
					newCube.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newCube);
				}

				//If stone and cube
				if (positions [x].positionsArray [y] == 5)
				{
					GameObject newStone = Instantiate (stone);
					newStone.transform.SetParent (newTile.transform);
					newStone.transform.localPosition = doubleOrbPosition1;
					newStone.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newStone);

					GameObject newCube = Instantiate (cube);
					newCube.transform.SetParent (newTile.transform);
					newCube.transform.localPosition = doubleOrbPosition2;
					newCube.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newCube);
				}

				//If piramid and stone
				if (positions [x].positionsArray [y] == 6)
				{
					GameObject newPiramid = Instantiate (piramid);
					newPiramid.transform.SetParent (newTile.transform);
					newPiramid.transform.localPosition = doubleOrbPosition1;
					newPiramid.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newPiramid);

					GameObject newStone = Instantiate (stone);
					newStone.transform.SetParent (newTile.transform);
					newStone.transform.localPosition = doubleOrbPosition2;
					newStone.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newStone);
				}

				//If cube and piramid
				if (positions [x].positionsArray [y] == 7)
				{
					GameObject newCube = Instantiate (cube);
					newCube.transform.SetParent (newTile.transform);
					newCube.transform.localPosition = doubleOrbPosition1;
					newCube.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newCube);

					GameObject newPiramid = Instantiate (piramid);
					newPiramid.transform.SetParent (newTile.transform);
					newPiramid.transform.localPosition = doubleOrbPosition2;
					newPiramid.transform.localScale = orbScale;
					tileBehaviour.orbs.Add (newPiramid);
				}

				//If portal, there needs to be an entrance and an exit
				if (positions [x].positionsArray [y] == 8)
				{
					GameObject newPortal = Instantiate (portal);
					newPortal.transform.SetParent (newTile.transform);
					newPortal.transform.localPosition = portalPosition;
					newPortal.transform.localScale = portalScale;
				}
			}
		}

		gridNewScript.sizeX = gridWidth;
		gridNewScript.sizeZ = gridHeight;
	}

	public void DestroyLevel ()
	{
		DestroyImmediate (newLevel);
		newLevel = null;
	}

	public void Test ()
	{
		BuildLevel ();
	}
}
