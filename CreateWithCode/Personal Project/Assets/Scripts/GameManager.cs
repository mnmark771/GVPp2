using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private MeshRenderer meshMaterial;
    private MeshRenderer floorMaterial;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    public Button playButton;
    public Button restartButton;
    public Button timeButton;
    public Button backgroundButton;
    public Button floorButton;
    public Button fogButton;
    public Button visualsButton;

    public GameObject titleScreen;
    public GameObject mountains;
    public GameObject skydome;
    public GameObject floor;
    public GameObject fog;
    public GameObject sun;

    public Material Gravel;
    public Material Grass;
    public Material Concrete;
    public Material Sand;
    public Material Default;

    public bool isGameActive = false;
    public bool day = true;
    public bool foggy = true;
    private bool visualsMenu = false;

    public int score;
    private int backgroundNumber = 1;
    private int floorNumber = 1;

    public float fenceSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        meshMaterial = mountains.GetComponent<MeshRenderer>();
        floorMaterial = floor.GetComponent<MeshRenderer>();
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
        //fogButton.gameObject.SetActive(false);
        //timeButton.gameObject.SetActive(false);
        visualsButton.gameObject.SetActive(false);
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

    public void ChangeVisuals()
    {
        if (visualsMenu)
        {
            visualsMenu = false;
            GameObject.Find("Visuals Button").GetComponentInChildren<TextMeshProUGUI>().text = "Menu";
            playButton.gameObject.SetActive(false);
            timeButton.gameObject.SetActive(true);
            fogButton.gameObject.SetActive(true);
            backgroundButton.gameObject.SetActive(true);
            floorButton.gameObject.SetActive(true);
        }

        else if (!visualsMenu)
        {
            visualsMenu = true;
            GameObject.Find("Visuals Button").GetComponentInChildren<TextMeshProUGUI>().text = "Visuals";
            playButton.gameObject.SetActive(true);
            timeButton.gameObject.SetActive(false);
            fogButton.gameObject.SetActive(false);
            backgroundButton.gameObject.SetActive(false);
            floorButton.gameObject.SetActive(false);
        }
    }

    public void ChangeTime()
    {
        if (day == true)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Night";
            sun.gameObject.SetActive(false);
            mountains.gameObject.SetActive(false);
            skydome.gameObject.SetActive(true);
            day = false;
        }

        else if (day == false)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Day";
            sun.gameObject.SetActive(true);
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

    public void ChangeBackgroundTexture()
    {
        if (backgroundNumber == 1)
        {
            meshMaterial.material = Default;
            backgroundNumber += 1;
        }

        else if (backgroundNumber == 2)
        {
            meshMaterial.material = Concrete;
            backgroundNumber += 1;
        }

        else if (backgroundNumber == 3)
        {
            meshMaterial.material = Sand;
            backgroundNumber++;
        }

        else if (backgroundNumber == 4)
        {
            meshMaterial.material = Gravel;
            backgroundNumber++;
        }

        else if (backgroundNumber == 5)
        {
            meshMaterial.material = Grass;
            backgroundNumber = 1;
        }
    }

    public void ChangeFloorTexture()
    {
        if (floorNumber == 1)
        {
            floorMaterial.material = Concrete;
            floorNumber += 1;
        }

        else if (floorNumber == 2)
        {
            floorMaterial.material = Sand;
            floorNumber += 1;
        }

        else if (floorNumber == 3)
        {
            floorMaterial.material = Gravel;
            floorNumber += 1;
        }

        else if (floorNumber == 4)
        {
            floorMaterial.material = Grass;
            floorNumber = 1;
        }
    }
}
