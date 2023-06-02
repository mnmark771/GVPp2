using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string currentScene;

    public void PlayGame()
    {
        currentScene = PlayerPrefs.GetString("currentScene");
        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("keys", 0);
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("kabooms", 0);
        PlayerPrefs.SetInt("healthPotions", 0);
        PlayerPrefs.SetFloat("currentHealth", 3);
        PlayerPrefs.SetFloat("maxHealth", 3);
        PlayerPrefs.SetString("currentScene", "TheMaze");
        PlayerPrefs.SetInt("masterKey", 0);
    }
}
