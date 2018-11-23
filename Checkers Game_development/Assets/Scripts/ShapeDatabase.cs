using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDatabase : MonoBehaviour {

    void Start()
    {

    }

    //public List<char> possibleDirections = new List<char>();

    //Minimum orbs: 4
    public List<char> DrawCircle(int orbs, GameObject _currentTile)
    {
        //print("CurrentTile: " + _currentTile.name);

        //Reference of the level generator
        LevelGenerator lg = GetComponent<LevelGenerator>();

        /*
            Different directions:
            1: NESW
            2: NWSE
            3: SENW
            4: SWNE
        */

        //Create list of the steps to create a circle
        List<char> circle = new List<char>();

        //Set Variables
        float vLength = 0;
        float hLength = 0;

        //Check if the amount of orbs is dividable by 4. This will be a perfect circle
        if (orbs % 4 == 0)
        {
            vLength = orbs / 2;
            hLength = orbs / 2;
        }
        //Check if the orb amount is an even number. The length of the circle will be a little longer than the heigth
        else if (orbs % 2 == 0)
        {
            vLength = orbs / 2 - 1;
            hLength = orbs / 2 + 1;
        }
        //If the amount of orbs is uneven there is no possible way that you get a round circle
        else
        {
            Debug.LogError("A circle is not possible with an uneven number!");
        }

        //Check length grid, check currentTile, check if it is possible
        GameObject cTile = _currentTile;
        int lengthGrid = lg.gridFolder.sizeX;
        int heightGrid = lg.gridFolder.sizeZ;

        //print("CurrentTile: " + cTile.name);

        //Check the space left between the edge of the map and the current tile
        int spaceNorth = (heightGrid - cTile.GetComponent<TileBehaviour>().col);
        int spaceEast = (lengthGrid - cTile.GetComponent<TileBehaviour>().row);
        int spaceSouth = cTile.GetComponent<TileBehaviour>().col + 1;
        int spaceWest = cTile.GetComponent<TileBehaviour>().row + 1;

        //Temporary list in which the possibleDirections will be stored
        List<int> possibleDirections = new List<int>();

        if (spaceNorth > vLength && spaceEast > hLength)
        {
            possibleDirections.Add(1);
        }
        if (spaceNorth > vLength && spaceWest > hLength)
        {
            possibleDirections.Add(2);
        }
        if (spaceSouth > vLength && spaceEast > hLength)
        {
            possibleDirections.Add(3);
        }
        if (spaceSouth > vLength && spaceWest > hLength)
        {
            possibleDirections.Add(4);
        }

        //Set a random possible direction
        int direction = 0;

        if (possibleDirections.Count != 0)
        {
            direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
        }
        else
        {
            Debug.LogError("There are no possible ways that this circle can be inserted in the level!");
            lg.allLevelsPossible = false;
        }

        for (int i = 0; i < vLength / 2; i++)
        {
            if (direction == 1 || direction == 2)
                circle.Add('N');
            else if (direction == 3 || direction == 4)
                circle.Add('S');
        }

        for (int i = 0; i < hLength / 2; i++)
        {
            if (direction == 1 || direction == 3)
                circle.Add('E');
            else if (direction == 2 || direction == 4)
                circle.Add('W');
        }

        for (int i = 0; i < vLength / 2; i++)
        {
            if (direction == 1 || direction == 2)
                circle.Add('S');
            else if (direction == 3 || direction == 4)
                circle.Add('N');
        }

        for (int i = 0; i < hLength / 2; i++)
        {
            if (direction == 1 || direction == 3)
                circle.Add('W');
            else if (direction == 2 || direction == 4)
                circle.Add('E');
        }

        CheckShapeContents(circle);

        return circle;
    }

    //Minimum orbs: 6   INFO: _currentTile must have an X of > 1 && X < gridsize - 1. Same for Z.
    public List<char> DrawLoop(int orbs, GameObject _currentTile)
    {
        //Reference of the level generator
        LevelGenerator lg = GetComponent<LevelGenerator>();

        //print("CurrentTile: " + _currentTile.name);

        /*
            Different directions: (maybe its possible to go E/W first and then N/S?)
            1: NNESWW
            2: NNWSEE
            3: SSENWW
            4: SSWNEE
            5: EENWSS
            6: EESWNN
            7: WWSENN
            8: WWNESS
        */

        //Create list of the steps to create a loop
        List<char> loop = new List<char>();

        //Set variables
        float vLength = 0;
        float hLength = 0;

        //Check if the amount of orbs is dividable by 6. This will be a perfect loop
        if (orbs % 6 == 0)
        {
            vLength = orbs / 3 * 2;
            hLength = orbs / 3 * 2;

            //print(vLength);
            //print(hLength);
        }
        //Check if the orb amount is an even number. The length of the loop will be a little longer than the heigth
        else if (orbs % 2 == 0)
        {
            print("Not possible (yet). Try a number which is dividable by 6 (12,18,24 etc).");
            //vLength = orbs / 2 - 1;
            //hLength = orbs / 2 + 1;
        }
        //If the amount of orbs is uneven there is no possible way that you get a perfect loop
        else
        {
            Debug.LogError("A loop is not yet possible with an uneven number!");
        }

        //Check length grid, check currentTile, check if it is possible
        GameObject cTile = _currentTile;
        int lengthGrid = lg.gridFolder.sizeX;
        int heightGrid = lg.gridFolder.sizeZ;

        //print("CurrentTile " + cTile.name);

        //Check the space left between the edge of the map and the current tile
        int spaceNorth = (heightGrid - cTile.GetComponent<TileBehaviour>().col);
        int spaceEast = (lengthGrid - cTile.GetComponent<TileBehaviour>().row);
        int spaceSouth = cTile.GetComponent<TileBehaviour>().col + 1;
        int spaceWest = cTile.GetComponent<TileBehaviour>().row + 1;

        //print("SN " + spaceNorth + " SE " + spaceEast + " SS " + spaceSouth + " SW " + spaceWest);

        //Temporary list in which the possibleDirections will be stored
        List<int> possibleDirections = new List<int>();

        //1st one must be bigger than 4, second two bigger than 3?
        if (spaceNorth > vLength && spaceEast > hLength - 2 && spaceWest > hLength - 2)           //hLength = 4  vLength = 4
        {
            possibleDirections.Add(1);
        }
        if (spaceNorth > vLength && spaceWest > hLength - 2 && spaceEast > hLength - 2)
        {
            possibleDirections.Add(2);
        }
        if (spaceSouth > vLength && spaceEast > hLength - 2 && spaceWest > hLength - 2)
        {
            possibleDirections.Add(3);
        }
        if (spaceSouth > vLength && spaceWest > hLength - 2 && spaceEast > hLength - 2)
        {
            possibleDirections.Add(4);
        }
        if (spaceEast > vLength && spaceNorth > hLength - 2 && spaceSouth > hLength - 2)
        {
            possibleDirections.Add(5);
        }
        if (spaceEast > vLength && spaceSouth > hLength - 2 && spaceNorth > hLength - 2)
        {
            possibleDirections.Add(6);
        }
        if (spaceWest > vLength && spaceNorth > hLength - 2 && spaceSouth > hLength - 2)
        {
            possibleDirections.Add(7);
        }
        if (spaceWest > vLength && spaceSouth > hLength - 2 && spaceNorth > hLength - 2)
        {
            possibleDirections.Add(8);
        }

        //Set a random possible direction
        int direction = 0;

        if (possibleDirections.Count != 0)
        {
            direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
        }
        else
        {
            Debug.LogError("There are no possible ways that this loop can be inserted in the level!");
            lg.allLevelsPossible = false;
        }

        for (int i = 0; i < vLength / 2; i++)
        {
            if (direction == 1 || direction == 2)
                loop.Add('N');
            else if (direction == 3 || direction == 4)
                loop.Add('S');
            else if (direction == 5 || direction == 6)
                loop.Add('E');
            else if (direction == 7 || direction == 8)
                loop.Add('W');
        }

        for (int i = 0; i < hLength / 4; i++)
        {
            if (direction == 1 || direction == 3)
                loop.Add('E');
            else if (direction == 2 || direction == 4)
                loop.Add('W');
            else if (direction == 5 || direction == 7)
                loop.Add('N');
            else if (direction == 6 || direction == 8)
                loop.Add('S');
        }

        for (int i = 0; i < vLength / 4; i++)
        {
            if (direction == 1 || direction == 2)
                loop.Add('S');
            else if (direction == 3 || direction == 4)
                loop.Add('N');
            else if (direction == 5 || direction == 6)
                loop.Add('W');
            else if (direction == 7 || direction == 8)
                loop.Add('E');
        }

        for (int i = 0; i < hLength / 2; i++)
        {
            if (direction == 1 || direction == 3)
                loop.Add('W');
            else if (direction == 2 || direction == 4)
                loop.Add('E');
            else if (direction == 5 || direction == 7)
                loop.Add('S');
            else if (direction == 6 || direction == 8)
                loop.Add('N');
        }

        CheckShapeContents(loop);

        //Set new currenttile for the levelgenerator;
        SetNewCurrentTile(loop, cTile);

        return loop;
    }

    //Minimum orbs: 2    INFO: Currently goes NESW in a straight line, make it variable
    public List<char> DrawLine(int orbs, GameObject _currentTile, bool portal)
    {
        //Reference of the level generator
        LevelGenerator lg = GetComponent<LevelGenerator>();

        /*
            Different directions:
            1: N
            2: E
            3: S
            4: W
        */

        //Create list of the steps to create a circle
        List<char> line = new List<char>();

        /*
        Check starting tile. 
        Check space to each direction, choose the one with the most space. 
        when the line can't be continued, choose the second longest direction.
        Continue untill there are no orbs left

        */

        //Check length grid, check currentTile, check if it is possible
        GameObject cTile = _currentTile;
        int lengthGrid = lg.gridFolder.sizeX;
        int heightGrid = lg.gridFolder.sizeZ;

        //OrbCounter
        int orbCount = orbs;
        //print("CurrentTile: " + cTile.name);

        //Check the space left between the edge of the map and the current tile
        int spaceNorth = (heightGrid - cTile.GetComponent<TileBehaviour>().col);
        int spaceEast = (lengthGrid - cTile.GetComponent<TileBehaviour>().row);
        int spaceSouth = cTile.GetComponent<TileBehaviour>().col + 1;
        int spaceWest = cTile.GetComponent<TileBehaviour>().row + 1;



        //int direction = Random.Range(0, 4);
        //print("Init Direction " + direction);

        int spaceLeft = 0;
        char prevDir = 'X';

        //Temporary list in which the possibleDirections will be stored
        List<int> possibleDirections = new List<int>();

        possibleDirections.Add(1);
        possibleDirections.Add(2);
        possibleDirections.Add(3);
        possibleDirections.Add(4);
        //Set a random possible direction


        int direction = 0;

        if (possibleDirections.Count != 0)
        {
            direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
        }
        else
        {
            Debug.LogError("There are no possible ways that this circle can be inserted in the level!");
            lg.allLevelsPossible = false;
        }

        ////Shortened references
        //int nRow = cTile.GetComponent<TileBehaviour>().row;
        //int nCol = cTile.GetComponent<TileBehaviour>().col;

        GameObject newTile;
        GameObject _tile = cTile;
        //int spaceNorth = (heightGrid - cTile.GetComponent<TileBehaviour>().col);

        if(spaceEast > 2)
        {
            while (spaceNorth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('N');
                orbs--;
                spaceNorth -= 2;
                spaceSouth += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col + 2];
                //print(_tile);
            }

            while (spaceEast > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('E');
                orbs--;
                spaceEast -= 2;
                spaceWest += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + 2, _tile.GetComponent<TileBehaviour>().col];
                //print(_tile);
            }

            if (portal)
            {
                //Set that from here a portal must be spawned
                line.Add('P');
                portal = false;

                //Create new current tile to go further from (and check if it is possible to complete)
                int n = Mathf.FloorToInt(spaceNorth / 2) -1;
                int e = Mathf.FloorToInt(spaceEast / 2) -1;
                int s = -Mathf.FloorToInt(spaceSouth / 2) +1;
                int w = -Mathf.FloorToInt(spaceWest / 2) +1;

                //print(spaceNorth + " " + spaceEast);
                //print(spaceSouth + " " + spaceWest);

                int locH = 0;
                int locV = 0;

                //Shortened references
                int nRow = cTile.GetComponent<TileBehaviour>().row;
                int nCol = cTile.GetComponent<TileBehaviour>().col;

                //print(nRow + " " + nCol);
                //print(n + " " + e + " " + s + " " + w);


                while (locH == 0 || locV == 0)
                {
                    locH = Random.Range(w, e +1);
                    locV = Random.Range(n, s -1);
                }

                print(_tile);

                print("locH" + locH + "LocV" + locV);

                //print(_tile.GetComponent<TileBehaviour>().row + locH + " " + _tile.GetComponent<TileBehaviour>().col + locV);
                //print(lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + locH, _tile.GetComponent<TileBehaviour>().col + locV]);

                newTile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + (locH * 2), _tile.GetComponent<TileBehaviour>().col + (locV * 2)];

                print(newTile);

                //Check the space left between the edge of the map and the current tile
                spaceNorth = (heightGrid - newTile.GetComponent<TileBehaviour>().col);
                spaceEast = (lengthGrid - newTile.GetComponent<TileBehaviour>().row);
                spaceSouth = newTile.GetComponent<TileBehaviour>().col + 1;
                spaceWest = newTile.GetComponent<TileBehaviour>().row + 1;




                //newTile = lg.gridFolder.grid[Random.Range(2, lg.gridFolder.sizeX -2), Random.Range(2, lg.gridFolder.sizeZ -2)];
                //if (newTile == cTile)
                //{
                //    print("This shouldn't be possible....");
                //}

                //print("Old Current tile: " + cTile + " + New Current tile" + newTile);

                cTile = newTile;
                _tile = newTile;

                //Will only work with one poral per level (for testing purposes)
                lg.portalTile = newTile;

                //Calculate new space north, east, etc..



            }

            while (spaceSouth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }
                line.Add('S');
                orbs--;
                spaceSouth -= 2;
                spaceNorth += 2;

                //Bij alle mogelijkheden dit doen
                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col - 2];
            }

            while (spaceWest > 2)
            {
                if (orbs == 0)
                {
                    break;
                }
                line.Add('W');
                orbs--;
                spaceWest -= 2;
                spaceEast += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row - 2, _tile.GetComponent<TileBehaviour>().col];
            }

            while (spaceNorth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('N');
                orbs--;
                spaceNorth -= 2;
                spaceSouth += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col + 2];
            }

            while (spaceEast > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('E');
                orbs--;
                spaceEast -= 2;
                spaceWest += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + 2, _tile.GetComponent<TileBehaviour>().col];
            }
        }
        else
        {
            //print("Still needs functionality");

            while (spaceNorth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('N');
                orbs--;
                spaceNorth -= 2;
                spaceSouth += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col + 2];
                //print(_tile);
            }

            while (spaceWest > 2)
            {
                if (orbs == 0)
                {
                    break;
                }
                line.Add('W');
                orbs--;
                spaceWest -= 2;
                spaceEast += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row - 2, _tile.GetComponent<TileBehaviour>().col];
            }

            if (portal)
            {
                //Set that from here a portal must be spawned
                line.Add('P');
                portal = false;

                //Create new current tile to go further from (and check if it is possible to complete)
                int n = Mathf.FloorToInt(spaceNorth / 2) - 1;
                int e = Mathf.FloorToInt(spaceEast / 2) - 1;
                int s = -Mathf.FloorToInt(spaceSouth / 2) + 1;
                int w = -Mathf.FloorToInt(spaceWest / 2) + 1;

                print(spaceNorth + " " + spaceEast);
                print(spaceSouth + " " + spaceWest);

                int locH = 0;
                int locV = 0;

                //Shortened references
                int nRow = cTile.GetComponent<TileBehaviour>().row;
                int nCol = cTile.GetComponent<TileBehaviour>().col;

                print(nRow + " " + nCol);
                print(n + " " + e + " " + s + " " + w);


                while (locH == 0 || locV == 0)
                {
                    locH = Random.Range(w, e + 1);
                    locV = Random.Range(n, s - 1);
                }

                print(_tile);

                print("locH" + locH + "LocV" + locV);

                //print(_tile.GetComponent<TileBehaviour>().row + locH + " " + _tile.GetComponent<TileBehaviour>().col + locV);
                //print(lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + locH, _tile.GetComponent<TileBehaviour>().col + locV]);

                newTile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + (locH * 2), _tile.GetComponent<TileBehaviour>().col + (locV * 2)];

                print(newTile);

                //Check the space left between the edge of the map and the current tile
                spaceNorth = (heightGrid - newTile.GetComponent<TileBehaviour>().col);
                spaceEast = (lengthGrid - newTile.GetComponent<TileBehaviour>().row);
                spaceSouth = newTile.GetComponent<TileBehaviour>().col + 1;
                spaceWest = newTile.GetComponent<TileBehaviour>().row + 1;

                //newTile = lg.gridFolder.grid[Random.Range(2, lg.gridFolder.sizeX -2), Random.Range(2, lg.gridFolder.sizeZ -2)];
                //if (newTile == cTile)
                //{
                //    print("This shouldn't be possible....");
                //}

                //print("Old Current tile: " + cTile + " + New Current tile" + newTile);

                cTile = newTile;
                _tile = newTile;

                //Will only work with one poral per level (for testing purposes)
                lg.portalTile = newTile;

                //Calculate new space north, east, etc..



            }

            while (spaceSouth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }
                line.Add('S');
                orbs--;
                spaceSouth -= 2;
                spaceNorth += 2;

                //Bij alle mogelijkheden dit doen
                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col - 2];
            }

            while (spaceEast > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('E');
                orbs--;
                spaceEast -= 2;
                spaceWest += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row + 2, _tile.GetComponent<TileBehaviour>().col];
            }

            while (spaceNorth > 2)
            {
                if (orbs == 0)
                {
                    break;
                }

                line.Add('N');
                orbs--;
                spaceNorth -= 2;
                spaceSouth += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row, _tile.GetComponent<TileBehaviour>().col + 2];
            }

            while (spaceWest > 2)
            {
                if (orbs == 0)
                {
                    break;
                }
                line.Add('W');
                orbs--;
                spaceWest -= 2;
                spaceEast += 2;

                _tile = lg.gridFolder.grid[_tile.GetComponent<TileBehaviour>().row - 2, _tile.GetComponent<TileBehaviour>().col];
            }

            //while (spaceNorth > 2)
            //{
            //    if (orbs == 0)
            //    {
            //        break;
            //    }

            //    line.Add('N');
            //    orbs--;
            //    spaceNorth -= 2;
            //    spaceSouth += 2;
            //}

            //while (spaceWest > 2)
            //{
            //    if (orbs == 0)
            //    {
            //        break;
            //    }
            //    line.Add('W');
            //    orbs--;
            //    spaceWest -= 2;
            //    spaceEast += 2;
            //}

            //while (spaceSouth > 2)
            //{
            //    if (orbs == 0)
            //    {
            //        break;
            //    }
            //    line.Add('S');
            //    orbs--;
            //    spaceSouth -= 2;
            //    spaceNorth += 2;
            //}

            //while (spaceEast > 2)
            //{
            //    if (orbs == 0)
            //    {
            //        break;
            //    }

            //    line.Add('E');
            //    orbs--;
            //    spaceEast -= 2;
            //    spaceWest += 2;
            //}
        }



        //SetNewCurrentTile(line, _tile);
        lg.tileCheck = _tile;
        CheckShapeContents(line);

        return line;
    }

    //Print the contents of a shape list
    void CheckShapeContents(List<char> shape)
    {
        for (int i = 0; i < shape.Count; i++)
        {
            //print(shape[i]);
        }
    }

    //Will set a new current tile to use in the next shapes to check
    void SetNewCurrentTile(List<char> shape, GameObject _currentTile)
    {
        //Create variables for different directions
        int H = 0; int Z = 0;

        GameObject tila = _currentTile;

        Grid gridFolder = GameObject.FindGameObjectWithTag("GridFolder").GetComponent<Grid>();

        for (int i = 0; i < shape.Count; i++)
        {
            switch (shape[i])
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
            
            if(shape[i] == 'P')
            {
                continue;
            }

            //Shortened references
            int nRow = tila.GetComponent<TileBehaviour>().row;
            int nCol = tila.GetComponent<TileBehaviour>().col;

            //print("nRow " + (nRow + (H * 2)) + " nCol " + (nCol + Z * 2));

            tila = gridFolder.grid[nRow + H * 2, nCol + Z * 2].gameObject;
        }

     
        LevelGenerator lg = GetComponent<LevelGenerator>();
        lg.tileCheck = tila;
    }

    
}
