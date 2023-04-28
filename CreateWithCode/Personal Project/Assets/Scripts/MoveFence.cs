using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFence : MonoBehaviour
{
    private GameManager gameManager;
    
    public float speed = 0.0f;
    private bool bottom = false;
    private bool top = false;
    private bool right = false;
    private bool left = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speed = gameManager.fenceSpeed;
        if (transform.position.z <= -10)
        {
            bottom = true;
        }

        if (transform.position.z >= 10)
        {
            top = true;
        }

        if (transform.position.x <= -10)
        {
            left = true;
        }

        if (transform.position.x >= 10)
        {
            right = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bottom == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (top == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed);
        }

        if (right == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speed);
        }

        if (left == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (transform.position.z >= 20 || transform.position.z <= -20)
        {
            Destroy(gameObject);
        }

        if (transform.position.x >= 20 || transform.position.x <= -20)
        {
            Destroy(gameObject);
        }
    }
}
