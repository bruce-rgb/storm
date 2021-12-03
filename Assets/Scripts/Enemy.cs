using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public int lives = 2;

    public float movHor = 1f;
    public float speed = 2f;

    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer; //Se crea Layer "Ground" y se asigna en este mismo srcript (Enemy) 

    public float frontGrndRayDist = 0.45f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    private RaycastHit2D hit;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); //obtener en una variable el acceso a todas las propiedasdes del componente.
    }

    // Update is called once per frame
    void Update()
    {
        //Evitar caer al precipicio
        isGroundFloor = (
            Physics2D.Raycast(
                new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
                new Vector3( movHor, 0, 0), frontGrndRayDist, groundLayer)
        );

        if (isGroundFloor) 
            movHor *= -1; //cambia orientación del enemigo al lado opuesto para evitar caer

        //Manejar choque con paredes
        if(Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor *= -1; //cambia orientación del enemigo al lado opuesto para evitar chocar

        //choque con otro enemigo
        hit = Physics2D.Raycast(
            new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z),
            new Vector3(movHor, 0, 0), frontDist);

        if (hit != null)
            if (hit.transform != null)
                if (hit.transform.CompareTag("Enemy"))
                    movHor *= -1;

    }

    void FixedUpdate()
    {
        anim.SetBool("isDamage", false);
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y); //manejar el movimiento de los enemigos
    }

    //NO SIRVE!! si se hecha a andar se activa is trigger en collider y "!" en linea 46: -->if (!isGroundFloor)<-- 
    //Llamado cuando este Collider2d/RigbBody2d collisiona con otro Collider2d/RigbBody2d. Cuando enemigo toca a player lo daña...
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Dañar personaje
        if (collision.gameObject.CompareTag("Player") && anim.GetBool("isDamage") == true)
        {
            Debug.Log("Daño a personaje");
            Player.obj.Damage();
        }
    } 

    //Cuando player toca a enemigo lo daña
    void OnTriggerEnter2D(Collider2D collision)
    {
        //dañar Enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    public void Damage()
    {
        Debug.Log("Daño a enemigo");
        lives--;
        anim.SetBool("isDamage", true);
        if (lives == 0)
        {
            
            anim.SetBool("dead", true);
            AudioController.obj.PlayKillEnemy();
            Game.obj.AddScore(scoreGive);
            //gameObject.SetActive(false); //destruye al enemigo
            Destroy(gameObject, 0.6f);
        }
    }
}
