using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    public GameObject grass;
    public GameObject enemy;
    public GameObject portal;
    public GameObject spikeTrap;
    public int enemyAmount = 25;
    public int grassAmount = 250;
    public int trapAmount = 10;
    public int mapSize = 25;

    // Start is called before the first frame update
    void Start()
    {
        spawnenemy();
        spawngrass();
        spawnportal();
        spawntrap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnenemy()
    {
        for(int i=0; i<enemyAmount; i++)
        {
            float randomX = Random.Range(-mapSize,mapSize);
            float randomZ = Random.Range(-mapSize,mapSize);
            Vector3 spawnpos = new Vector3(randomX, 1, randomZ);
            Instantiate(enemy, spawnpos, transform.rotation, gameObject.transform);
        }

    }

    private void spawngrass()
    {
        for(int i=0; i<grassAmount; i++)
        {
            float randomX = Random.Range(-mapSize,mapSize);
            float randomZ = Random.Range(-mapSize,mapSize);
            Vector3 spawnpos = new Vector3(randomX, 1, randomZ);
            Instantiate(grass, spawnpos, transform.rotation, gameObject.transform);
        }

    }

    private void spawnportal()
    {
            float randomX = Random.Range(-mapSize,mapSize);
            float randomZ = Random.Range(-mapSize,mapSize);
            Vector3 spawnpos = new Vector3(randomX, 1, randomZ);
            Instantiate(portal, spawnpos, transform.rotation, gameObject.transform);
    }

    private void spawntrap()
    {
        for(int i=0; i<trapAmount; i++)
        {
            float randomX = Random.Range(-mapSize,mapSize);
            float randomZ = Random.Range(-mapSize,mapSize);
            Vector3 spawnpos = new Vector3(randomX, 1, randomZ);
            Instantiate(spikeTrap, spawnpos, transform.rotation, gameObject.transform);
        }
    }
}
