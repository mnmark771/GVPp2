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
    public TextMeshProUGUI chestText;
    private GameObject player;
    
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
        StartCoroutine(TellPlayer());
        var playerScript = player.GetComponent<PlayerController>();
        playerScript.kabooms += kabooms;
        playerScript.healthPotions += healthPotions;
        playerScript.coins += coins;
        playerScript.UpdateUI();
    }

    IEnumerator TellPlayer()
    {
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
