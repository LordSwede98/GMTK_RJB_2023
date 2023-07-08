using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spacing;

    public int gridX, gridY;

    void Start()
    {
        SpawnGrid();
    }

    void Update()
    {
        
    }

    //Spawn a grid of a given size and loop through each cell, spawning a random object at each
    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, y * spacing, 0);
                SpawnObject(spawnPosition, Quaternion.identity);
            }
        }
    }

    //Take a position and rotation to instantiate a random object from the objects array
    void SpawnObject(Vector3 _spawnPosition, Quaternion _spawnRotation)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject spawnedObject = Instantiate(prefabs[randomIndex], _spawnPosition, _spawnRotation);
    }
}
