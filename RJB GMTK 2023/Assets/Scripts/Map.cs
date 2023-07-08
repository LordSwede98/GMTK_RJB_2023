using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //references to tiles
    public GameObject[] prefabs;
    public GameObject path;
    public MapTile[,] mapTiles;

    //grid spawning variables
    public float spacing, randomisedPositionRange;
    public int gridX, gridY, randomPathX, randomPathY;

    void Start()
    {
        //instantiate the 3D array to hold all of the map tiles
        mapTiles = new MapTile[gridX,gridY];

        //generate a random position for a path on the X and Y axis
        randomPathX = Random.Range(1, gridX - 2);
        randomPathY = Random.Range(1, gridY - 2);

        //spawn the tiles on the grid
        SpawnGrid();
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

                //then spawn an object. If the spawn position is in ny of the paths, spawn a path prefab instead
                if (x != (gridX / 2) && y != (gridY / 2) && x != randomPathX && y != randomPathY)
                    SpawnBuilding(RandomizePosition(spawnPosition), Quaternion.identity, x, y);
                else
                {
                    SpawnPath(spawnPosition + new Vector3(0, 0, -0.4f), new Quaternion(-90, 0, 0, 0), x, y);
                }
            }
        }
    }

    //Take a position and rotation to instantiate a random object from the objects array, then add it to the 3D map tiles array
    void SpawnBuilding(Vector3 _spawnPosition, Quaternion _spawnRotation, int _x, int _y)
    {
        //generate a random number to determine which prefab is spawned and spawn it
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject spawnedObject = Instantiate(prefabs[randomIndex], _spawnPosition, _spawnRotation);

        //once the object is spawned, add it to the map tiles 3D array and set it as a child item of this object
        mapTiles[_x, _y] = spawnedObject.GetComponent<MapTile>();
        mapTiles[_x, _y].transform.SetParent(this.transform);
    }

    void SpawnPath(Vector3 _spawnPosition, Quaternion _spawnRotation, int _x, int _y)
    {
        GameObject spawnedObject = Instantiate(path, _spawnPosition, _spawnRotation);

        //once the path is spawned, add it to the map tiles 3D array and set it as a child item of this object and set path to true
        mapTiles[_x, _y] = spawnedObject.GetComponent<MapTile>();
        mapTiles[_x, _y].transform.SetParent(this.transform);
    }

    Vector3 RandomizePosition(Vector3 _position)
    {
        Vector3 randomizedPosition = new Vector3(Random.Range(-randomisedPositionRange, randomisedPositionRange), Random.Range(-randomisedPositionRange, randomisedPositionRange), 0) + _position;

        return randomizedPosition;
    }

    public float GridWidth()
    {
        return gridX * spacing;
    }

    public float GridHeight()
    {
        return gridY * spacing;
    }

    public Vector2 CenterPosition()
    {
        return new Vector2((GridWidth() / 2) - (spacing / 2), (GridHeight() / 2) - (spacing / 2));
    }
}
