using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody playerRb;
    private BoxCollider playerCollider;
    private Animator playerAnim;
    private AudioSource playerAudio;
    
    public Transform seconds;
    public GameObject powerupIndicator;

    public ParticleSystem coinParticle;
    public ParticleSystem powerupParticle;

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip coinSound;
    public AudioClip powerupSound;
    public AudioClip powerupWarningSound;

    public int pointValue = 1;

    public float rotationSpeed;
    public float jumpForce = 5.0f;
    private float speed = 5.0f;
    private float Range = 9.5f;

    private bool isOnGround = false;
    private bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GameObject.Find("Player Model").GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Keep player in bounds
        if (transform.position.x < -Range)
            
            {
                transform.position = new Vector3(-Range, transform.position.y, transform.position.z);
            }

        if (transform.position.x > Range)
            
            {
                transform.position = new Vector3(Range, transform.position.y, transform.position.z);
            }
        if (transform.position.z < -Range)
            
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -Range);
            }

        if (transform.position.z > Range)
            
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Range);
            }


        //Powerup indicator follows player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        //Get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Move player, look in direction of movement, animate movement
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (gameManager.isGameActive == true)
        {
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        }

        if (movementDirection != Vector3.zero && gameManager.isGameActive == true)
        {
            if (isOnGround)
            {
                playerAnim.SetFloat("Speed_f", 10.0f);
            }
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
        if (movementDirection == Vector3.zero || !isOnGround)
        {
            playerAnim.SetFloat("Speed_f", 0);
        }

        //Get jumping input
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && gameManager.isGameActive == true)
        {
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            playerAnim.SetFloat("Speed_f", 0);
            playerAnim.SetTrigger("Jump_trig");
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
        if (other.CompareTag("Powerup") && !hasPowerup)
        {
            playerAudio.PlayOneShot(powerupSound, 1.0f);
            powerupParticle.Play();
            gameObject.tag = "Untagged";
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }

        if (other.CompareTag("Fence") && gameObject.CompareTag("Player"))
        {
            playerAudio.PlayOneShot(hitSound, 1.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            gameManager.GameOver();
        }

        if (other.CompareTag("Coin"))
        {
            coinParticle.Play();
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
            gameManager.UpdateSpeed();
            playerAudio.PlayOneShot(coinSound, 1.0f);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(3.5f);
        powerupIndicator.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        powerupIndicator.gameObject.SetActive(true);
        playerAudio.PlayOneShot(powerupWarningSound, 1.0f);
        yield return new WaitForSeconds(0.4f);
        powerupIndicator.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        powerupIndicator.gameObject.SetActive(true);
        playerAudio.PlayOneShot(powerupWarningSound, 1.0f);
        yield return new WaitForSeconds(0.4f);
        gameObject.tag = "Player";
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
