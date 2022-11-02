using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    private float speed = 5.0f;
    public float jumpForce = 5.0f;

    private bool isOnGround = true;
    private bool hasPowerup = false;

    private Rigidbody playerRb;
    private BoxCollider playerCollider;

    public Transform seconds;

    public int pointValue = 1;

    //public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerCollider = GetComponent<BoxCollider>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            gameObject.tag = "Untagged";
            hasPowerup = true;
            Destroy(other.gameObject);
            //Debug.Log("Had Power");
            StartCoroutine(PowerupCountdownRoutine());
            //powerupIndicator.gameObject.SetActive(true);
        }

        if (other.CompareTag("Fence") && gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }

        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
            //Debug.Log("+1");
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        gameObject.tag = "Player";
        //Debug.Log("no power");
        hasPowerup = false;
        //powerupIndicator.gameObject.SetActive(false);
    }
}
