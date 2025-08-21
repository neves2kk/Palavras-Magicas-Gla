using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int life;
    public int max_life;

    public Image[] heart;
    public Sprite cheio;
    public Sprite vazio;


    // Update is called once per frame
    void Update()
    {
        Health_logic();
        DeadState();
    }

    void Health_logic()
    {
        if (life > max_life)
        {
            life = max_life;
        }

        for (int i = 0; i < heart.Length; i++)
        {
            if (i < life)
            {
                heart[i].sprite = cheio;
            }
            else
            {
                heart[i].sprite = vazio;
            }

            if (i < max_life)
            {
                heart[i].enabled = true;
            }
            else 
            {
                heart[i].enabled = false;
            }
        }
    }

    void DeadState()
    {
        if(life <= 0)
        {
            GetComponent<Personagem>().enabled = false;
            Personagem.Instance.Dead();
        }
    }
}

