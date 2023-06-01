using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour
{
    public Vector3 teleportTo;
    public string dungeon;

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
            other.GetComponent<PlayerController>().SaveStats();
            SceneManager.LoadScene(dungeon);
            //other.gameObject.transform.position = teleportTo;
        }
    }
}
