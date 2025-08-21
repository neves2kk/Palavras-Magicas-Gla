using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public string[] dialogueAux;

    public float wordSpeed;
    public bool flagFrase;
    public BoxCollider2D box;

    private void Start()
    {
        flagFrase = false;
        box = GetComponent<BoxCollider2D>();
    }


    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        flagFrase = true;
    }

    public void NextLine()
    {
        
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!dialoguePanel.activeInHierarchy)
            {
                box.enabled  = false;
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(TimeDestroyerPopup());
        }
    }

    IEnumerator TimeDestroyerPopup()
    {   
        while(!flagFrase)
        {
            yield return new WaitForSeconds(0f);
        } 
        yield return new WaitForSeconds(3f);
        zeroText();
        Destroy(gameObject);
    }
}

