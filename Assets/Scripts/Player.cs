using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj; //Objeto estatico, jugador. Se inicializa en Awake.
    public GameObject sword;

    public int lives = 3; //vidas del jugador

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isAttacking = false;

    public bool isImune = false;

    public float speed = 5f; 
    public float jumpForce = 10f;
    public float movHor;

    public float imuneTimeCnt = 0f;
    public float imuneTime = 0.5f;

    public LayerMask groundLayer; //Se crea Layer "Ground" y se asigna en este mismo srcript (Player) 

    public float radius = 0.3f; //0.3f
    public float groundRayDist = 0.5f; //0.5f

    //variables para el ataque
    public float lastHit;
    public float spaceBtwnHit = 0.05f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    private void Awake()
    {
        obj = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //obtener en una variable el acceso a todas las propiedasdes del componente. !!!--> Gravity Scale = 2
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.obj.gamePause)
        {
            movHor = 0.0f;
            return;
        }

        //Obtiene los valores 1 para derecha y -1 para izquierda y los gurda en la varibale movHor
        movHor = Input.GetAxisRaw("Horizontal");

        isMoving = (movHor != 0); // el personaje se mueve?

        //Genera un circulo para saber si está en el suelo
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        //Si se preciona espacio ejecuta saltar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            Hit();
            AudioController.obj.PlayHit();
            isAttacking = true;
        }
        else
            isAttacking = false;

        //Envía booleanos al animator
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isAttacking", isAttacking);

        //Dirección de movimiento horizontal
        Flip(movHor);

        if (isImune)
        {
            spr.enabled = !spr.enabled;
            imuneTimeCnt -= Time.deltaTime;
            if (imuneTimeCnt <= 0.0f)
            {
                isImune = false;
                spr.enabled = true;
            }
        }

    }

    private void FixedUpdate()
    {
        //Manipula el movimiento horizontal
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y); //manipula la velocidad/movimiento del jugapor en función a la variable movimiento y velocidad, "y" se queda igual

        /* //Disparo
        if (Input.GetButtonDown("Fire1") && Time.time > lastHit + spaceBtwnHit)
        {
            Hit();
            lastHit = Time.deltaTime;
        } */

    }

    public void goImune()
    {
        isImune = true;
        imuneTimeCnt = imuneTime;
    }

    private void Hit()
    {
        Vector3 direc;
        if (transform.localScale.x == 1.0f)
        {
            direc = Vector2.right;
        }
        else
        {
            direc = Vector2.left;
        }
        //se istancia el sword, poniendo un ligero desface de 0.29 para evitar ese desface en la animación
        GameObject sword_ = Instantiate(sword, (transform.position - new Vector3(0.29f, 0, 0)) + direc * 1.3f, Quaternion.identity);

        sword_.GetComponent<Sword>().SetDirection(direc);
    }

    void Jump()
    {
        if (!isGrounded) return; //Si no está tocando el piso no hace nada
        AudioController.obj.PlayJump();
        rb.velocity = Vector2.up * jumpForce; //Agrega fuerza hacia arriba
    }

    void Flip(float movHor)
    {
        //To left or right?
        if (movHor < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (movHor > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    internal void AddLive()
    {
        AudioController.obj.PlayAddLive();

        if (lives < Game.obj.mxLives)
        {
            lives++;
            UIManager.obj.UpdateLives();
        }
    }

    public void Damage()
    {
        AudioController.obj.PlayPlayerDamage();
        lives--;
        goImune();
        UIManager.obj.UpdateLives();
        if (lives == 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        lives = 0;
        Debug.Log("vidas: "+ lives);
        UIManager.obj.gamePause = true;
        AudioController.obj.PlayGameOver();
        anim.SetBool("dead", true);
        //Destroy(gameObject, 0.9f); //0.9f
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
