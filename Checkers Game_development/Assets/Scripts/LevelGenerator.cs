using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [Header("Extra Mechanics")]
    public GameObject portal;
    [HideInInspector] public GameObject portalTile;

    public Grid gridFolder;
    public GameManager gm;
    public ShapeDatabase shapeDatabase;


    [Header("Setup Level generator")]
    public List<GameObject> orbTypes;
    public int orbsPerColor;


    public int currentColor = 0;
    public int orbsLeft;

    //If level will be generated step for step, making these variables public will help you debug
    GameObject currentOrb;
    public GameObject currentTile;
    public List<char> possibleDirections;
    char direction;

    GameObject initTile;
    [HideInInspector] public GameObject tileCheck;

    public List<char> currentShape = new List<char>();
    public int currentShapeINT = 0;
    public List<List<char>> shapesInLevel = new List<List<char>>();

    Vector3 orbPosition = new Vector3(0, 0.1f, 0);

	Vector3 doubleOrbPosition1 = new Vector3(-0.2f, 0.1f, 0f);
	Vector3 doubleOrbPosition2 = new Vector3(0.2f, 0.1f, 0f);
	Vector3 doubleOrbPosition3 = new Vector3(0f, 0.1f, -0.2f);
    Vector3 orbScale = new Vector3(0.175f, 0.175f, 0.175f);

    [HideInInspector] public bool allLevelsPossible;
    bool portalSpawn;
    public bool removeRandomOrb;
    public bool placeAvatar = true;

    public List<int> shapes = new List<int>();      //0 = line, 1 = circle, 2 = loop

    //Information for the DataTracker
    public int tilesWithMultipleOrbs = 0;

	static bool firstTime0 = true;
	static bool firstTime1 = true;
	static bool firstTime2 = true;
	static bool firstTime3 = true;
	static bool firstTime4 = true;
	static bool firstTime5 = true;
	static bool firstTime6 = true;

	public Explanation explanation;

    void Start ()
    {
//        print(placeAvatar);
//        gridFolder = GameObject.FindGameObjectWithTag("GridFolder").GetComponent<Grid>();
//        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
//        shapeDatabase = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ShapeDatabase>();
	}
	
    void SetInfoForDataTracker()
    {
        tilesWithMultipleOrbs = 0;

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (tile.GetComponent<TileBehaviour>().orbs.Count > 1)
            {
                tilesWithMultipleOrbs++;
            }
        }



        //Debugging
        //print(tilesWithMultipleOrbs);

        for(int i = 0; i < shapesInLevel.Count; i++)
        {
            //print(shapesInLevel[i]);
        }
    }

	public void CreateLevel(int difficulty)
    {
		gridFolder.GenerateGrid ();
        allLevelsPossible = true;
        //Choose starting tile
        //currentTile = gridFolder.grid[Random.Range(0, gridFolder.sizeX), Random.Range(0, gridFolder.sizeZ)].gameObject;

        //Loop testing
        switch (Random.Range(0, 2))
        {
            case 0: currentTile = gridFolder.grid[Random.Range(0, gridFolder.sizeX), Random.Range(2, gridFolder.sizeZ - 2)].gameObject; break;
            case 1: currentTile = gridFolder.grid[Random.Range(2, gridFolder.sizeX - 2), Random.Range(0, gridFolder.sizeZ)].gameObject; break;
        }

        //Testing the shapes
        //currentTile = gridFolder.grid[2, 2].gameObject;

        tileCheck = currentTile;

        //Set counters
        orbsLeft = orbsPerColor;
        currentOrb = orbTypes[currentColor].gameObject;

		if (difficulty == 0)
		{
            //print("Level 0");

			if (firstTime0)
			{
				placeAvatar = true;
				explanation.mechanic = Mechanic.firstPlay;
				firstTime0 = false;
			}

			shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false)); shapes.Add(0);
		}
		else if (difficulty == 1)
        {
            //print("Level 1");

			if (firstTime1)
			{
				placeAvatar = true;
				explanation.mechanic = Mechanic.multipleColors;
				firstTime1 = false;
			}

            shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false)); shapes.Add(0);
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
        }
		else if (difficulty == 2)
        {
            //print("Level 2");

			if (firstTime2)
			{
				placeAvatar = true;
				explanation.mechanic = Mechanic.placeAvatar;
				firstTime2 = false;
			}
			else
				placeAvatar = false;
			
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
            shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false)); shapes.Add(0);
        }
		else if (difficulty == 3)
        {
            //print("Level 3");
			placeAvatar = false;
			
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
        }
		else if (difficulty == 4)
        {
            //print("Level 4");

			if (firstTime4)
			{
				placeAvatar = true;
				explanation.mechanic = Mechanic.swipe;
				firstTime4 = false;
			}
			else
            	placeAvatar = false;
			
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
        }
		else if (difficulty == 5)
        {
            //print("Level 5");

			if (firstTime5)
			{
				placeAvatar = true;
				explanation.mechanic = Mechanic.addElement;
				firstTime5 = false;
			}
			else
            	placeAvatar = false;
			
            removeRandomOrb = true;
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck)); shapes.Add(1);
            shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false)); shapes.Add(0);
        }
		else if (difficulty == 6)
        {
            //print("Level 6");
            placeAvatar = false;
            removeRandomOrb = true;
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
            shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck)); shapes.Add(2);
        }

        //        shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck));        shapes.Add(2);
        //        shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck));      shapes.Add(1);
        //        shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false)); shapes.Add(0);

        //initTile = currentTile;

        //shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false));
        //shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false));
        //shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck, false));


        //       print("shape 1 " + tileCheck);
        //shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck));


        //     print("shape 2 " + tileCheck);
        //shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck));



        //shapesInLevel.Add(shapeDatabase.DrawLoop(6, tileCheck));

        //      print("shape 3 " + tileCheck);
        //shapesInLevel.Add(shapeDatabase.DrawLine(6, tileCheck));


        //shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck));
        //shapesInLevel.Add(shapeDatabase.DrawCircle(6, tileCheck));


        //currentTile = initTile;

        if (allLevelsPossible)
        {
            SetTiles();
        }
        else
        {
            print("One or more shapes are not possible. Creating a new level");
			CreateLevel(DataTracker.levelType);
        }
    }

    //When a random shape has to be generated. (is always possible, backup plan for when a preset shape can't be inserted (for w/e reason).
    char CheckPossibleDirections()
    {
        //Set previous Direction
        char previousDirection = direction;

        //Reset list of possible directions
        possibleDirections.Clear();

        //Shortened references
        int nRow = currentTile.GetComponent<TileBehaviour>().row;
        int nCol = currentTile.GetComponent<TileBehaviour>().col;

        //Setup directions
        char N = 'N'; char E = 'E'; char S = 'S'; char W = 'W';

        //North
        if (nCol + 1 < gridFolder.sizeZ - 1 && previousDirection != 'S')
            possibleDirections.Add(N);

        //East
        if (nRow + 1 < gridFolder.sizeX - 1 && previousDirection != 'W')
            possibleDirections.Add(E);

        //South
        if (nCol - 1 > 0 && previousDirection != 'N')
            possibleDirections.Add(S);

        //West
        if (nRow - 1 > 0 && previousDirection != 'E')
            possibleDirections.Add(W);

        return possibleDirections[Random.Range(0, possibleDirections.Count)];
    }

    bool CheckShapePossibility(List<char> shape)
    {
        GameObject cTile;
        cTile = currentTile;

        int H = 0; int Z = 0;

        //Check if shape [0 is possible, then 1, then 2]
        for (int i = 0; i < shape.Count; i++)
        {
            direction = shape[i];

            //Shortened references
            int nRow = cTile.GetComponent<TileBehaviour>().row;
            int nCol = cTile.GetComponent<TileBehaviour>().col;

            switch (direction)
            {
                case 'N':
                    H = 0; Z = 1;

                    if (nCol + 1 < gridFolder.sizeZ - 1) { }
                    else
                        return false;

                    break;

                case 'E':
                    H = 1; Z = 0;

                    if (nRow + 1 < gridFolder.sizeX - 1){ }
                    else
                        return false;

                    break;

                case 'S':
                    H = 0; Z = -1;

                    if (nCol - 1 > 0) { }
                    else
                        return false;

                    break;

                case 'W':
                    H = -1; Z = 0;

                    if (nRow - 1 > 0) { }
                    else
                        return false;

                    break;
            }

            cTile = gridFolder.grid[nRow + H * 2, nCol + Z * 2].gameObject;
        }

        return true;
    }

    void SetTiles()
    {
        //currentShape.Clear();
        currentShape = shapesInLevel[currentShapeINT];

        //print(shapesInLevel[currentShapeINT]);

        //Create variables for different directions
        int H = 0; int Z = 0;

        for (int i = 0; i < currentShape.Count; i++)
        {
            switch (currentShape[i])
            {
                case 'N':
                    H = 0; Z = 1;
                    break;
                case 'E':
                    H = 1; Z = 0;
                    break;
                case 'S':
                    H = 0; Z = -1;
                    break;
                case 'W':
                    H = -1; Z = 0;
                    break;
                case 'P':
                    portalSpawn = true;
                    break;
            }

            //print(currentShape[i] + " + " + currentTile.name);

            //Shortened references
            int nRow = currentTile.GetComponent<TileBehaviour>().row;
            int nCol = currentTile.GetComponent<TileBehaviour>().col;


            GameObject portalGO;
            GameObject portalExitGO;

            if (portalSpawn)
            {
                print("Next object has to be a portal");
                //Instantiate portal at currentTile
                portalGO = Instantiate(portal, gameObject.transform);
                
                //Set parent
                portalGO.transform.SetParent(currentTile.transform);
                portalGO.transform.localScale = Vector3.one;
                portalGO.transform.localPosition = new Vector3(0, 0,0);

                currentTile = portalTile;

                portalExitGO = Instantiate(portal, gameObject.transform);

                portalExitGO.transform.SetParent(currentTile.transform);
                portalExitGO.transform.localScale = Vector3.one;
                portalExitGO.transform.localPosition = new Vector3(0, 0.1f, 0);

                portalSpawn = false;

                continue;
            }

            //Instantiate orb
            GameObject orb = Instantiate(currentOrb, gameObject.transform) as GameObject;

            //Add orb to the tile
            gridFolder.grid[nRow + H, nCol + Z].GetComponent<TileBehaviour>().orbs.Add(orb);

            //Set parent
            Transform cTile = gridFolder.grid[nRow + H, nCol + Z].transform;
            orb.transform.SetParent(cTile);

            orb.transform.localScale = orbScale;

            //Set position on top of the tile
            int orbsOnTile = cTile.GetComponent<TileBehaviour>().orbs.Count;

            if (orbsOnTile == 1)
            {
                orb.transform.localPosition = orbPosition;
            }
            else if(orbsOnTile == 2)
            {
                cTile.GetComponent<TileBehaviour>().orbs[0].transform.localPosition = doubleOrbPosition1;
				orb.transform.localPosition = doubleOrbPosition2;
            }
			else if (orbsOnTile == 3)
            {
				cTile.GetComponent<TileBehaviour>().orbs[0].transform.localPosition = doubleOrbPosition1;
				cTile.GetComponent<TileBehaviour>().orbs[1].transform.localPosition = doubleOrbPosition2;
				orb.transform.localPosition = doubleOrbPosition3;
                //orb.transform.position = new Vector3(cTile.transform.position.x - 0.3f + (orbsOnTile * 0.3f), 0.1f, cTile.transform.position.z);
            }
                
            

            //Add orb to all orbs list
            gm.allOrbs.Add(orb);

            //Set new current tile
            currentTile = gridFolder.grid[nRow + H * 2, nCol + Z * 2].gameObject;
        }

        ////Check possible direction
        //direction = CheckPossibleDirections();

        currentColor++;
       	currentShapeINT++;

		if (currentColor == shapesInLevel.Count)
        {
            if(removeRandomOrb)
            {
                int num = Random.Range(0, gm.allOrbs.Count);

                for(int i = 0; i < gm.allOrbs.Count; i++)
                {
                    if (num == i)
                        gm.allOrbs[i].gameObject.SetActive(false);
                    else
                        continue;
                }
            }

            if(placeAvatar)
            {
                var go = GameObject.Find("Avatar Button").GetComponent<AvatarButton>();
                go.SpawnAvatar(currentTile);
                go.initAvatarLocation = currentTile;
                TileBehaviour.avatarLocation = currentTile;
            }

            SetInfoForDataTracker();

            SetGameManager();

            //
        }
        else
        {
			//print ("Ja");
            currentOrb = orbTypes[currentColor];

            SetTiles();
        }
    }
    

    /*void SetTiles()
    {
        //Shortened references
        int nRow = currentTile.GetComponent<TileBehaviour>().row;
        int nCol = currentTile.GetComponent<TileBehaviour>().col;

        //Check possible direction
        direction = CheckPossibleDirections();

        //Create variables for different directions
        int H = 0; int Z = 0;

        switch (direction)
        {
            case 'N':
                H = 0; Z = 1;
                break;
            case 'E':
                H = 1; Z = 0;
                break;
            case 'S':
                H = 0; Z = -1;
                break;
            case 'W':
                H = -1; Z = 0;
                break;
        }

        //Add orb to the tile
        gridFolder.grid[nRow + H, nCol + Z].GetComponent<TileBehaviour>().orbs.Add(currentOrb);

        //Instantiate orb
        GameObject orb = Instantiate(currentOrb, gameObject.transform) as GameObject;

        //Set parent
        Transform cTile = gridFolder.grid[nRow + H, nCol + Z].transform;
        orb.transform.SetParent(cTile);

        //Set position on top of the tile
        int orbsOnTile = cTile.GetComponent<TileBehaviour>().orbs.Count;

        orb.transform.position = new Vector3(cTile.transform.position.x - 0.3f + (orbsOnTile * 0.3f), 0.1f, cTile.transform.position.z);

        //Add orb to all orbs list
        gm.allOrbs.Add(orb);

        //Set new current tile
        currentTile = gridFolder.grid[nRow + H*2, nCol + Z*2].gameObject;

        orbsLeft--;

        if (orbsLeft > 0)
        {
            SetTiles();
        }
        else if (orbsLeft == 0)
        {
            currentColor++;
            
            orbsLeft = orbsPerColor;

            if (currentColor == orbTypes.Count)
            {
                SetGameManager();
            }
            else
            {
                currentOrb = orbTypes[currentColor];
                SetTiles();
            }
        }
    }
    */


    //Might be a little redundant? But for testing purposes it is a good solution I guess.
    void SetGameManager()
    {
        gm.currentLevel = gridFolder.transform;
        gm.SetVariablesFromLevelGenerator();
    }

    //Reset level (AFTER RESET THE LEVEL IS NOT YET PLAYABLE)
    public void RemoveOrbs()
    {
        gm.OrbColors.Clear();
        shapes.Clear();
        gm.currentColorINT = -1;
        gm.allOrbs.Clear();
        gm.addedColors.Clear();
        //gm.ResetOrbs();

        foreach (GameObject orb in GameObject.FindGameObjectsWithTag("Orb"))
        {
            orb.transform.GetComponentInParent<TileBehaviour>().orbs.Clear();
            Destroy(orb);
        }

        currentColor = 0;
        orbsLeft = 0;

        currentOrb = null;
        currentTile = null;
        tileCheck = null;

		CreateLevel(DataTracker.levelType);
        //SetGameManager();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            //CreateLevel();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetTiles();
        }


        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveOrbs();
        }

        if (Input.GetKey(KeyCode.Insert))
        {
            RemoveOrbs();
        }



        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    CheckPossibleDirections();
        //}
    }
}
