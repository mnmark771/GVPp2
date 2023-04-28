using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private AudioSource gameManagerAudio;

    private MeshRenderer meshMaterial;
    private MeshRenderer floorMaterial;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public AudioClip clicksound;

    public Button playButton;
    public Button restartButton;
    public Button timeButton;
    public Button backgroundButton;
    public Button floorButton;
    public Button fogButton;
    public Button parcipitationButton;
    public Button visualsButton;

    public GameObject titleScreen;
    public GameObject mountains;
    public GameObject skydome;
    public GameObject floor;
    public ParticleSystem snow;
    public ParticleSystem rain;
    public GameObject fog;
    public GameObject sun;

    public Material Gravel;
    public Material Grass;
    public Material Concrete;
    public Material Sand;
    public Material Default;

    public bool isGameActive = false;
    private bool day = true;
    private bool foggy = true;
    private bool visualsMenu = false;

    public int score;
    private int highscore;
    private int backgroundNumber = 1;
    private int floorNumber = 1;
    private int parcipitationType = 1;

    public float fenceSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        highscoreText.text = "Highscore: " + highscore;
        Time.timeScale = 0;
        meshMaterial = mountains.GetComponent<MeshRenderer>();
        floorMaterial = floor.GetComponent<MeshRenderer>();
        gameManagerAudio = GetComponent<AudioSource>();
        UpdateVisualsToPreferences();
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
        if (score > highscore)
        {
            highscore = score;
            highscoreText.text = "Highscore: " + highscore;
        }
    }

    public void StartGame()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
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
        PlayerPrefs.SetInt("highscore", highscore);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeVisuals()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (visualsMenu)
        {
            parcipitationType = PlayerPrefs.GetInt("parcipitation pref");
            visualsMenu = false;
            GameObject.Find("Visuals Button").GetComponentInChildren<TextMeshProUGUI>().text = "Menu";
            playButton.gameObject.SetActive(false);
            timeButton.gameObject.SetActive(true);
            fogButton.gameObject.SetActive(true);
            backgroundButton.gameObject.SetActive(true);
            floorButton.gameObject.SetActive(true);
            parcipitationButton.gameObject.SetActive(true);
            UpdateVisualsText();
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
            parcipitationButton.gameObject.SetActive(false);
        }
    }

    public void ChangeTime()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (day == true)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Night";
            sun.gameObject.SetActive(false);
            mountains.gameObject.SetActive(false);
            skydome.gameObject.SetActive(true);
            day = false;
            PlayerPrefs.SetInt("time pref", 2);
        }

        else if (day == false)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Day";
            sun.gameObject.SetActive(true);
            mountains.gameObject.SetActive(true);
            skydome.gameObject.SetActive(false);
            day = true;
            PlayerPrefs.SetInt("time pref", 1);
        }
    }

    public void FogControl()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (foggy == true)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: OFF";
            fog.gameObject.SetActive(false);
            foggy = false;
            PlayerPrefs.SetInt("fog pref", 2);
        }

        else if (foggy == false)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: ON";
            fog.gameObject.SetActive(true);
            foggy = true;
            PlayerPrefs.SetInt("fog pref", 1);
        }
    }

    public void ChangeBackgroundTexture()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (backgroundNumber == 1)
        {
            meshMaterial.material = Default;
            backgroundNumber ++;
            PlayerPrefs.SetInt("background pref", 2);
        }

        else if (backgroundNumber == 2)
        {
            meshMaterial.material = Concrete;
            backgroundNumber ++;
            PlayerPrefs.SetInt("background pref", 3);
        }

        else if (backgroundNumber == 3)
        {
            meshMaterial.material = Sand;
            backgroundNumber++;
            PlayerPrefs.SetInt("background pref", 4);
        }

        else if (backgroundNumber == 4)
        {
            meshMaterial.material = Gravel;
            backgroundNumber++;
            PlayerPrefs.SetInt("background pref", 5);
        }

        else if (backgroundNumber == 5)
        {
            meshMaterial.material = Grass;
            backgroundNumber = 1;
            PlayerPrefs.SetInt("background pref", 1);
        }
    }

    public void ChangeFloorTexture()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (floorNumber == 1)
        {
            floorMaterial.material = Concrete;
            floorNumber ++;
            PlayerPrefs.SetInt("floor pref", 2);
        }

        else if (floorNumber == 2)
        {
            floorMaterial.material = Sand;
            floorNumber ++;
            PlayerPrefs.SetInt("floor pref", 3);
        }

        else if (floorNumber == 3)
        {
            floorMaterial.material = Gravel;
            floorNumber ++;
            PlayerPrefs.SetInt("floor pref", 4);
        }

        else if (floorNumber == 4)
        {
            floorMaterial.material = Grass;
            floorNumber = 1;
            PlayerPrefs.SetInt("floor pref", 1);
        }
    }

    public void ChangeParcipitation()
    {
        gameManagerAudio.PlayOneShot(clicksound, 1.0f);
        if (parcipitationType == 1)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: Rain";
            rain.gameObject.SetActive(true);
            snow.gameObject.SetActive(false);
            parcipitationType++;
            PlayerPrefs.SetInt("parcipitation pref", 2);
        }

        else if (parcipitationType == 2)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: Snow";
            rain.gameObject.SetActive(false);
            snow.gameObject.SetActive(true);
            parcipitationType++;
            PlayerPrefs.SetInt("parcipitation pref", 3);
        }

        else if (parcipitationType == 3)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: None";
            rain.gameObject.SetActive(false);
            snow.gameObject.SetActive(false);
            parcipitationType = 1;
            PlayerPrefs.SetInt("parcipitation pref", 1);
        }
    }

    public void UpdateVisualsToPreferences()
    {
        if (PlayerPrefs.GetInt("parcipitation pref") == 2)
        {
            rain.Play();
            snow.Stop();
            //rain.gameObject.SetActive(true);
            //snow.gameObject.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("parcipitation pref") == 3)
        {
            rain.Stop();
            snow.Play();
            //rain.gameObject.SetActive(false);
            //snow.gameObject.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("parcipitation pref") == 1)
        {
            rain.Stop();
            snow.Stop();
            //rain.gameObject.SetActive(false);
            //snow.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("floor pref") == 2)
        {
            floorMaterial.material = Concrete;
        }

        else if (PlayerPrefs.GetInt("floor pref") == 3)
        {
            floorMaterial.material = Sand;
        }

        else if (PlayerPrefs.GetInt("floor pref") == 4)
        {
            floorMaterial.material = Gravel;
        }

        else if (PlayerPrefs.GetInt("floor pref") == 1)
        {
            floorMaterial.material = Grass;
        }

        if (PlayerPrefs.GetInt("background pref") == 2)
        {
            meshMaterial.material = Default;
        }

        else if (PlayerPrefs.GetInt("background pref") == 3)
        {
            meshMaterial.material = Concrete;
        }

        else if (PlayerPrefs.GetInt("background pref") == 4)
        {
            meshMaterial.material = Sand;
        }

        else if (PlayerPrefs.GetInt("background pref") == 5)
        {
            meshMaterial.material = Gravel;
        }

        else if (PlayerPrefs.GetInt("background pref") == 1)
        {
            meshMaterial.material = Grass;
        }

        if (PlayerPrefs.GetInt("fog pref") == 2)
        {
            fog.gameObject.SetActive(false);
            foggy = false;
        }

        else if (PlayerPrefs.GetInt("fog pref") == 1)
        {
            fog.gameObject.SetActive(true);
            foggy = true;
        }

        if (PlayerPrefs.GetInt("time pref") == 2)
        {
            sun.gameObject.SetActive(false);
            mountains.gameObject.SetActive(false);
            skydome.gameObject.SetActive(true);
            day = false;
        }

        else if (PlayerPrefs.GetInt("time pref") == 1)
        {
            sun.gameObject.SetActive(true);
            mountains.gameObject.SetActive(true);
            skydome.gameObject.SetActive(false);
            day = true;
        }
    }

    public void UpdateVisualsText()
    {
        if (PlayerPrefs.GetInt("parcipitation pref") == 2)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: Rain";
        }

        else if (PlayerPrefs.GetInt("parcipitation pref") == 3)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: Snow";
        }

        else if (PlayerPrefs.GetInt("parcipitation pref") == 1)
        {
            GameObject.Find("Parcipitation Button").GetComponentInChildren<TextMeshProUGUI>().text = "Parcipitation: None";
        }

        if (foggy == true)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: ON";
        }

        else if (foggy == false)
        {
            GameObject.Find("Fog Button").GetComponentInChildren<TextMeshProUGUI>().text = "Fog: OFF";
        }

        if (day == true)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Day";
        }

        else if (day == false)
        {
            GameObject.Find("Time Button").GetComponentInChildren<TextMeshProUGUI>().text = "Night";
        }
    }
}
