using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int coins;
    public int kabooms;
    public int healthPotions;
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
        var playerScript = player.GetComponent<PlayerController>();
        playerScript.kabooms += kabooms;
        playerScript.healthPotions += healthPotions;
        playerScript.coins += coins;
        Destroy(gameObject);
        playerScript.UpdateUI();
    }
}
