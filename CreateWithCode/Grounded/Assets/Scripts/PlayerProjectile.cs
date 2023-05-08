using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float speed = 10;
    public LayerMask enemyLayers;
    public bool detecting = true;
    private float projectileDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifeTime());
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
                    detecting = false;
                }

                else if (enemy.CompareTag("Dragon Boss"))
                {
                    enemy.GetComponent<DragonBoss>().TakeDamage(projectileDamage);
                    detecting = false;
                }
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
