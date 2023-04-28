using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Slider playerHealthbar;
    public Slider playerStaminabar;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private Renderer playerRenderer;

    public ParticleSystem coinParticle;
    public ParticleSystem powerupParticle;

    public AudioClip hitSound;
    public AudioClip coinSound;

    public GameObject pizzaProjectilePrefab;
    public GameObject kaboomPrefab;
    public GameObject gameOverMenu;
    public Transform rangePosition;

    public TextMeshProUGUI kaboomsText;
    public TextMeshProUGUI coinsText;

    public float knockBack;
    private float rotationSpeed = 360;
    private float speed = 5.0f;
    private float playerRange = 0.5f;
    private float attackRate = 1.0f;
    private float nextAttackTime = 0.0f;
    private float playerDamage = 1.0f;
    private float maxHealth = 3.0f;
    private float currentHealth;
    private int coins = 0;
    private int kabooms = 0;
    private float fillTime = 0.0f;

    private bool isGameActive = true;
    private bool missed = true;
    public bool isImmortal;
    private bool canMove = true;
    private bool canAttack = true;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerHealthbar.maxValue = maxHealth;
        playerHealthbar.value = currentHealth;
        playerStaminabar.value = 1;
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        playerRenderer = gameObject.GetComponent<Renderer>();
    }

    // Draw the player's range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (rangePosition == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(rangePosition.position, playerRange);
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(rangePosition.position, playerRange, enemyLayers);
        foreach(Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<BasicEnemy>().TakeDamage(playerDamage);
                missed = false;
            }

            else if (enemy.CompareTag("Breakable"))
            {
                enemy.GetComponent<Breakable>().TakeDamage(playerDamage);
                missed = true;
            }

            else if (enemy.CompareTag("Dragon Boss"))
            {
                enemy.GetComponent<DragonBoss>().TakeDamage(playerDamage);
                missed = true;
            }

            else
            {
                missed = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Move player, look in direction of movement, animate movement
        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        if (isGameActive == true && canMove)
        {
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        }

        if (movementDirection != Vector3.zero && isGameActive == true && canMove)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime && canAttack)
        {            
            missed = true;
            Attack();
            playerStaminabar.value = 0;
            fillTime = 0.0f;
            if (missed && currentHealth == maxHealth)
            {
                Instantiate(pizzaProjectilePrefab, transform.position, transform.rotation);
            }
            nextAttackTime = Time.time + 1f/attackRate;
        }

        if (playerStaminabar.value < 1)
        {
            playerStaminabar.value = Mathf.Lerp(playerStaminabar.minValue, playerStaminabar.maxValue, fillTime);
            fillTime += 1.0f * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F) && kabooms > 0)
        {
            Instantiate(kaboomPrefab, transform.position, transform.rotation);
            kabooms -= 1;
            kaboomsText.text = kabooms.ToString();
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins += 1;
            coinsText.text = coins.ToString();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Kaboom Pickup"))
        {
            kabooms += 1;
            kaboomsText.text = kabooms.ToString();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Life Pickup"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                playerHealthbar.value = currentHealth;
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sign"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //canAttack = false;
                //canMove = false;
                other.GetComponent<Sign>().Interact();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike Trap"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isImmortal)
        {
            canMove = false;
            currentHealth -= damage;
            playerHealthbar.value = currentHealth;
            StartCoroutine(Immortality());
            if (currentHealth <= 0)
            {
                Eliminate();
            }
            playerRb.AddRelativeForce(-Vector3.forward * knockBack, ForceMode.Impulse);
        }
    }

    public void Eliminate()
    {
        Destroy(gameObject);
        gameOverMenu.SetActive(true);
    }

    IEnumerator Immortality()
    {
        isImmortal = true;
        playerRenderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(1.0f);
        canMove = true;
        yield return new WaitForSeconds(1.0f);
        playerRenderer.material.SetColor("_Color", Color.white);
        isImmortal = false;
    }
}