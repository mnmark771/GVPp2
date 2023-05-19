using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        if (player.GetComponent<PlayerController>().keys > 0)
        {
            player.GetComponent<PlayerController>().keys -= 1;
            player.GetComponent<PlayerController>().UpdateUI();
            Destroy(gameObject);
        }
    }
}
