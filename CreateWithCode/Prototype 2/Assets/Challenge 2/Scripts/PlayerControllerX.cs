using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float dogDelay = 1;
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (dogDelay == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                dogDelay = 0;
            }
        }

        dogDelay += 1 * Time.deltaTime;

        if (dogDelay > 1)
        {
            dogDelay = 1;
        }

    }
}
