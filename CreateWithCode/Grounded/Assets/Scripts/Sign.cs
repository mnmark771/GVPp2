using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Sign : MonoBehaviour
{
    public GameObject interactPanel;
    public GameObject player;
    public TextMeshProUGUI interactText;
    private int textNumber = 1;
    private int signLength = 0;
    private bool isInteracting;
    public string aText = "";
    public string bText = "";
    public string cText = "";
    public string dText = "";
    public string eText = "";
    public string fText = "";
    public string gText = "";
    private List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        textList.Add(aText);
        textList.Add(bText);
        textList.Add(cText);
        textList.Add(dText);
        textList.Add(eText);
        textList.Add(fText);
        textList.Add(gText);
        textList.RemoveAll(blank => blank == "");
        signLength = textList.Count;
        interactText.text = textList[textNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInteracting)
        {
            NextText();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Interact()
    {
        if (!isInteracting)
        {
            textNumber = 0;
            interactText.text = textList[textNumber];
            isInteracting = true;
            Time.timeScale = 0;
            interactPanel.SetActive(true);
        }
    }

    public void Quit()
    {
        Time.timeScale = 1;
        isInteracting = false;
        interactPanel.SetActive(false);
        StartCoroutine(player.GetComponent<PlayerController>().SignWait());
    }

    private void NextText()
    {
        textNumber++;
        if (textNumber < signLength)
        {
            interactText.text = textList[textNumber];
        }

        else
        {
            Quit();
        }
    }
}
