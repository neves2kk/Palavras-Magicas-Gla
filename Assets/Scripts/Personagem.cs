using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public static Personagem Instance;
    [SerializeField] private FixedJoystick joy;

    [SerializeField] public float Speed;
    [SerializeField] public float JumpForce;
    
    private float Horizontalmovement;
    private float Verticalmovement;

    [SerializeField] private DetectionButton[] butonsDirections;

    public int MoveDirection;

    public bool isJumping;
    public bool doubleJump;
    public bool StopMove;
    
    [SerializeField] public Rigidbody2D rig;
    [SerializeField] private Animator anim;

    public bool flagIsGround;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;                        //estabelecendo uma instanciação para transformar o Personagem em um objeto
        rig = GetComponent<Rigidbody2D>();      //receber o rididbody armazenado
        anim = GetComponent<Animator>();        //receber o animator armazenado
        flagIsGround = false;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        // Jump();
        Verticalmovement = joy.Vertical; 
        if(Verticalmovement > 0.2f && !isJumping)
        {
            Jump();
        }
        
        Chest.instance.Compare();
    }

    // Desktop
    // Função para os movimentos do personagem, direita e esquerda. 
  
  /**
    void Move()
    {
        if(StopMove) 
        {
            // Desativa a movimentação
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0f;
            rig.isKinematic = true;
            anim.SetBool("run", false);
            anim.SetBool("jump", false);

            // Reativa a movimentação
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Dialogue1.instance.wordSpeed = 0f;
                StopMove = false;
                GameController.instance.StopTalk();
            }
        }
        
        if(!StopMove)
        {
            // Altera retorna o valor de movement, sendo x no Vector3, através de Input.GetAxis("Horizontal")
            // Atualiza o valor do deslocamento retornando tranform.position
            rig.isKinematic = false;


            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;
        
            // Anda para direita
            if(Input.GetAxis("Horizontal")> 0f) 
            {
                transform.eulerAngles = new Vector3(0f,0f,0f);
                anim.SetBool("run", true);
            }

            // Anda para a esquerda e faz a uma mudança de animação (Inverte o eulerAngles ir para a esquerda) 
            if(Input.GetAxis("Horizontal") < 0f) 
            {
                transform.eulerAngles = new Vector3(0f,180f,0f);
                anim.SetBool("run", true);
            }

            // Verifica se o personagem está andando, para realizar a animação de idle, retornando a condição de run um valor booleano

            if(Input.GetAxis("Horizontal") == 0f)
            {
                anim.SetBool("run", false);
            }
        }
    }
        
     // Função para os movimentos do personagem, pulo.
    void Jump()
    {
        if(Input.GetButtonDown("Jump")) // !isJump inverte o valor e verfica se é falso; pode usar também isJump == false 
        {
            if(!isJumping && flagIsGround)
            {
                rig.AddForce(new Vector2(0f, JumpForce),ForceMode2D.Impulse);
                anim.SetBool("jump", true);
            }   
        }
    }
**/


    // Android
    // Função para os movimentos do personagem, direita e esquerda. 
  
    void Move()
    {
        if(StopMove) 
        {
            // Desativa a movimentação
            Horizontalmovement = (joy.Horizontal * -1);
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0f;
            rig.isKinematic = true;
            anim.SetBool("run", false);
            anim.SetBool("jump", false);

            // Reativa a movimentação
            if(Input.GetMouseButtonDown(0))
            {
                Dialogue1.instance.wordSpeed = 0f;
                StopMove = false;
                GameController.instance.StopTalk();
            }
        }
   
        // Altera retorna o valor de movement, sendo x no Vector3, através de Input.GetAxis("Horizontal")
        // Atualiza o valor do deslocamento retornando tranform.position
        // Reativa a movimentação

        if(!StopMove)
            
            rig.isKinematic = false;

            Vector3 movement;
            Horizontalmovement = joy.Horizontal;
            movement = new Vector3(Horizontalmovement, 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;
            
            if(Horizontalmovement > 0){
                transform.eulerAngles = new Vector3(0f,0f,0f);
                anim.SetBool("run", true);

            }else if(Horizontalmovement < 0){

                transform.eulerAngles = new Vector3(0f,180f,0f);
                anim.SetBool("run", true);

            } else {    
                anim.SetBool("run", false);
        }
    }


    // Função para os movimentos do personagem, pulo.
    void Jump()
    {       
        if(!isJumping && flagIsGround)
        {     
            rig.velocity = new Vector2(rig.velocity.x, 0f);
            rig.AddForce(new Vector2(0f, JumpForce),ForceMode2D.Impulse);
            anim.SetBool("jump", true);        
        }
    }


    // Detectando quando o personagem encosta em alguma coisa (Utilizando layers e tags)
    void OnCollisionEnter2D(Collision2D collison)
    {  
        if(collison.gameObject.layer == 8) // 8 por causa do layer
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }  

        if(collison.gameObject.tag == "fall")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

    }

    // Detectando quando personagem deixa de encostar em alguma coisa (Utilizando layers e tags)
    void OnCollisionExit2D(Collision2D collison)
     {
        if(collison.gameObject.layer == 8) // 8 por causa do layer
        {
            isJumping = true;
        }  
     }

    // Criando uma função para animar o personagem tomando dano, no Script TriggerDamage.cs   
    public void Damage()
    {
        anim.SetTrigger("damage");
    }

    // Criando uma função para animar o personagem tomando dano, no Script HeartSystem.cs   
    public void Dead()
    {
        anim.SetBool("die", true); 
        Destroy(gameObject, 0.75f);
        GameController.instance.ShowGameOver();
    }
}
