using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public int health;
    public float jumpForce;

    [Header("Move Settings")]
    Vector2 inputDirection;
    float lookingDirection;
    Vector2 moveDirection;

    [Header("Booleans")]
    bool isMoving;
    bool isJumping;
    bool isAttacking;

    [Header("Others")]
    Animator anim;
    Rigidbody2D rig;
    SpriteRenderer sprite;

    [Header("SFX")]
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip stepSFX;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InputListener();
        GFXController();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputListener()
    {
        inputDirection.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Set(speed * inputDirection.x, rig.velocity.y);
        lookingDirection = inputDirection.x != 0 ? inputDirection.x : lookingDirection;
        isMoving = moveDirection.x != 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }
    
    void Move()
    {
        rig.velocity = moveDirection;
    }

    void Jump()
    {
        if (isJumping) return;
        AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
        Vector2 jumpDirection = new Vector2(rig.velocity.x, jumpForce);
        rig.velocity = jumpDirection;
        isJumping = true;
    }

    void Attack()
    {
        if (isAttacking) return;
        StartCoroutine(AttackExec());
    }

    void GFXController()
    {
        if (lookingDirection > 0)
        {
            sprite.flipX = false;
        }
        if (lookingDirection < 0)
        {
            sprite.flipX = true;
        }

        if (isAttacking)
        {
            anim.SetInteger("transition", 3);
            return;
        }

        if (isJumping)
        {
            anim.SetInteger("transition", 2);
            return;
        }

        if (isMoving)
        {
            anim.SetInteger("transition", 1);
        }
        else
        {
            anim.SetInteger("transition", 0);
        }
    }

    IEnumerator AttackExec()
    {
        isAttacking = true;
        float cacheSpeed = speed;
        speed = 0;
        AudioSource.PlayClipAtPoint(attackSFX, transform.position);


        yield return new WaitForSeconds(0.6f);


        speed = cacheSpeed;
        isAttacking = false;
    }

    public void StepSound()
    {
        AudioSource.PlayClipAtPoint(stepSFX, transform.position);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isJumping) return;
        if (!collision.gameObject.CompareTag("Ground")) return;
        if (rig.velocity.y <= 0.1f)
        {
            isJumping = false;
        }
    }
}
