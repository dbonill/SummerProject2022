using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSettings : MonoBehaviour
{
    public bool GameOver = false;
    public bool startTheGame = false;
    bool starttheTimer = false;
    public float startTimer = 3f;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI gameOverText;
    public Button StartGame;
    public Button Retry;
    public List<GameObject> Essentials;
    public List<Transform> SpawnPoints;
    public List<GameObject> Enemies;

    public float SpawnSquareEveryX = 5f;
    float SquareTimer = 0f;

    float speedMultiplier = 1.1f;


    public void SquareSetup(Square squareObj)
    {
        squareObj.gameManager = this;
        squareObj.circle = Essentials[0].GetComponent<Transform>();
    }   


    public void startGame()
    {
        Time.timeScale = 1;
        StartGame.gameObject.SetActive(false);
        starttheTimer = true;
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        startTheGame = false;
        Retry.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        //timer.text = "GAME OVER";

    }

    public void retryLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }


    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (starttheTimer && startTimer > 0)
        {
            timer.text = "STARTING IN " + startTimer.ToString("F2");
            startTimer -= Time.deltaTime;
        }
        else if (startTimer <= 0 && timer.gameObject.activeSelf)
        {
            timer.gameObject.SetActive(false);
            startTheGame = true;
        }

        if (startTheGame)
        {
            SquareTimer += Time.deltaTime;
            if (SquareTimer > SpawnSquareEveryX)
            {
                SquareTimer = 0;
                int spawnPoint = Random.Range(0, SpawnPoints.Count);
                var go = Instantiate(Enemies[0], SpawnPoints[spawnPoint].position, Quaternion.identity);
                SquareSetup(go.GetComponent<Square>());
                go.GetComponent<Square>().speed *= speedMultiplier;
                speedMultiplier += 0.1f;
            }
        }
    }
}
