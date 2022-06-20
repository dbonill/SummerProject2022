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
