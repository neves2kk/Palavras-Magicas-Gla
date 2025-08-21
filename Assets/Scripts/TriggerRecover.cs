using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRecover : MonoBehaviour
{
    // Chama a biblioteca de HeatSystem, que fica atrelada ao objeto Player.
    public HeartSystem heart;

    // Criando uma função privada, que verifica colisões entre a tag "Player" e o trigger do Boxcollider2D.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            // Verifica se a vida temporária:
            // for igual que a vida máxima, incrementamos tanto a vida máxima como a vida temporária.
            if(heart.life == heart.max_life)
            {
                heart.max_life++;
                heart.life++;
            }
            // for menor que a vida máxima, incrementamos +1 em vida máxima e recuperamso a vida total do player.
            if (heart.life < heart.max_life)
            {
                heart.life = heart.max_life;
            }

            // Destruindo o objeto (Maçã)
            Destroy(gameObject);
        }
    }
}
        


    


