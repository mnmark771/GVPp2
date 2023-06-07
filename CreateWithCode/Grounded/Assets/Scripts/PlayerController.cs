using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayers;
    public LayerMask interactableLayers;

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

    public TextMeshProUGUI keysText;
    public TextMeshProUGUI kaboomsText;
    public TextMeshProUGUI healthPotionsText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shopKaboomsText;
    public TextMeshProUGUI shopHealthPotionsText;
    public TextMeshProUGUI shopCoinsText;

    public float knockBack;
    private float rotationSpeed = 360;
    private float speed = 5.0f;
    private float playerRange = 0.5f;
    private float attackRate = 1.0f;
    private float nextAttackTime = 0.0f;
    private float playerDamage = 1.0f;
    private float fillTime = 0.0f;

    public float maxHealth = 3.0f;
    private float currentHealth;

    public int keys = 0;
    public int coins = 0;
    public int kabooms = 0;
    public int healthPotions = 0;
    public int currentItemNumber = 0;
    public int masterKey = 0;

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
        PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
        LoadStats();
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
        UpdateUI();
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

            else if (enemy.CompareTag("Wandering Enemy"))
            {
                enemy.GetComponent<WanderEnemy>().TakeDamage(playerDamage);
            }

            else if (enemy.CompareTag("Breakable"))
            {
                enemy.GetComponent<Breakable>().TakeDamage(playerDamage);
                missed = true;
            }

            else if (enemy.CompareTag("Dragon Boss"))
            {
                enemy.GetComponent<DragonBoss>().TakeDamage(playerDamage);
                missed = false;
            }

            else if (enemy.CompareTag("Boss Two"))
            {
                enemy.GetComponent<BossTwo>().TakeDamage(playerDamage);
                missed = false;
            }

            else if (enemy.CompareTag("Head"))
            {
                enemy.GetComponent<BossTwoHead>().TakeDamage(playerDamage);
                missed = false;
            }

            else if (enemy.CompareTag("Wizard Enemy"))
            {
                enemy.GetComponent<WizardEnemy>().TakeDamage(playerDamage);
                missed = false;
            }

            else
            {
                missed = true;
            }
        }
    }

    void Interaction()
    {
        Collider[] interactingWith = Physics.OverlapSphere(rangePosition.position, playerRange, interactableLayers);
        foreach(Collider interactable in interactingWith)
        {
            if (interactable.CompareTag("Sign"))
            {
                interactable.GetComponent<Sign>().Interact();
                inMenu = true;
            }

            else if (interactable.CompareTag("Chest"))
            {
                interactable.GetComponent<Chest>().Open();
            }

            else if (interactable.CompareTag("Shop"))
            {
                Time.timeScale = 0;
                shopPanel.SetActive(true);
                inMenu = true;
            }

            else if (interactable.CompareTag("Key Door"))
            {
                interactable.GetComponent<KeyDoor>().Open();
            }

            else if (interactable.CompareTag("Door"))
            {
                Destroy(GameObject.Find("Start Portal"));
                Destroy(interactable.gameObject);
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

        if (Input.GetKeyDown(KeyCode.E) && !inMenu)
        {
            Interaction();
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
        keysText.text = keys.ToString();

        shopCoinsText.text = coins.ToString();
        shopHealthPotionsText.text = healthPotions.ToString();
        shopKaboomsText.text = kabooms.ToString();
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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boomerang"))
        {
            if (other.gameObject.GetComponent<Boomerang>().comeBack == true)
            {
                canBoomerang = true;
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boomerang"))
        {
            if (other.gameObject.GetComponent<Boomerang>().comeBack == true)
            {
                canBoomerang = true;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Coin"))
        {
            coins += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Kaboom Pickup"))
        {
            kabooms += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Life Pickup"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 1;
                playerHealthbar.value = currentHealth;
                Destroy(other.gameObject);
            }
        }

        else if (other.CompareTag("Key"))
        {
            keys += 1;
            UpdateUI();
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("One Up"))
        {
            maxHealth += 1;
            currentHealth = maxHealth;
            playerHealthbar.maxValue = maxHealth;
            playerHealthbar.value = currentHealth;
            Destroy(other.gameObject);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike Trap"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("Wandering Enemy"))
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

    public IEnumerator SignWait()
    {
        yield return new WaitForSeconds(0.5f);
        inMenu = false;
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("keys", keys);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("kabooms", kabooms);
        PlayerPrefs.SetInt("healthPotions", healthPotions);
        PlayerPrefs.SetFloat("currentHealth", currentHealth);
        PlayerPrefs.SetFloat("maxHealth", maxHealth);
        PlayerPrefs.SetInt("masterKey", masterKey);
    }

    public void LoadStats()
    {
        keys = PlayerPrefs.GetInt("keys");
        coins = PlayerPrefs.GetInt("coins");
        kabooms = PlayerPrefs.GetInt("kabooms");
        healthPotions = PlayerPrefs.GetInt("healthPotions");
        currentHealth = PlayerPrefs.GetFloat("currentHealth");
        maxHealth = PlayerPrefs.GetFloat("maxHealth");
        masterKey = PlayerPrefs.GetInt("masterKey");
    }
}