using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public int health;
    [Header("Booleans")]
    public bool isGrounded;
    [Header("Others")]
    public Animator anim;
    public Rigidbody2D rig;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(inputX * speed, rig.velocity.y);
    }
}
