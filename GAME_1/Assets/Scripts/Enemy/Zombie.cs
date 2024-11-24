using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 2f; // �������� �������� �����
    public float attackRange = 1.5f; // ��������� �����
    public float attackDamage = 10f; // ���� �� �����
    public float attackCooldown = 1f; // ����� ����� �������
    public float chaseDistance = 5f; // ���������� �������������
    public float stopDistance = 2f; // ���������� ����������

    private Transform player; // ������ �� ������
    private float lastAttackTime; // ����� ��������� �����
    private Rigidbody2D rb; // Rigidbody2D ��� ��������
    private Vector3 startingPosition; // ��������� ������� �����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ����
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D
        startingPosition = transform.position; // ���������� ��������� ������� �����
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���������, ��������� �� ����� � �������� ���������� �������������
        if (distanceToPlayer < chaseDistance)
        {
            MoveTowardsPlayer();

            // ���� ����� ������ � �����, �������
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // ���� ����� ������, ������������ �� ��������� �������
            ReturnToStartingPosition();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ��������� ������ ���� ����� �� ������� ������
            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed; // ���������� Rigidbody2D ��� ��������
            }
            else
            {
                rb.velocity = Vector2.zero; // ���������������, ���� ������� ������
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // ���������������, ���� ������ ���
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // ��� ����� �������� ������, ����� ������ ���� ������
            Debug.Log("Attack! Damage: " + attackDamage);
            lastAttackTime = Time.time;
        }
    }

    void ReturnToStartingPosition()
    {
        // ������������ �� ��������� ���������
        float distanceToStart = Vector2.Distance(transform.position, startingPosition);

        if (distanceToStart > 0.1f) // ��� �������������� ��������
        {
            Vector2 direction = (startingPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // ������������ � ��������� �����
        }
        else
        {
            rb.velocity = Vector2.zero; // ���������������, ��� ������ �������� ��������� �������
        }
    }
}