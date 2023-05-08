using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    private GameObject player;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused && player.GetComponent<PlayerController>().inMenu == false)
            {
                Pause();
            }

            else if (gameIsPaused)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Respawn()
    {
        SceneManager.LoadScene("TheMaze");
    }
}
