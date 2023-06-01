using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public LayerMask playerLayer;
    private Rigidbody enemyRb;
    public GameObject target;
    private GameObject player;
    private NavMeshAgent agent;
    public GameObject enemyProjectilePrefab;
    public GameObject LifePickupPrefab;
    public GameObject KaboomPickupPrefab;
    public float enemyRange = 5.5f;
    public float knockBack = 8.0f;
    private float maxHealth = 3;
    public float currentHealth;
    private float enemyDamage = 1.0f;
    private bool hitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        SetRandomDestination();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 1.3)
        {   
            if (target != player)
            {
                StartCoroutine(StopAndThrow());
            }
            SetRandomDestination();
        }

        hitPlayer = Physics.CheckSphere(transform.position, enemyRange, playerLayer);
        if (hitPlayer)
        {
            target = GameObject.Find("Player");
            agent.SetDestination(target.transform.position);
        }

        else
        {
            target = null;
        }

    }
    
    public void SetRandomDestination()
    {
        var randomXPos = Random.Range(transform.position.x - 8.0f, transform.position.x + 8.0f);
        var randomZPos = Random.Range(transform.position.z - 5.5f, transform.position.z + 5.5f);
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
        enemyRb.AddRelativeForce(-Vector3.forward * knockBack, ForceMode.Impulse);
    }

    public void Eliminate()
    {
        var randomDrop = Random.Range(1,9);
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

    IEnumerator StopAndThrow()
    {
        target = null;
        yield return new WaitForSeconds(1.0f);
        Instantiate(enemyProjectilePrefab, transform.position, transform.rotation);
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