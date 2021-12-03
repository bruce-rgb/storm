using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2;
    private Vector2 dir;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        dir = direction;
        //To left or right?
        if (direction.x < 0.0f) transform.localScale = new Vector2(-1.0f, 1.0f);
        else if (direction.x > 0.0f) transform.localScale = new Vector2(1.0f, 1.0f);
    }

    public void DestroySword()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Damage();
        }

        //DestroySword();
    }
}
