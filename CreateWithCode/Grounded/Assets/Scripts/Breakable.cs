using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private float health = 1.0f;
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        var randomDropNumber = Random.Range(1, 7);
        if (randomDropNumber == 1)
        {
            Instantiate(coinPrefab, transform.position, transform.rotation);
        }
    }
}
