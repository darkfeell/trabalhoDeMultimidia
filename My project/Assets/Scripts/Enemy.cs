using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    public enum MoveDirection{ Right, Left }
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Animator m_animator;
    [SerializeField] MoveDirection m_moveDirection;
    [SerializeField] float m_speed;
    [SerializeField] Timer m_timer;
    [SerializeField] float m_health;
    private void Update()
    {
        m_spriteRenderer.flipX = m_moveDirection == MoveDirection.Right;
        m_timer.TimerElapse(Time.deltaTime);
        Move();
    }

    void Move()
    {
        transform.Translate(MoveFinalDirection(), Space.World);
        Vector2 MoveFinalDirection()
        {
            Vector2 moveDirectionVector = Vector2.right;
            if (m_moveDirection == MoveDirection.Left)
            {
                moveDirectionVector = Vector2.left;
            }
            return m_speed * Time.deltaTime * moveDirectionVector;
        }
    }

    public void Flip()
    {
        if (m_moveDirection == MoveDirection.Left)
        {
            m_moveDirection = MoveDirection.Right;
        }
        else
        {
            m_moveDirection = MoveDirection.Left;
        }
    }

    public void Damage(float damageValue)
    {
        m_health -= damageValue;
        m_animator.SetTrigger("Damage");
        StartCoroutine(StopMovingByTime(0.5f));
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StopMovingByTime(float secondsStop)
    {
        float cacheSpeed = m_speed;
        m_timer.ActiveTimer(false);
        m_speed = 0;
        yield return new WaitForSeconds(secondsStop);
        m_timer.ActiveTimer(true);
        m_speed = cacheSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
            damageble.Damage(1);
        }
    }
}

public interface IDamageble
{
    public void Damage(float damageValue);
}