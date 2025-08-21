using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palavras : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D box;

    public int Score;

    public string nomeDaPalavra;

    // Start is called before the first frame update
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

            if (!string.IsNullOrEmpty(nomeDaPalavra))
            {
                GlBoardController.instance.TrackCorrectWord(nomeDaPalavra);
            }

            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();
           
            Destroy(gameObject, 0.25f);
        }
    }
}
