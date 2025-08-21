using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavras : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D box;

    public int Score;
    
    // Campo para você definir o nome da palavra no Inspector da Unity
    public string nomeDaPalavra;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            box.enabled = false;

            // Notifica o GameController sobre a palavra correta que foi coletada
            if (!string.IsNullOrEmpty(nomeDaPalavra))
            {
                GameController.instance.RegistrarPalavraCorreta(nomeDaPalavra);
            }

            // Atualiza a pontuação e o texto na tela
            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();
           
            Destroy(gameObject, 0.25f);
        }
    }
}