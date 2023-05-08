using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private float speed = 10;
    public bool comeBack;
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

        Collider[] hitEnemies = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<BasicEnemy>().TakeDamage(projectileDamage);
                comeBack = true;
            }

            else if (enemy.CompareTag("Dragon Boss"))
            {
                enemy.GetComponent<DragonBoss>().TakeDamage(projectileDamage);
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

    IEnumerator WaitDistance()
    {
        yield return new WaitForSeconds(1.0f);
        comeBack = true;
    }
}
