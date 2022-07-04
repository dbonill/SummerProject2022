using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject ObjectParent;

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
    public TextMeshProUGUI ScoreText;
    public int CurrentScore = 0;
    public float timeSurvived = 0f;

    [Header("Difficulty")]
    public int everyXScoreMakeGameHarder = 10;
    public float makeGameHarderByXTime = 1f;
    public float speedMultiplyerBonus = 1f;
    public float addToSpeedBonus = 0.3f;


    [Header("Start/End Game")]
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject gameOverText;
    public GameObject nameOfMiniGame;
    public string LoadSceneOnGameOver;


    //working on this rn
    [Header("Cloud/Boulder Spawns")]
    public List<GameObject> Clouds;
    public List<GameObject> Boulders;
    public List<Transform> CloudSpawnPos; //2 positions only one for bottom and top range
    public Transform BoulderSpawn; //its just one position
    public Transform CloudUIParent;
    public Transform BoulderUIParent;
    public float spawnCloudIntervalStart = 2;
    public float spawnCloudIntervalEnd = 3;
    private float nextTimeToSpawnCloud = 3;

    public float spawnBoulderIntervalStart = 2;
    public float spawnBoulderIntervalEnd = 3;
    private float nextTimeToSpawnBoulder = 3;

    public void spawnCloud()
    {
        int indexOfCloud = Random.Range(0, Clouds.Count);
        float newY = Random.Range(CloudSpawnPos[0].position.y, CloudSpawnPos[1].position.y);
        Vector3 posToSpawn = new Vector3(CloudSpawnPos[0].position.x, newY, 0);

        //int flipX = Random.Range(0, 2);


        var cloud = Instantiate(Clouds[indexOfCloud], posToSpawn, Quaternion.identity);

        /*
         * just going to make extra prefabs that have them fliped already
         * 
        if (flipX > 0)
        {
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * -1, cloud.transform.localScale.y, cloud.transform.localScale.z);
        }
        */

        cloud.transform.SetParent(CloudUIParent);
        cloud.transform.localScale = new Vector3(1, 1, 1);

        nextTimeToSpawnCloud = Random.Range(spawnCloudIntervalStart, spawnCloudIntervalEnd);
    }

    public void spawnBoulder()
    {
        int indexOfBoulder = Random.Range(0, Boulders.Count);
        //float newY = Random.Range(CloudSpawnPos[0].position.y, CloudSpawnPos[1].position.y);
        //Vector3 posToSpawn = new Vector3(CloudSpawnPos[0].position.x, newY, 0);

        //int flipX = Random.Range(0, 2);


        var boulder = Instantiate(Boulders[indexOfBoulder], BoulderSpawn.position, Quaternion.identity);

        /*
         * just going to make extra prefabs that have them fliped already
         * 
        if (flipX > 0)
        {
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * -1, cloud.transform.localScale.y, cloud.transform.localScale.z);
        }
        */

        boulder.transform.SetParent(BoulderUIParent);
        boulder.transform.localScale = new Vector3(1, 1, 1);
        nextTimeToSpawnBoulder = Random.Range(spawnBoulderIntervalStart, spawnBoulderIntervalEnd);
    }

    public void BackGroundObjectSpawner()
    {
        if (startGame)
        {
            //do boulder spawns
            if (nextTimeToSpawnBoulder > 0)
                nextTimeToSpawnBoulder -= Time.deltaTime;
            else
                spawnBoulder();
        }

        //cloud spawns are always on

        if (nextTimeToSpawnCloud > 0)
            nextTimeToSpawnCloud -= Time.deltaTime;
        else
            spawnCloud();

    }




    //Start/End/Reset Game
    public bool startGame = false;

    public void StartGame()
    {
        Time.timeScale = 1f;
        startButton.SetActive(false);
        nameOfMiniGame.SetActive(false);
        startGame = true;
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        restartButton.SetActive(true);
        gameOverText.SetActive(true);
        GameOver = true;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(LoadSceneOnGameOver);
    }

    //Save System

    //Difficulty
    void MakeGameHarder()
    {
        if (CurrentScore % everyXScoreMakeGameHarder == 0 && timeSurvived > endSpawnObstacle)
        {
            if (endSpawnObstacle - makeGameHarderByXTime >= startSpawnObstacle)
            {
                endSpawnObstacle = endSpawnObstacle - makeGameHarderByXTime;
                speedMultiplyerBonus += addToSpeedBonus;
            }
        }
    }

    //Spawn Obstacle Functions
    void ObstacleSpawner()
    {
        spawnObstacleTimer = Random.Range(startSpawnObstacle, endSpawnObstacle);
        //Instantiate here
        int indexToSpawn = Random.Range(0, ObstaclesToSpawn.Count);
        if (ObstaclesToSpawn[indexToSpawn].startHigh)
        {
            var go = Instantiate(ObstaclesToSpawn[indexToSpawn].Obstacle, SpawnPoints[1].position, Quaternion.identity);
            go.GetComponent<MovementSystem>().speed = ObstaclesToSpawn[indexToSpawn].Speed * speedMultiplyerBonus;
        }
        else
        {
            var go = Instantiate(ObstaclesToSpawn[indexToSpawn].Obstacle, SpawnPoints[0].position, Quaternion.identity);
            go.GetComponent<MovementSystem>().speed = ObstaclesToSpawn[indexToSpawn].Speed * speedMultiplyerBonus;
            go.transform.SetParent(ObjectParent.transform);
        }

    }

    void ObstacleSpawnTimers()
    {
        if (GameOver)
            return;
        if (spawnObstacleTimer <= 0)
        {
            //batch spawns?
            ObstacleSpawner();
            MakeGameHarder();
        }
        else
        {
            spawnObstacleTimer -= Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        spawnObstacleTimer = startSpawnObstacle;

        //make it where boulder and clouds spawn

        //Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Time Alive
        if (!GameOver && startGame) //need to add pause
        {
            timeSurvived += Time.deltaTime;
            ObstacleSpawnTimers();
        }

        BackGroundObjectSpawner();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            CurrentScore++;
            ScoreText.text = "SCORE: " + CurrentScore;
        }
    }


}
