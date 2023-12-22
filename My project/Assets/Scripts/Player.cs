using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public int health;
    public float jumpForce;
    [Header("Booleans")]
    public bool isGrounded;
    public bool isJumping;
    [Header("Others")]
    public Animator anim;
    public Rigidbody2D rig;
    public SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Jump();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        
        float inputX = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(inputX * speed, rig.velocity.y);

        if (inputX > 0)
        {
            anim.SetInteger("transition", 1);
            sprite.flipX = false;
        }
        else if (inputX < 0)
        {
            anim.SetInteger("transition", 1);
            sprite.flipX = true;
        }
        if(inputX == 0)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
                anim.SetInteger("transition", 2);
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
