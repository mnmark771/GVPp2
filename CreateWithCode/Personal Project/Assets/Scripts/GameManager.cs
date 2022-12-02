using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    public float fenceSpeed = 2.5f;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    public Button playButton;
    public Button restartButton;
    public Button timeButton;
    public Button fogButton;

    public GameObject titleScreen;
    public GameObject mountains;
    public GameObject skydome;
    public GameObject fog;

    public bool isGameActive = false;
    public bool day = true;
    public bool foggy = true;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpeed()
    {
        if (fenceSpeed < 5.5f)
        {
            fenceSpeed += 0.15f;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        fogButton.gameObject.SetActive(false);
        timeButton.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Time.timeScale = 1;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
    }

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

    public void ChangeTime()
    {
        if (day == true)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Night";
            mountains.gameObject.SetActive(false);
            skydome.gameObject.SetActive(true);
            day = false;
        }

        else if (day == false)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Day";
            mountains.gameObject.SetActive(true);
            skydome.gameObject.SetActive(false);
            day = true;
        }
    }

    public void FogControl()
    {
        if (foggy == true)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: OFF";
            fog.gameObject.SetActive(false);
            foggy = false;
        }

        else if (foggy == false)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: ON";
            fog.gameObject.SetActive(true);
            foggy = true;
        }
    }
}
