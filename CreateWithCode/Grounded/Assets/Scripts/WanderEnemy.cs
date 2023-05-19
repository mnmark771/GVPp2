using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderEnemy : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject target;
    private GameObject player;
    private Vector3 startingPlace;
    private NavMeshAgent agent;
    public GameObject LifePickupPrefab;
    public float enemyRange = 5.5f;
    private float maxHealth = 1;
    public float currentHealth;
    private float enemyDamage = 1.0f;
    private bool hitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentHealth = maxHealth;
        startingPlace = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 0.7)
        {
            if (target == player)
            {
                target.GetComponent<PlayerController>().TakeDamage(enemyDamage);
            }

            SetRandomDestination();
        }

        hitPlayer = Physics.CheckSphere(transform.position, enemyRange, playerLayer);
        if (hitPlayer)
        {
            target = GameObject.Find("Player");
            agent.SetDestination(target.transform.position);
        }

        if (!hitPlayer)
        {
            target = null;
        }

    }
    
    public void SetRandomDestination()
    {
        var randomXPos = Random.Range(startingPlace.x - 8.0f, startingPlace.x + 8.0f);
        var randomZPos = Random.Range(startingPlace.z - 5.5f, startingPlace.z + 5.5f);
        Vector3 randomDestination = new Vector3(randomXPos, transform.position.y, randomZPos);
        agent.SetDestination(randomDestination);
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Eliminate();
        }
    }

    public void Eliminate()
    {
        var randomDrop = Random.Range(1,2);
        if (randomDrop == 1)
        {
            Instantiate(LifePickupPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            SetRandomDestination();
        }

        else if (collision.gameObject.CompareTag("Key Door"))
        {
            SetRandomDestination();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            SetRandomDestination();
        }

        else if (collision.gameObject.CompareTag("Key Door"))
        {
            SetRandomDestination();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (transform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, enemyRange);
    }
}
