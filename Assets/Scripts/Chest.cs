using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{    
    public static Chest instance;
    public int end;
    public BoxCollider2D box;

    void Start()
    {
        instance = this;
        box = GetComponent<BoxCollider2D>();

    }

    public void Compare()
    {
        // Desativa o colisor da caixa
        box.enabled = false;
        
        // Compara se o valor da pontuação foi alcançado
        if(GameController.instance.totalScore >= end)
        {
            box.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.WinGame();

        }
    }
}
