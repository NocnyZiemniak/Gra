using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 6f;
    public int damage = 10;

    // NOWE zmienne do timera ataku
    public float attackRate = 1f;       // co ile sekund zombie może uderzyć
    private float nextAttackTime = 0f;   // czas następnego ataku

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Goni gracza jeśli blisko
        if (distance <= detectionRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }
    }

    // Atak przy kontakcie z timerem
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprawdzenie, czy minął czas od ostatniego ataku
            if (Time.time >= nextAttackTime)
            {
                collision.gameObject.GetComponent<Health>()?.taking_damage(damage);
                Debug.Log("Atak");

                // ustawiamy czas kolejnego ataku
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}