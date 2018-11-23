using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public GameObject[,] grid;

    [Header("Prefab")]
    public GameObject tilePrefab;

	public List <GameObject> tiles = new List<GameObject> ();

    [Header("Grid Generation")]
    public int sizeX;
    public int sizeZ;
    int initSizeX;
    int initSizeZ;
    int tilesCreated;
    int totalTiles;
    int tilesPerRow;

    public bool generateLevel;
    int counter = 0;

    Transform gridFolder;
    GameManager gm;

    Vector3 tileRotation = new Vector3(0, 45, 0);
    Vector3 tileScale = new Vector3(0.75f, 0.75f, 0.75f);

//    void Awake ()
//    {
//        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
//
//        if (generateLevel)
//        {
//            //Set array
//            grid = new GameObject[sizeX, sizeZ];
//
//            initSizeX = sizeX;
//            initSizeZ = sizeZ;
//
//            gridFolder = GameObject.FindGameObjectWithTag("GridFolder").transform;
//
//            GenerateGrid();
//        }
//        else
//        {
//            LoadGrid();
//        }
//    }

    void LoadGrid()
    {
        grid = new GameObject[sizeX, sizeZ];

        for (int z = 0; z < sizeZ; z++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                GameObject curTile = gameObject.transform.GetChild(counter).gameObject;
				curTile.tag = "Tile";

				if (curTile.name == "StartTile")
				{
					AvatarButton avatarButton = GameObject.Find ("Avatar Button").GetComponent<AvatarButton> ();
					avatarButton.SpawnAvatar (curTile);
					avatarButton.initAvatarLocation = curTile;
					TileBehaviour.avatarLocation = curTile;
				}

                grid[x, z] = curTile;

				if (curTile.transform.childCount > 0)
				{
					foreach (Transform orb in curTile.transform)
					{
						gm.allOrbs.Add (orb.gameObject);


                        //if(orb.GetComponent<Orb>().orbColor == OrbColor.red)
                        //{
                        //    gm.redOrbsTotal++;
                        //}
                        //if (orb.GetComponent<Orb>().orbColor == OrbColor.blue)
                        //{
                        //    gm.blueOrbsTotal++;
                        //}
                        //if (orb.GetComponent<Orb>().orbColor == OrbColor.yellow)
                        //{
                        //    gm.yellowOrbsTotal++;
                        //}
					}
				}

                counter++;

                //gm.UpdateOrbsLeft ();
            }
        }
    }

    public void GenerateGrid()
    {
		//Set array
		grid = new GameObject[sizeX, sizeZ];

		initSizeX = sizeX;
		initSizeZ = sizeZ;

		gridFolder = GameObject.FindGameObjectWithTag("GridFolder").transform;

        totalTiles = sizeX * sizeZ;
        tilesPerRow = sizeX;
        sizeZ = -1;

        for (tilesCreated = 0; tilesCreated < totalTiles; tilesCreated++)
        {
            sizeX += 1;

            if (tilesCreated % tilesPerRow == 0)
            {
                sizeZ += 1;
                sizeX = 0;
            }

            //Create new Node
			GameObject tile = Instantiate(tiles[Random.Range(0,6)], new Vector3(sizeX, 0 , sizeZ), tilePrefab.transform.rotation);
            //tile.transform.localEulerAngles = tileRotation;
            tile.transform.localEulerAngles = new Vector3(0,45,0);
            tile.transform.SetParent(gridFolder);

            tile.tag = "Tile";
            tile.GetComponent<TileBehaviour>().row = sizeX;
            tile.GetComponent<TileBehaviour>().col = sizeZ;
            tile.name = "Tile Location:  " + "X: " + sizeX + "  Z: " + sizeZ;

            //Add node to 2d array
            grid[sizeX, sizeZ] = tile;
        }

        sizeX = initSizeX;
        sizeZ = initSizeZ;
    }
}
