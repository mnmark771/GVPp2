using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject fenceBottom;
    public GameObject fenceTop;
    public GameObject fenceRight;
    public GameObject fenceLeft;

    private float spawnPosX = 0.0f;
    private float spawnPosZ = 0.0f;
    private float startDelay = 2.0f;
    private float repeatRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFence", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFence();
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
        Debug.Log(fenceNumber);
    }
}
