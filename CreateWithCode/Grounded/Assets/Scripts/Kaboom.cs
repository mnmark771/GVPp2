using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaboom : MonoBehaviour
{
    public LayerMask kaboomLayers;
    public ParticleSystem popParticle;
    public float popDamage = 2;
    public float kaboomRange = 2;
    private Renderer kabooomRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pop());
        kabooomRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, kaboomRange);
    }

    void PopBubble()
    {
        Collider[] kaboomHits = Physics.OverlapSphere(transform.position, kaboomRange, kaboomLayers);
        foreach(Collider objectHit in kaboomHits)
        {
            if (objectHit.CompareTag("Enemy"))
            {
                objectHit.GetComponent<BasicEnemy>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Breakable"))
            {
                objectHit.GetComponent<Breakable>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Player"))
            {
                objectHit.GetComponent<PlayerController>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Breakable Wall"))
            {
                objectHit.GetComponent<BreakableWall>().Break();
            }

            if (objectHit.CompareTag("Dragon Boss"))
            {
                objectHit.GetComponent<DragonBoss>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Wandering Enemy"))
            {
                objectHit.GetComponent<WanderEnemy>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Boss Two"))
            {
                objectHit.GetComponent<BossTwo>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Head"))
            {
                objectHit.GetComponent<BossTwoHead>().TakeDamage(popDamage);
            }

            if (objectHit.CompareTag("Wizard Enemy"))
            {
                objectHit.GetComponent<WizardEnemy>().TakeDamage(popDamage);
            }
        }
    }

    IEnumerator Pop()
    {
        yield return new WaitForSeconds(1.5f);
        kabooomRenderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.0f);
        PopBubble();
        Destroy(gameObject);
    }
}
