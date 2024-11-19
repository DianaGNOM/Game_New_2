using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visual : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2;
    private SpriteRenderer sprite_renderer;
    private Vector3 movementDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        movementDirection = rb2.velocity;

        if (movementDirection.x < 0)
        {
            //animator.SetTrigger("Left"); // Влево
            sprite_renderer.flipX = true;

        }
        else if (movementDirection.x > 0)
        {
            //animator.SetTrigger("Right"); // Вправо
            sprite_renderer.flipX = false;
        }
    }

}
