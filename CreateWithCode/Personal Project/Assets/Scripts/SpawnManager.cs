using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject powerupPrefab;
    public GameObject fenceBottom;
    public GameObject fenceTop;
    public GameObject fenceRight;
    public GameObject fenceLeft;
    public GameObject coinPrefab;

    private int coinCount;

    public float interpolationPeriod = 4.0f;
    private float pointsForPower = 5;
    private float spawnRange = 6.5f;
    private float spawnPosX = 0.0f;
    private float spawnPosZ = 0.0f;
    private float startDelay = 2.0f;
    private float repeatRate = 4.0f;
    private float time = 0.0f;

    // Start is called before the first frame update
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SpawnCoin();
    }
    
    // Update is called once per frame
    void Update()
    {
        //If game is playing then spawn a fence every certain period of time
        if (gameManager.isGameActive == true)
        {
            time += Time.deltaTime;
            if (time >= interpolationPeriod)
            {
                SpawnFence();
                time = 0.0f;
            }
        }

        //Decrease time between fences spawning when a coin is collected
        coinCount = GameObject.FindGameObjectsWithTag("Coin").Length;
        if (coinCount <= 0)
        {
            if (interpolationPeriod > 2.0f)
            {
                interpolationPeriod -= 0.05f;
            }

            if (interpolationPeriod < 2.0f)
            {
                interpolationPeriod = 2.0f;
            }
            SpawnCoin();
            
            if (gameManager.score % pointsForPower == 0)
            {
                Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            }
        }
    }

    void SpawnFence()
    {
        var obstacleType = Random.Range(1, 7);
        if (obstacleType == 1 || obstacleType == 2)
        {
            SingleFence();
        }

        if (obstacleType == 3 || obstacleType == 4)
        {
            CornerFence();
        }

        if (obstacleType == 5 || obstacleType == 6)
        {
            DoubleFence();
        }
    }

    void SingleFence()
    {
        var fenceNumber = Random.Range(1,5);
        if (fenceNumber == 1)
        {
            Instantiate(fenceTop, fenceTop.transform.position, fenceTop.transform.rotation);
        }

        if (fenceNumber == 2)
        {
            Instantiate(fenceRight, fenceRight.transform.position, fenceRight.transform.rotation);
        }

        if (fenceNumber == 3)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position, fenceBottom.transform.rotation);
        }

        if (fenceNumber == 4)
        {
            Instantiate(fenceLeft, fenceLeft.transform.position, fenceLeft.transform.rotation);
        }
    }

    void CornerFence()
    {
        var cornerNumber = Random.Range(1,9);
        if (cornerNumber == 1)
        {
            Instantiate(fenceTop, fenceTop.transform.position, fenceTop.transform.rotation);
            Instantiate(fenceRight, fenceRight.transform.position + new Vector3(5,0,0), fenceRight.transform.rotation);
        }

        if (cornerNumber == 2)
        {
            Instantiate(fenceTop, fenceTop.transform.position + new Vector3(0,0,5), fenceTop.transform.rotation);
            Instantiate(fenceRight, fenceRight.transform.position, fenceRight.transform.rotation);
        }

        if (cornerNumber == 3)
        {
            Instantiate(fenceTop, fenceTop.transform.position, fenceTop.transform.rotation);
            Instantiate(fenceLeft, fenceLeft.transform.position + new Vector3(-5,0,0), fenceLeft.transform.rotation);
        }

        if (cornerNumber == 4)
        {
            Instantiate(fenceTop, fenceTop.transform.position + new Vector3(0,0,5), fenceTop.transform.rotation);
            Instantiate(fenceLeft, fenceLeft.transform.position, fenceLeft.transform.rotation);
        }

        if (cornerNumber == 5)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position + new Vector3(0,0,-5), fenceBottom.transform.rotation);
            Instantiate(fenceLeft, fenceLeft.transform.position, fenceLeft.transform.rotation);
        }

        if (cornerNumber == 6)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position, fenceBottom.transform.rotation);
            Instantiate(fenceLeft, fenceLeft.transform.position + new Vector3(-5,0,0), fenceLeft.transform.rotation);
        }

        if (cornerNumber == 7)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position + new Vector3(0,0,-5), fenceBottom.transform.rotation);
            Instantiate(fenceRight, fenceRight.transform.position, fenceRight.transform.rotation);
        }

        if (cornerNumber == 8)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position, fenceBottom.transform.rotation);
            Instantiate(fenceRight, fenceRight.transform.position + new Vector3(5,0,0), fenceRight.transform.rotation);
        }
    }

    void DoubleFence()
    {
        var fenceNumber = Random.Range(1,5);
        if (fenceNumber == 1)
        {
            Instantiate(fenceTop, fenceTop.transform.position, fenceTop.transform.rotation);
            Instantiate(fenceTop, fenceTop.transform.position + new Vector3(0,0,6), fenceTop.transform.rotation);
        }

        if (fenceNumber == 2)
        {
            Instantiate(fenceRight, fenceRight.transform.position, fenceRight.transform.rotation);
            Instantiate(fenceRight, fenceRight.transform.position + new Vector3(6,0,0), fenceRight.transform.rotation);
        }

        if (fenceNumber == 3)
        {
            Instantiate(fenceBottom, fenceBottom.transform.position, fenceBottom.transform.rotation);
            Instantiate(fenceBottom, fenceBottom.transform.position + new Vector3(0,0,-6), fenceBottom.transform.rotation);
        }

        if (fenceNumber == 4)
        {
            Instantiate(fenceLeft, fenceLeft.transform.position, fenceLeft.transform.rotation);
            Instantiate(fenceLeft, fenceLeft.transform.position + new Vector3(-6,0,0), fenceLeft.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0.75f, spawnPosZ);

        return randomPos;
    }

    public void SpawnCoin()
    {
        Instantiate(coinPrefab, GenerateSpawnPosition(), coinPrefab.transform.rotation);
    }
}