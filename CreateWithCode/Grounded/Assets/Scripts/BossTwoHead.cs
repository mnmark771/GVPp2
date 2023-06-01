using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoHead : MonoBehaviour
{
    private GameObject player;
    private Renderer BossTwoHeadRenderer;
    public GameObject enemyProjectilePrefab;
    public float maxHealth = 3;
    private float startDelay = 3;
    private float repeatRate = 3;
    public float currentHealth;
    //private float enemyDamage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.Find("Player");
        BossTwoHeadRenderer = gameObject.GetComponent<Renderer>();
        InvokeRepeating("Throw", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Throw()
    {
        transform.LookAt(player.transform);
        Instantiate(enemyProjectilePrefab, transform.position, transform.rotation);
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
        BossTwoHeadRenderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.5f);
        BossTwoHeadRenderer.material.SetColor("_Color", Color.white);
    }
}
