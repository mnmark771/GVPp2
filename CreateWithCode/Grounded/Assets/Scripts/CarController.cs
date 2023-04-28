using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed;
    public float adjustSpeed;
    public float turnSpeed;
    public float lookSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * adjustSpeed);

            Vector3 pos = hit.point;
            Ray upRay = new Ray(pos, transform.position - pos); 
            Vector3 upDist = upRay.GetPoint(1); 

            // Gradually interpolate the car's position towards the hit point
            transform.position = Vector3.Lerp(transform.position, upDist, Time.deltaTime * speed);

            Vector3 forwardForce = transform.forward * speed;
            Vector3 horizontalForce = transform.right * turnSpeed;
            rb.AddForce(forwardForce * forwardInput * Time.deltaTime);
            transform.Rotate(Vector3.up, lookSpeed * horizontalInput * Time.deltaTime);

        }
    }
}