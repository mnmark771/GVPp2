using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Variables
    private float spawnRate = 1.0f;
    private int score;

    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public List<GameObject> targets;
    public bool isGameActive; // Bool is short for boolean
    public GameObject titleScreen;

    public TextMeshProUGUI livesText;
    private int lives;

    public GameObject pauseScreen;
    private bool paused;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    //This is a method header or method signiature  for UpdateScore
    //It uses a parameter "scoreToAdd
    public void UpdateScore(int scoreToAdd)
    {
        // += is like score = score + scoreToAdd
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        // Check if the player still has lives left. If not, the game ends
        if (lives <= 0)
        {
            GameOver();
        }
    }
    
    //Another method header that does not include a parameter
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        UpdateLives(3);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
