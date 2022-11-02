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

    public int coinCount;

    private float pointsForPower = 5;
    private float spawnRange = 8.0f;
    private float spawnPosX = 0.0f;
    private float spawnPosZ = 0.0f;
    private float startDelay = 2.0f;
    private float repeatRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SpawnCoin();
        InvokeRepeating("SpawnFence", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        coinCount = GameObject.FindGameObjectsWithTag("Coin").Length;
        if (coinCount <= 0)
        {
            SpawnCoin();
            
            if (gameManager.score % pointsForPower == 0)
            {
                Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            }
        }
    }

    void SpawnFence()
    {
        spawnPosX = 0;
        spawnPosZ = 0;

        var fenceNumber = Random.Range(1,5);
        if (fenceNumber == 1)
        {
            spawnPosX = 10;
        }

        if (fenceNumber == 2)
        {
            spawnPosX = -10;
        }

        if (fenceNumber == 3)
        {
            spawnPosZ = 10;
        }

        if (fenceNumber == 4)
        {
            spawnPosZ = -10;
        }

        Vector3 spawnPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);

        if (fenceNumber == 1)
        {
            Instantiate(fenceRight, spawnPos, fenceRight.transform.rotation);
        }

        if (fenceNumber == 2)
        {
            Instantiate(fenceLeft, spawnPos, fenceLeft.transform.rotation);
        }

        if (fenceNumber == 3)
        {
            Instantiate(fenceTop, spawnPos, fenceTop.transform.rotation);
        }

        if (fenceNumber == 4)
        {
            Instantiate(fenceBottom, spawnPos, fenceBottom.transform.rotation);
        }
        //Debug.Log(fenceNumber);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    public void SpawnCoin()
    {
        Instantiate(coinPrefab, GenerateSpawnPosition(), coinPrefab.transform.rotation);
    }
}
