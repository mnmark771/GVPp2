using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;
    public GameObject conditionalParent;
    public GameObject makeActive;

    public GameObject keyPrefab;

    public bool hasKey = false;
    public bool clearCondition = false;
    public bool childCondition = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clearCondition && enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null && enemy5 == null && enemy6 == null && enemy7 == null && enemy8 == null && enemy9 == null && enemy10 == null)
        {
            if (hasKey)
            {
                Instantiate(keyPrefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        if (childCondition && conditionalParent.transform.childCount == 0)
        {
            makeActive.SetActive(true);
        }
    }
}
