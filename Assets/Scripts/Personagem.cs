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

    [Header("Detecção de Inatividade")]
    public float idleThreshold = 5f;
    private float idleTimer;
    private bool isIdle;

    void Start()
    {
        Instance = this;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flagIsGround = false;
    }

    void Update()
    {
        Move();
        Verticalmovement = joy.Vertical; 
        if(Verticalmovement > 0.2f && !isJumping)
        {
            Jump();
        }
        
        if (Chest.instance != null)
        {
            Chest.instance.Compare();
        }

        CheckForIdle();
    }

    void CheckForIdle()
    {
        if (rig.velocity.magnitude < 0.1f)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleThreshold && !isIdle)
            {
                isIdle = true;
                GameController.instance.RegistrarEventoIdle(idleThreshold, transform.position);
            }
        }
        else
        {
            idleTimer = 0;
            isIdle = false;
        }
    }

    void Move()
    {
        if(StopMove) 
        {
            Horizontalmovement = (joy.Horizontal * -1);
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0f;
            rig.isKinematic = true;
            anim.SetBool("run", false);
            anim.SetBool("jump", false);

            if(Input.GetMouseButtonDown(0))
            {
                if (Dialogue1.instance != null) Dialogue1.instance.wordSpeed = 0f;
                StopMove = false;
                GameController.instance.StopTalk();
            }
        }
   
        if(!StopMove)
        {
            rig.isKinematic = false;
            Vector3 movement;
            Horizontalmovement = joy.Horizontal;
            movement = new Vector3(Horizontalmovement, 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;
            
            if(Horizontalmovement > 0){
                transform.eulerAngles = new Vector3(0f,0f,0f);
                anim.SetBool("run", true);
            } else if(Horizontalmovement < 0){
                transform.eulerAngles = new Vector3(0f,180f,0f);
                anim.SetBool("run", true);
            } else {    
                anim.SetBool("run", false);
            }
        }
    }

    void Jump()
    {       
        if(!isJumping && flagIsGround)
        {     
            rig.velocity = new Vector2(rig.velocity.x, 0f);
            rig.AddForce(new Vector2(0f, JumpForce),ForceMode2D.Impulse);
            anim.SetBool("jump", true);        
        }
    }

    void OnCollisionEnter2D(Collision2D collison)
    {  
        if(collison.gameObject.layer == 8)
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

    void OnCollisionExit2D(Collision2D collison)
     {
        if(collison.gameObject.layer == 8)
        {
            isJumping = true;
        }  
     }
    public void Damage()
    {
        GameController.instance.RegistrarDanoInimigo(transform.position);

        anim.SetTrigger("damage");
    }

    public void Dead()
    {
        anim.SetBool("die", true); 
        Destroy(gameObject, 0.75f);
        GameController.instance.ShowGameOver();
    }
}