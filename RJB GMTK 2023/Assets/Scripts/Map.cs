using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //references to tiles
    public GameObject[] prefabs;
    public MapTile[,] mapTiles;
    public List<MapTile> TilesOnFire;

    List<Vector2> neighboursToCatchFire;
    List<Vector2> spreadableNeighbours;

    //grid spawning variables
    public float spacing, randomisedPositionRange, fireTimer, spreadTime;
    public int gridX, gridY, randomPathX1, randomPathX2, randomPathY1, randomPathY2, itemIndex;

    void Start()
    {
        //instantiate the 3D array to hold all of the map tiles and the list to hold the tiles on fire
        mapTiles = new MapTile[gridX,gridY];
        TilesOnFire = new List<MapTile>();

        //generate a random position for a path on the X and Y axis
        randomPathX1 = Random.Range(1, gridX - 2);
        randomPathX2 = Random.Range(1, gridX - 2);
        randomPathY1 = Random.Range(1, gridY - 2);
        randomPathY2 = Random.Range(1, gridY - 2);

        //spawn the tiles on the grid
        SpawnGrid();

        //Set the fire spread timer
        spreadTime = 2f;
    }

    void FixedUpdate()
    {
        fireTimer = fireTimer - Time.deltaTime;

        if (fireTimer < 0)
            SpreadFire();
    }

    void Update()
    {
        if (GameController.Instance._phase == GameController.Phase.WaterPhase && TilesOnFire.Count == 0)
            GameController.Instance.TimerScoreController.TimerEnded();
    }

    //Spawn a grid of a given size and loop through each cell, spawning a random object at each
    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                //generate a spawn position in the grid based on the position in the loop and the spacing between tiles on the grid
                Vector3 spawnPosition = new Vector3(x * spacing, y * spacing, 0);

                //if we aren't at any of the path coordinates, then we want the item index to be above 0 and the position to be randomised. Otherwise we set item index to 0 to spawn a path
                if (x != (gridX / 2) && y != (gridY / 2) && x != randomPathX1 && x != randomPathX2 && y != randomPathY1 && y != randomPathY2)
                {
                    itemIndex = Random.Range(1, prefabs.Length);
                    spawnPosition = RandomizePosition(spawnPosition);
                }
                else
                {
                    itemIndex = 0;
                }

                //then spawn an object. If the spawn position is in any of the paths, spawn a path prefab instead
                SpawnTile(spawnPosition, Quaternion.identity, x, y, itemIndex);
            }
        }
    }

    //Take a position and rotation to instantiate a random object from the objects array, then add it to the 3D map tiles array
    void SpawnTile(Vector3 _spawnPosition, Quaternion _spawnRotation, int _x, int _y, int _itemIndex)
    {
        GameObject spawnedObject = Instantiate(prefabs[_itemIndex], _spawnPosition, _spawnRotation);

        //once the object is spawned, add it to the map tiles 3D array and set it as a child item of this object
        mapTiles[_x, _y] = spawnedObject.GetComponent<MapTile>();
        mapTiles[_x, _y].transform.SetParent(this.transform);
        mapTiles[_x, _y].posX = _x;
        mapTiles[_x, _y].posY = _y;
    }

    //Randomise a position by a small offset to make the map more life like
    Vector3 RandomizePosition(Vector3 _position)
    {
        Vector3 randomizedPosition = new Vector3(Random.Range(-randomisedPositionRange, randomisedPositionRange), Random.Range(-randomisedPositionRange, randomisedPositionRange), 0) + _position;

        return randomizedPosition;
    }

    //Spread Fire once the timer runs out, depending on a random chance
    void SpreadFire()
    {
        Debug.Log("Spreading Fire");
        //firstly, set the timer back to the spread time
        fireTimer = spreadTime;

        //for each tile that is currently on fire, roll a random number with a 50/50 chance
        for(int j = 0; j < TilesOnFire.Count; j++)
        {
            if(Random.Range(0, 10) <= 2)
            {
                //if the fire will spread, grab a list of all neighbours that fire could spread to
                spreadableNeighbours = GetNeighbours(new Vector2(TilesOnFire[j].posX, TilesOnFire[j].posY));

                //roll another random number to check how many neighbours should be spread to
                int tilesToSpreadTo = Random.Range(0, spreadableNeighbours.Count);

                //create a list to store which neighbours will catch fire
                neighboursToCatchFire = new List<Vector2>();

                switch (tilesToSpreadTo)
                {
                    case 1:
                    case 4:
                        {
                            //if there is only one tile to spread fire to, randomly choose a tile from the possible neighbours
                            neighboursToCatchFire.Add(spreadableNeighbours[(Random.Range(0, spreadableNeighbours.Count))]);
                        }
                        break;
                    case 2:
                        {
                            //if there are two tiles to spread to, generate the first one randomly and set the second to the same value
                            int firstTile = Random.Range(0, spreadableNeighbours.Count);
                            int secondTile = firstTile;

                            //then while the second value is the same as the first one, make a new value. this should guarantee two unique tiles added to the list
                            while (secondTile == firstTile)
                            {
                                secondTile = Random.Range(0, spreadableNeighbours.Count);
                            }

                            //finally add the two tiles to the list
                            neighboursToCatchFire.Add(spreadableNeighbours[firstTile]);
                            neighboursToCatchFire.Add(spreadableNeighbours[secondTile]);
                        }
                        break;
                    case 3:
                        {
                            //if there are three tiles to spread to, generate the first one randomly and set the second and third to the same value
                            int firstTile = Random.Range(0, spreadableNeighbours.Count);
                            int secondTile = firstTile;
                            int thirdTile = firstTile;

                            //then while the second value is the same as the first one, make a new value. this should guarantee two unique tiles
                            while (secondTile == firstTile)
                            {
                                secondTile = Random.Range(0, spreadableNeighbours.Count);
                            }

                            //then repeat with the third tile, this time running the loop while it's equal to either the first or second tile
                            while (thirdTile == firstTile || thirdTile == secondTile)
                            {
                                thirdTile = Random.Range(0, spreadableNeighbours.Count);
                            }

                            //finally add the three tiles to the list
                            neighboursToCatchFire.Add(spreadableNeighbours[firstTile]);
                            neighboursToCatchFire.Add(spreadableNeighbours[secondTile]);
                            neighboursToCatchFire.Add(spreadableNeighbours[thirdTile]);
                        }
                        break;
                    /*case 4:
                        {
                            //if there are four tiles to spread to, that means that all possible available neighbours need to catch fire, so add them all to the list
                            neighboursToCatchFire = spreadableNeighbours;
                        }
                        break;*/
                }

                //now we have the list of neighbours to set fire to, loop through the list
                foreach (Vector2 tilePosition in neighboursToCatchFire)
                {
                    Debug.Log("Setting New Fire");
                    mapTiles[(int)tilePosition.x, (int)tilePosition.y].SetAblaze(false);
                }
            }
        }

    }

    //returns all possible neighbours that can be set on fire for a given tile
    public List<Vector2> GetNeighbours(Vector2 _tilePosition)
    {
        List<Vector2> SpreadableNeighbours = new List<Vector2>();

        //for each adjacent tile, check that it is within the grid and the tile isn't a path or already on fire. If it fits the criteria, add it to the Spreadable Neighbours list
        if (_tilePosition.x + 1 < gridX - 1 && mapTiles[(int)_tilePosition.x + 1, (int)_tilePosition.y].path != true && mapTiles[(int)_tilePosition.x + 1, (int)_tilePosition.y].onFire != true)
            SpreadableNeighbours.Add(new Vector2(_tilePosition.x + 1, _tilePosition.y));

        if (_tilePosition.y + 1 < gridY - 1 && mapTiles[(int)_tilePosition.x, (int)_tilePosition.y + 1].path != true && mapTiles[(int)_tilePosition.x, (int)_tilePosition.y + 1].onFire != true)
            SpreadableNeighbours.Add(new Vector2(_tilePosition.x, _tilePosition.y + 1));

        if (_tilePosition.x - 1 > 0 && mapTiles[(int)_tilePosition.x - 1, (int)_tilePosition.y].path != true && mapTiles[(int)_tilePosition.x - 1, (int)_tilePosition.y].onFire != true)
            SpreadableNeighbours.Add(new Vector2(_tilePosition.x - 1, _tilePosition.y));

        if (_tilePosition.y - 1 > 0 && mapTiles[(int)_tilePosition.x, (int)_tilePosition.y - 1].path != true && mapTiles[(int)_tilePosition.x, (int)_tilePosition.y - 1].onFire != true)
            SpreadableNeighbours.Add(new Vector2(_tilePosition.x, _tilePosition.y - 1));

        //once each of the tiles has been checked, return the spreadable neighbours list
        return SpreadableNeighbours;
    }

    //Getters
    public float GridWidth()
    {
        return gridX * spacing;
    }

    public float GridHeight()
    {
        return gridY * spacing;
    }

    public Vector3 CenterPosition()
    {
        return new Vector3((GridWidth() / 2) - (spacing / 2), (GridHeight() / 2) - (spacing / 2), - 1);
    }
}
