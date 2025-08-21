using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public float speed;
    public bool ground;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        anim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Flip()
    {
        // Inverte a posição do sprite 
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    void Move()
    {
        // Movimenta para a direita, multiplicando pelo valor da velocidade
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Desenha um verificador para saber se o player se encontra no chão
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        // Condicionall para inverter a velocidade do inimigo para ele não cair
        if(ground == false)
        {
            speed *= -1;
        }

        if(speed > 0 && !facingRight)
        {
            Flip();
        }
        else if(speed < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Damage()
    {
        anim.SetTrigger("damage");
    }

    public void Damage1()
    {
        anim.SetTrigger("air");
    }
    
}
