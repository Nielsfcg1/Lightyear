using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBehaviour : MonoBehaviour
{
    [Header("Grid Location")]
    public int row; //Horizontal
    public int col; //Vertical

    Grid gridFolder;
	GridNew gridNew;

    public static int avatarLocationRow;
    public static int avatarLocationCol;
    public static GameObject avatarLocation; 

    [Header("Orb Information")]
    public List<GameObject> orbs = new List<GameObject>();

    GameObject north;
    GameObject east;
    GameObject south;
    GameObject west;
	[HideInInspector] public GameObject tileToCheck;

    //Script references
    GameManager gm;
	AvatarButton avatarButton;

	public int index;

	ChooseOrb chooseOrb;
	AvatarMovement avatarMovement;

	[HideInInspector] public GameObject targetLocation;

	[HideInInspector] public static bool canClick = true;

	[HideInInspector] public bool wrongColor; 

    void Start()
    {
        gridFolder = GameObject.FindGameObjectWithTag("GridFolder").GetComponent<Grid>();
		//gridNew = transform.parent.GetComponent<GridNew> ();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		avatarButton = GameObject.Find ("Avatar Button").GetComponent<AvatarButton> ();
		chooseOrb = GameObject.Find ("Choose Orb Screen").GetComponent<ChooseOrb> ();
		avatarMovement = avatarButton.avatar.GetComponent<AvatarMovement> ();
    }

    //Execute behaviour when clicked
	//When the player clicks a tile, we check if we can place the avatar or if the tile has any orbs that can be picked up.
    public void OnMouseDown()
    {
        //If the popupscreen is not active
		if (canClick)
        {
            //If the avatar has not been placed, place avatar. Else, check if the avatar can jump to its target tile
	        if (avatarButton.avatarButtonState == AvatarButtonState.active)
            {
                //Check if this is a legit starting point (must atleast can jump over one tile)
                if(CheckNeighbours(gameObject) == true)
                {
			        //Set the avatar to the position of this tile
					avatarButton.SpawnAvatar (gameObject);

                    //Set avatar location
                    avatarLocation = gameObject;
                }
                else
                {
                    Debug.LogError("The avatar has no possible starting direction! Please choose another location");
                }
            }
	        else if (avatarButton.avatarButtonState == AvatarButtonState.reset)
            {
                //Check if the avatar can jump to this node
                targetLocation = gameObject;
                tileToCheck = null;
				wrongColor = false;

                //Calculate directions
                int directionX = targetLocation.GetComponent<TileBehaviour>().row - avatarLocation.GetComponent<TileBehaviour>().row;
                int directionZ = targetLocation.GetComponent<TileBehaviour>().col - avatarLocation.GetComponent<TileBehaviour>().col;

                if (Mathf.Abs(directionX) == 2 && Mathf.Abs(directionZ) == 0 || Mathf.Abs(directionX) == 0 && Mathf.Abs(directionZ) == 2)
                {
                    //Check correct tile
					tileToCheck = gridFolder.grid[avatarLocation.GetComponent<TileBehaviour>().row + directionX / 2, avatarLocation.GetComponent<TileBehaviour>().col + directionZ / 2];
                }
				else
				{
					//Debug.LogError("You cannot walk in that direction! You can only walk in straight lines. OR: This action cannot be completed.");
				}              

				//If there is no tile to check
				if (tileToCheck == null)
				{
					//Debug.LogError("Invalid action!");
				}

                //If the tile has more than one orb
                else if (tileToCheck.GetComponent<TileBehaviour>().orbs.Count >= 1)
                {
                    CheckOrbs();
                }	
            }
        }
    }

	//When we know that the tile contains orb, we must check what color we have so we can pick up an orb
    public void CheckOrbs()
    {
        int activeOrbsOnTile = 0;
        for (int i = 0; i < tileToCheck.transform.childCount; i++)
        {
			GameObject orb = tileToCheck.transform.GetChild(i).gameObject;

            if (orb.activeInHierarchy)
            {
				if (gm.currentColorINT == orb.GetComponent<Orb> ().orbType)
				{
					wrongColor = false;
					index = i;
					MoveTowardsTile (targetLocation, orb.GetComponent<Orb> ());
					break;
				}
				else if (gm.currentColorINT != -1 && orb.GetComponent<Orb> ().orbType != gm.currentColorINT)
				{
					wrongColor = true;
				}
                else if (gm.currentColorINT == -1)
                {
                    index = i;
                    activeOrbsOnTile++;

					chooseOrb.orbs.Add (orb.transform);
                }
            }
        }

        if (activeOrbsOnTile > 1 && gm.currentColorINT == -1)
        {
            chooseOrb.ShowPopUp(this);
        }
        else if (activeOrbsOnTile == 1 && gm.currentColorINT == -1)
        {
			chooseOrb.ClearOrbColors ();
			gm.currentColorINT = tileToCheck.transform.GetChild(index).GetComponent<Orb> ().orbType;
			MoveTowardsTile(targetLocation, tileToCheck.transform.GetChild(index).GetComponent<Orb> ());
        }
    }

    //Move the avatar to its new location
	public void MoveTowardsTile(GameObject targetLocation, MonoBehaviour orbScript)
    {
        //Set new position of the avatar
        avatarLocation = targetLocation;

		//Let the avatar movement script now to what tile is has to move, and which orb it has to disable
		avatarMovement.index = index;
		avatarMovement.tileWithOrb = tileToCheck;
		avatarMovement.path.Add (targetLocation.transform);

		//Update movecounter
		gm.moves++;
		gm.UpdateMoveCounter();

		index = 0;
    }

    //MAYBE USE A PART OF THIS CODE FOR THE LEVEL SOLVER?
    bool CheckNeighbours(GameObject node)
    {
        int nRow = node.GetComponent<TileBehaviour>().row;
        int nCol = node.GetComponent<TileBehaviour>().col;

        ////Check all neighbours of clicked node
        //North
        if (nCol + 1 > gridFolder.sizeZ-1)
			north = null;
		else
			north = gridFolder.grid [nRow, nCol + 1];

        //East
		if (nRow + 1 > gridFolder.sizeX-1)
            east = null;
        else
			east = gridFolder.grid[nRow + 1, nCol];

        //South
        if (nCol - 1 < 0)
            south = null;
        else
			south = gridFolder.grid[nRow, nCol - 1];

        //West
        if (nRow - 1 < 0)
            west = null;
        else
			west = gridFolder.grid[nRow - 1, nCol];

        //Check if a node has orbs
        if (north != null)
        {
            if (north.GetComponent<TileBehaviour>().orbs.Count > 0)
            {
                return true;
            }
        }

        if (east != null)
        {
            if (east.GetComponent<TileBehaviour>().orbs.Count > 0)
            {
                return true;
            }
        }

        if (south != null)
        {
            if (south.GetComponent<TileBehaviour>().orbs.Count > 0)
            {
                return true;
            }
        }

        if (west != null)
        {
            if (west.GetComponent<TileBehaviour>().orbs.Count > 0)
            {
                return true;
            }
        }

        return false;
    }
}
