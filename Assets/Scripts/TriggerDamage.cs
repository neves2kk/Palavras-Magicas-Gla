using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    // Chama a biblioteca de HeatSystem, que fica atrelada ao objeto Player.
    public HeartSystem heart;

    // Criando uma função privada, que verifica colisões entre a tag "Player" e o trigger do Boxcollider2D.
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            // Decrementando a vida temporária.
            heart.life--;

            // Verificando se a vida temporária chegou em 1, para realizar a animação de dano
            if(heart.life >= 1)
            {
                Personagem.Instance.Damage();
            }
        }
    }
}

