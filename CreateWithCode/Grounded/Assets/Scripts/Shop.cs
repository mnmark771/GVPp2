using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject player;
    private int kaboomsPrice = 5;
    private int healthPotionPrice = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            player.GetComponent<PlayerController>().inMenu = false;
        }
    }

    public void BuyKaboom()
    {
        if (player.GetComponent<PlayerController>().coins >= kaboomsPrice)
        {
            player.GetComponent<PlayerController>().kabooms += 1;
            player.GetComponent<PlayerController>().coins -= kaboomsPrice;
            player.GetComponent<PlayerController>().UpdateUI();
        }
    }

    public void BuyHealthPotion()
    {
        if (player.GetComponent<PlayerController>().coins >= healthPotionPrice)
        {
            player.GetComponent<PlayerController>().healthPotions += 1;
            player.GetComponent<PlayerController>().coins -= healthPotionPrice;
            player.GetComponent<PlayerController>().UpdateUI();
        }
    }
}
