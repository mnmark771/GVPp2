using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoss : MonoBehaviour
{
    public GameObject enemyProjectilePrefab;
    public LayerMask playerLayer;
    private Vector3 startPosition;
    private Vector3 destination;
    public float enemyRange = 20.0f;
    private float speed = 1.0f;
    private float repeatRate = 0.0f;
    private float waitTime = 0.0f;
    private float maxHealth = 3.0f;
    private float currentHealth;
    private bool attacking;
    private bool hitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        destination = transform.position + new Vector3(0.0f, 0.0f, 1.0f);
        //InvokeRepeating("BreatheFire", startDelay, repeatRate);
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hitPlayer = Physics.CheckSphere(transform.position, enemyRange, playerLayer);
        if (hitPlayer)
        {
            attacking = true;
        }

        waitTime += Time.deltaTime;
        if (waitTime >= repeatRate && attacking)
        {
            BreatheFire();
            waitTime = 0.0f;
        }
        

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (transform.position == destination && destination.z > startPosition.z)
        {
            destination = transform.position + new Vector3(0.0f, 0.0f, -2.0f);
        }

        if (transform.position == destination && destination.z < startPosition.z)
        {
            destination = transform.position + new Vector3(0.0f, 0.0f, +2.0f);
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

    private void BreatheFire()
    {
        repeatRate = Random.Range(1.25f, 2.0f);
        Instantiate(enemyProjectilePrefab, transform.position, transform.rotation);
        Instantiate(enemyProjectilePrefab, transform.position + new Vector3(0.0f, 0.0f, 3.0f), transform.rotation);
        Instantiate(enemyProjectilePrefab, transform.position + new Vector3(0.0f, 0.0f, -3.0f), transform.rotation);
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
        Destroy(gameObject);
    }
}
