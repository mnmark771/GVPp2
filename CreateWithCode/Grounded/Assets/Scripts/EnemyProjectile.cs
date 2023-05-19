using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 10;
    public LayerMask playerLayer;
    private bool detecting = true;
    public ParticleSystem projectileParticle;

    // Start is called before the first frame update
    void Start()
    {
        projectileParticle.Play();
        StartCoroutine(lifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (detecting)
        {
            Collider[] hitPlayer = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, playerLayer);
            foreach(Collider player in hitPlayer)
            {
                if (player.CompareTag("Player"))
                {
                    player.GetComponent<PlayerController>().TakeDamage(1);
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
