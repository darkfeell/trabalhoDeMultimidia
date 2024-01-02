using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    public enum MoveDirection{ Right, Left }
    [SerializeField] MoveDirection m_moveDirection;
    [SerializeField] float m_speed;
    [SerializeField] Timer m_timer;
    [SerializeField] float m_health;
    private void Update()
    {
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
        m_moveDirection = m_moveDirection == MoveDirection.Left ? MoveDirection.Right : MoveDirection.Left;
    }

    public void Damage(float damageValue)
    {
        m_health -= damageValue;
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
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