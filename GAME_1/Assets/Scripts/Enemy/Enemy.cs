using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f; // �������� �������� �����
    public float attackRadius = 1.5f; // ������ �����
    public float returnRadius = 7f; // ������ �������������
    public float attackDamage = 10f; // ���� �� �����
    public float attackCooldown = 1f; // ����� ����� �������

    public Player playerScript;
    private Vector3 shotpos;
    private Transform player; // ������ �� ������
    private float lastAttackTime; // ����� ��������� �����
    private Rigidbody2D rb; // Rigidbody2D ��� ��������
    private Vector3 startposition;

    public float health_enemy = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startposition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ����
    }
    private void Update()
    {
        MoveTowardsPlayer();

        if (Vector2.Distance(transform.position, player.position) < attackRadius)
        {
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) < returnRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed; 
        }
        else
        {
            rb.MovePosition(startposition); //��� � ��������� �����
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            /*
            Vector2 dir_shoot = player.position - transform.position;
            if (dir_shoot.x != 0)
                dir_shoot.x = dir_shoot.x / Mathf.Abs(dir_shoot.x);
            if (dir_shoot.y != 0)
                dir_shoot.y = dir_shoot.y / Mathf.Abs(dir_shoot.y);
            RaycastHit2D hit = Physics2D.Raycast(shotpos, dir_shoot);
            if (hit.collider.gameObject.tag == "Player")
            {
                Player pl = hit.collider.GetComponent<Player>();
                pl.TakeDamage_hero(attackDamage);
            }
            */
            if (playerScript != null)
            {
                playerScript.TakeDamage_hero(attackDamage);
            }
            Debug.Log("Attack! Damage: " + attackDamage);
            lastAttackTime = Time.time;
        }
    }
    public void TakeDamage_enemy(float damage)
    {
        health_enemy -= damage; // ��������� ��������
        Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health_enemy);

        if (health_enemy <= 0)
        {
            Die(); // �������� ����� ������, ���� �������� ���� ��� ����� ����
        }
    }
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // ������ ������ ����� (��������, ������������ �����, �������� � �.�.)
    }
}
