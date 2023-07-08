using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //references to tiles
    public GameObject[] prefabs;
    public MapTile[,] mapTiles;

    //grid spawning variables
    public float spacing, randomisedPositionRange;
    public int gridX, gridY, randomPathX, randomPathY;

    void Start()
    {
        mapTiles = new MapTile[gridX,gridY];

        randomPathX = Random.Range(1, gridX - 2);
        randomPathY = Random.Range(1, gridY - 2);

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

                //then spawn an object if the position isn't in the middle of either row or the random paths to give the map some separation
                if (x != (gridX / 2) && y != (gridY / 2) && x != randomPathX && y != randomPathY)
                    SpawnObject(RandomizePosition(spawnPosition), Quaternion.identity, x, y);
            }
        }
    }

    //Take a position and rotation to instantiate a random object from the objects array, then add it to the 3D map tiles array
    void SpawnObject(Vector3 _spawnPosition, Quaternion _spawnRotation, int _x, int _y)
    {
        //generate a random number to determine which prefab is spawned and spawn it
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject spawnedObject = Instantiate(prefabs[randomIndex], _spawnPosition, _spawnRotation);

        //once the object is spawned, add it to the map tiles 3D array and set it as a child item of this object
        mapTiles[_x, _y] = spawnedObject.GetComponent<MapTile>();
        mapTiles[_x, _y].transform.SetParent(this.transform);
    }

    Vector3 RandomizePosition(Vector3 _position)
    {
        Vector3 randomizedPosition = new Vector3(Random.Range(-randomisedPositionRange, randomisedPositionRange), Random.Range(-randomisedPositionRange, randomisedPositionRange), 0) + _position;

        return randomizedPosition;
    }
}
