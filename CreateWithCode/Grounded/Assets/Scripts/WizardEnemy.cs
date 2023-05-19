using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public GameObject enemyProjectilePrefab;
    public GameObject LifePickupPrefab;
    public GameObject KaboomPickupPrefab;
    public float knockBack;
    private float maxHealth = 3;
    public float currentHealth;
    private float startDelay = 1;
    private float repeatRate = 2;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        enemyRb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        player = GameObject.Find("Player");
        InvokeRepeating("TeleportAndThrow", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void TeleportAndThrow()
    {
        StartCoroutine(MagicMove());
    }

    public void MoveRandomDestination()
    {
        var randomXPos = Random.Range(startPosition.x - 8.0f, startPosition.x + 8.0f);
        var randomZPos = Random.Range(startPosition.z - 5.5f, startPosition.z + 5.5f);
        Vector3 randomDestination = new Vector3(randomXPos, startPosition.y, randomZPos);
        transform.position = randomDestination;
        
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Eliminate();
        }
        enemyRb.AddRelativeForce(-Vector3.forward * knockBack, ForceMode.Impulse);
    }

    public void Eliminate()
    {
        var randomDrop = Random.Range(1,3);
        if (randomDrop == 1)
        {
            Instantiate(KaboomPickupPrefab, transform.position, transform.rotation);
        }

        if (randomDrop == 2)
        {
            Instantiate(LifePickupPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    IEnumerator MagicMove()
    {
        MoveRandomDestination();
        yield return new WaitForSeconds(1.0f);
        transform.LookAt(player.transform);
        Instantiate(enemyProjectilePrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(5.0f);
    }
}
