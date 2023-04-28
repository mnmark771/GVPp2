using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private float speed = 2.0f;
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    private Vector3 upStop;
    private Vector3 downStop;
    private Vector3 leftStop;
    private Vector3 rightStop;
    public LayerMask playerLayer;
    private bool vertical;
    private bool horizontal;
    private bool hasdirection;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        upStop = transform.position + new Vector3(0f,0f,4.5f);
        downStop = transform.position + new Vector3(0f,0f,-4.5f);
        rightStop = transform.position + new Vector3(7f,0f,0f);
        leftStop = transform.position + new Vector3(-7f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z >= upStop.z)
        {
            up = false;
            vertical = false;
        }
        if (transform.position.z <= downStop.z)
        {
            down = false;
            vertical = false;
        }
        if (transform.position.x >= rightStop.x)
        {
            right = false;
            horizontal = false;
        }
        if (transform.position.x <= leftStop.x)
        {
            left = false;
            horizontal = false;
        }
        if(up)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if(down)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }

        if(left)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if(right)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        if(!up && !down && !left && !right)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }

        if(transform.position == startPosition)
        {
            hasdirection = false;
            up = false;
            down = false;
            left = false;
            right = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !hasdirection)
        {
            hasdirection = true;
            Vector3 directionOfPlayer = transform.position - other.gameObject.transform.position;
            if (Mathf.Abs(directionOfPlayer.z) > Mathf.Abs(directionOfPlayer.x))
            {
                vertical = true;
            }
            else
            {
                horizontal = true;
            }

            if (vertical && other.gameObject.transform.position.z < transform.position.z)
            {
                down = true;
                up = false;
                left = false;
                right = false;
            }

            if (vertical && other.gameObject.transform.position.z > transform.position.z)
            {
                up = true;
                down = false;
                left = false;
                right = false;
            }

            if (horizontal && other.gameObject.transform.position.x < transform.position.x)
            {
                left = true;
                up = false;
                down = false;
                right = false;
            }

            if (horizontal && other.gameObject.transform.position.x > transform.position.x)
            {
                right = true;
                up = false;
                down = false;
                left = false;
            }
        }
    }
}
