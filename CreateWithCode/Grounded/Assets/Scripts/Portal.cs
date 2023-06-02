using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public Vector3 teleportTo;
    public string dungeon;
    public bool needKey;
    public int needNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            if (needKey)
            {
                if (other.GetComponent<PlayerController>().masterKey >= needNumber)
                {
                    other.GetComponent<PlayerController>().SaveStats();
                    SceneManager.LoadScene(dungeon);
                }
            }

            else
            {
                other.GetComponent<PlayerController>().SaveStats();
                SceneManager.LoadScene(dungeon);
            }
            //other.gameObject.transform.position = teleportTo;
        }
    }
}
