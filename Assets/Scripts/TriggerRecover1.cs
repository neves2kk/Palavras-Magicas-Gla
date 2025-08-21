using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRecover1 : MonoBehaviour
{
    // Função da maçã verde para recuperar vida
    // Chama a biblioteca de HeatSystem, que fica atrelada ao objeto Player.
    public HeartSystem heart;

    // Criando uma função privada, que verifica colisões entre a tag "Player" e o trigger do Boxcollider2D.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            // Verifica se a vida temporária:
            // for menor que a vida máxima, incrementamos a vida temporária.
            if (heart.life < heart.max_life)
            {
                heart.life++;
            }

            // Destruindo o objeto (Maçã)
            Destroy(gameObject);
        }
    }
}
        


    


