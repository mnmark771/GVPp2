using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public int coins;
    public int kabooms;
    public int healthPotions;
    public int bossNumberPlusOne = 0;
    public bool bossChest;
    private bool isOpen;
    public TextMeshProUGUI chestText;
    private GameObject player;
    public GameObject endPortal;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            var playerScript = player.GetComponent<PlayerController>();
            playerScript.kabooms += kabooms;
            playerScript.healthPotions += healthPotions;
            playerScript.coins += coins;
            playerScript.UpdateUI();
            StartCoroutine(TellPlayer());
            if (bossChest)
            {
                endPortal.SetActive(true);
                if (playerScript.masterKey < bossNumberPlusOne)
                {
                    playerScript.masterKey = bossNumberPlusOne;
                }
                playerScript.SaveStats();
            }
        }
    }

    IEnumerator TellPlayer()
    {
        if (bossChest)
        {
            chestText.text = "Dungeon Key";
            yield return new WaitForSeconds(1.0f);
        }
        chestText.text = "Coin x" + coins;
        yield return new WaitForSeconds(1.0f);
        chestText.text = "Health Potion x" + healthPotions;
        yield return new WaitForSeconds(1.0f);
        chestText.text = "Kaboom x" + kabooms;
        yield return new WaitForSeconds(1.0f);
        chestText.text = "";
        Destroy(gameObject);
    }
}
