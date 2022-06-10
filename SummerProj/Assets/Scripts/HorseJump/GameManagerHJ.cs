using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Obstacles
{
    public GameObject Obstacle;
    public bool startHigh;
    public float Speed;
}


public class GameManagerHJ : MonoBehaviour
{
    public bool GameOver = false;

    [Header("Obstacles")]
    public float startSpawnObstacle = 1f;
    public float endSpawnObstacle = 3f;
    float spawnObstacleTimer = 1f;
    public List<Obstacles> ObstaclesToSpawn;

    [Header("Items")]
    public float startSpawnItem = 25f;
    public float endSpawnItem = 33f;
    float spawnItemTimer = 25f;
    public List<GameObject> ItemsToSpawn;

    [Header("SpawnPoints")]
    public List<Transform> SpawnPoints;

    [Header("Score")]
    public int CurrentScore = 0;


    //Save System

    //Spawn Obstacle Functions
    void ObstacleSpawner()
    {
        spawnObstacleTimer = Random.Range(startSpawnObstacle, endSpawnObstacle);
        //Instantiate here
        int indexToSpawn = Random.Range(0, ObstaclesToSpawn.Count);
        if (ObstaclesToSpawn[indexToSpawn].startHigh)
        {
            var go = Instantiate(ObstaclesToSpawn[indexToSpawn].Obstacle, SpawnPoints[1].position, Quaternion.identity);
            go.GetComponent<MovementSystem>().speed = ObstaclesToSpawn[indexToSpawn].Speed;
        }
        else
        {
            var go = Instantiate(ObstaclesToSpawn[indexToSpawn].Obstacle, SpawnPoints[0].position, Quaternion.identity);
            go.GetComponent<MovementSystem>().speed = ObstaclesToSpawn[indexToSpawn].Speed;
        }



    }

    void ObstacleSpawnTimers()
    {
        if (GameOver)
            return;
        if (spawnObstacleTimer <= 0)
        {
            ObstacleSpawner();
        }
        else
        {
            spawnObstacleTimer -= Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleSpawnTimers();
    }
}
