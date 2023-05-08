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
    public GameObject boomerangPrefab;
    public GameObject kaboomPrefab;
    public GameObject gameOverMenu;
    public GameObject itemUI;
    public GameObject shopPanel;

    public Transform rangePosition;

    public TextMeshProUGUI kaboomsText;
    public TextMeshProUGUI healthPotionsText;
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
    public int coins = 0;
    public int kabooms = 0;
    public int healthPotions = 0;
    public int currentItemNumber = 0;
    private float fillTime = 0.0f;

    private bool isGameActive = true;
    private bool missed = true;
    public bool hasBoomerang;
    public bool isImmortal;
    public bool inMenu;
    private bool canMove = true;
    private bool canAttack = true;
    private bool canBoomerang = true;
    private Vector3 movementDirection;

    public string currentItem = "";

    private List<string> itemList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add("Kaboom");
        itemList.Add("Boomerang");
        itemList.Add("Health Potion");
        currentItem = "Kaboom";
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentItem == "Kaboom")
            {
                UseKaboom();
            }

            else if (currentItem == "Boomerang" && canBoomerang)
            {
                UseBoomerang();
            }

            else if (currentItem == "Health Potion")
            {
                UseHealthPotion();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentItemNumber = 0;
            currentItem = itemList[0];
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentItemNumber = 1;
            currentItem = itemList[1];
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentItemNumber = 2;
            currentItem = itemList[2];
            UpdateUI();
        }
        
        if (Input.mouseScrollDelta.y != 0)
        {
            currentItemNumber += Mathf.CeilToInt(Input.mouseScrollDelta.y);
            if (currentItemNumber < 0)
            {
                currentItemNumber = itemList.Count - 1;
            }

            else if (currentItemNumber > itemList.Count -1)
            {
                currentItemNumber = 0;
            }
            currentItem = itemList[currentItemNumber];
            UpdateUI();
        }
    }
    
    public void UpdateUI()
    {
        itemUI.transform.GetChild(0).gameObject.SetActive(false);
        itemUI.transform.GetChild(1).gameObject.SetActive(false);
        itemUI.transform.GetChild(2).gameObject.SetActive(false);
        itemUI.transform.GetChild(currentItemNumber).gameObject.SetActive(true);
        healthPotionsText.text = healthPotions.ToString();
        kaboomsText.text = kabooms.ToString();
        coinsText.text = coins.ToString();
    }

    private void UseKaboom()
    {
        if (kabooms > 0)
        {
            Instantiate(kaboomPrefab, transform.position, transform.rotation);
            kabooms -= 1;
            UpdateUI();
        }
    }

    private void UseBoomerang()
    {
        canBoomerang = false;
        Instantiate(boomerangPrefab, transform.position, transform.rotation);
    }

    private void UseHealthPotion()
    {
        if (currentHealth < maxHealth && healthPotions > 0)
        {
            currentHealth++;
            playerHealthbar.value = currentHealth;
            healthPotions -= 1;
            UpdateUI();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Kaboom Pickup"))
        {
            kabooms += 1;
            UpdateUI();
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
                other.GetComponent<Sign>().Interact();
                inMenu = true;
            }
        }

        if (other.CompareTag("Chest"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Chest>().Open();
            }
        }

        if (other.CompareTag("Shop"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                shopPanel.SetActive(true);
                inMenu = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike Trap"))
        {
            TakeDamage(1);
        }

        else if (collision.gameObject.CompareTag("Boomerang"))
        {
            if (collision.gameObject.GetComponent<Boomerang>().comeBack)
            {
                canBoomerang = true;
                Destroy(collision.gameObject);
            }
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