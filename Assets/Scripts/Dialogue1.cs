using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue1 : MonoBehaviour
{
    public static Dialogue1 instance;

    public GameObject dialoguePanel;            // Popup da fala do NPC
    public Text dialogueText;                   // Texto onde será a fala
    public string[] dialogue;                   // String que receberá a fala do NPC
    private int index;                          // Indice da array de strings
    public string[] dialogueAux;

    public float wordSpeed;                     // Velocidade da fala
    public bool flagFrase;                      // Bandeira que verifica se o personagem está próximo da fala
    public BoxCollider2D box;                   // Colisor do objeto de cena 

    private void Start()
    {
        instance = this;
        flagFrase = false;                           
        box = GetComponent<BoxCollider2D>();
    }

    // Esvazia o conteúdo da fala
    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        box.enabled  = false;
    }

    // Estrutura de repetição da escrita (Fazendo a fala sair letra por letra)
    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        flagFrase = true;
    }

    // Verifica se ainda há texto para ser escrito
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

    //  Verifica se o player está em contato com o colisor 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                GameController.instance.Talking();
            }
        }
    }

    // Verifica se o player saiu de contato com o colisor 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TimePopup());
        }
    }

    // Timer para o Popup ser detruido 
    IEnumerator TimePopup()
    {   
        while(!flagFrase)
        {
            yield return new WaitForSeconds(0f);
        } 
        yield return new WaitForSeconds(1.5f);
        zeroText();
    }
}
