using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTwo : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Renderer BossTwoRenderer;
    private float maxHealth = 5;
    public float currentHealth;
    private float enemyDamage = 1.0f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        BossTwoRenderer = gameObject.GetComponent<Renderer>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 1.3)
        {
            SetRandomDestination();
        }
    }
    
    public void SetRandomDestination()
    {
        var randomXPos = Random.Range(startPosition.x - 7.0f, startPosition.x + 7.0f);
        var randomZPos = Random.Range(startPosition.z - 4.5f, startPosition.z + 4.5f);
        Vector3 randomDestination = new Vector3(randomXPos, startPosition.y, randomZPos);
        agent.SetDestination(randomDestination);
        
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        StartCoroutine(HitIndicator());
        if (currentHealth <= 0)
        {
            Eliminate();
        }
    }

    private void Eliminate()
    {
        Destroy(gameObject);
    }

    IEnumerator HitIndicator()
    {
        BossTwoRenderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.5f);
        BossTwoRenderer.material.SetColor("_Color", Color.white);
    }
}
