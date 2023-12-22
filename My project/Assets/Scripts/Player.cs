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
    public bool isJumping;
    public bool isAttacking;
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
        playAttack();
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
            sprite.flipX = false;
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
        }
        else if (inputX < 0)
        {
            sprite.flipX = true;
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }

        }
        if(inputX == 0 && !isJumping)
        {
            anim.SetInteger("transition", 0);
        }
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                anim.SetInteger("transition", 2);
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
        }
        
    }
    void playAttack()
    {
        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetInteger("transition", 3);
            isAttacking = true;
            yield return new WaitForSeconds(3f);
            anim.SetInteger("transition", 0);
            isAttacking = false;
        }
    }
}
