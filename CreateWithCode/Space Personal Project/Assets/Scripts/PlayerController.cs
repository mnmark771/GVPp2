using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private float RangeX = 5f;
    private float RangeY = 3f;

    public GameObject bulletPrefab;
    public GameObject exitPoint;
    private Vector3 aim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.down * verticalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        //transform.Translate(Vector3.left * horizontalInput * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, exitPoint.transform.position, exitPoint.transform.rotation);
        }
        
    }
}