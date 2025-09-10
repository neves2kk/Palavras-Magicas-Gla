using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum para definir os tipos de palavra
public enum TipoPalavra
{
    Correta,
    Incorreta
}

public class Palavras : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D box;

    [Header("Configuração da Palavra")]
    public string nomeDaPalavra;
    public TipoPalavra tipo;
    public int scoreCorreto = 1; 
    public int danoIncorreto = 1; 

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

            Vector2 posicaoDaPalavra = transform.position;

            if (tipo == TipoPalavra.Correta)
            {
                GameController.instance.RegistrarPalavraCorreta(nomeDaPalavra, posicaoDaPalavra);
                
                GameController.instance.totalScore += scoreCorreto;
                GameController.instance.UpdateScoreText();
            }
            else if (tipo == TipoPalavra.Incorreta)
            {
                GameController.instance.RegistrarPalavraIncorreta(nomeDaPalavra, posicaoDaPalavra);
                
                HeartSystem vidaDoJogador = collider.GetComponent<HeartSystem>();
                if (vidaDoJogador != null)
                {
                    vidaDoJogador.life -= danoIncorreto;
                }
            }
           
            Destroy(gameObject, 0.25f);
        }
    }
}