using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private float speed = 10;
    public bool comeBack;
    private bool detecting = true;
    public LayerMask enemyLayers;
    private float projectileDamage = 1;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitDistance());
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (detecting)
        {
            Collider[] hitEnemies = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, enemyLayers);
            foreach(Collider enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<BasicEnemy>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }

                else if (enemy.CompareTag("Dragon Boss"))
                {
                    enemy.GetComponent<DragonBoss>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }
                
                else if (enemy.CompareTag("Wandering Enemy"))
                {
                    enemy.GetComponent<WanderEnemy>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }

                else if (enemy.CompareTag("Boss Two"))
                {
                    enemy.GetComponent<BossTwo>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }

                else if (enemy.CompareTag("Head"))
                {
                    enemy.GetComponent<BossTwoHead>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }

                else if (enemy.CompareTag("Wizard Enemy"))
                {
                    enemy.GetComponent<WizardEnemy>().TakeDamage(projectileDamage);
                    comeBack = true;
                    detecting = false;
                }

                else if (enemy.CompareTag("Wall"))
                {
                    comeBack = true;
                }
    
                else if (enemy.CompareTag("Breakable Wall"))
                {
                    comeBack = true;
                }
            
                else if (enemy.CompareTag("Key Door"))
                {
                    comeBack = true;
                }
    
                else if (enemy.CompareTag("Sign"))
                {
                    comeBack = true;
                }

                else if (enemy.CompareTag("Door"))
                {
                    comeBack = true;
                }

                else if (enemy.CompareTag("Clear Room Door"))
                {
                    comeBack = true;
                }

                comeBack = true;
            }
        }

        if (!comeBack)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Door") || collision.gameObject.CompareTag("Key Door"))
        {
            comeBack = true;
            detecting = false;
        }
    }

    IEnumerator WaitDistance()
    {
        yield return new WaitForSeconds(1.0f);
        comeBack = true;
    }
}
