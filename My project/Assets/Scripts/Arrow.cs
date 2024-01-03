using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float m_speed;
    [SerializeField] Timer m_lifeTime;
    void Start()
    {
        m_lifeTime.SetupTimer();
    }
    void Update()
    {
        transform.Translate(MoveDirection(), Space.Self);
        Vector3 MoveDirection()
        {
            return m_speed * Time.deltaTime * Vector3.right;
        }
        m_lifeTime.TimerElapse(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamageble>().Damage(1);
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            DeathArrow();
        }
    }

    public void DeathArrow()
    {
        Destroy(gameObject);
    }
}
