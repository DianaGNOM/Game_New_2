/*
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
*/






using System.Collections;
using UnityEngine;
public class Robot : MonoBehaviour
{
    public float moveSpeed = 2f; // �������� �������� �����
    public float attackRange = 1.5f; // ��������� �����
    public float attackDamage = 10f; // ���� �� �����
    public float attackCooldown = 1f; // ����� ����� �������
    public float chaseDistance = 5f; // ��������� �������������
    public float stopDistance = 1f; // ���������, �� ������� ���� ��������������� ����� �������
    public float health_enemy = 100f; // �������� �����
    public float shootingRange = 10f; // ������������ ��������� ��� ��������
    public Vector2 directionToPlayer;
    public float distanceToStart;
    private bool Left_1;
    private bool Right_1;
    private bool Left;
    private bool Right;
    private bool Up;
    private bool Down;
    private bool Idle;

    public Transform shootpoint;

    private Transform player; // ������ �� ������
    private float lastAttackTime; // ����� ��������� �����
    private Rigidbody2D rb; // Rigidbody2D ��� ��������
    private Animator animator; //Animator ��� ������������
    private Vector3 startingPosition; // ��������� ������� �����

    void Start()
    {
        animator = GetComponent<Animator>(); // �������� ��������� Animator
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
        // �������� ����������� � ������
        Vector2 direction = (player.position - transform.position).normalized;

        // ��������� ���������� �� ������
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ��������� ������ ���� ����� �� ������� ������ (� �������� stopDistance)
        if (distanceToPlayer > stopDistance)
        {
            //rb.velocity = direction * moveSpeed; // ������������ � ������
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // ���������� �� ��� X
                rb.velocity = new Vector3(Mathf.Sign(direction.x) * moveSpeed, 0, 0);
            }
            else
            {
                // ���������� �� ��� Y
                rb.velocity = new Vector3(0, Mathf.Sign(direction.y) * moveSpeed, 0);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // ���������������, ���� ������� ������
        }
        MoveMainEnemy();
    }
    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // ��� ����� �������� ������, ����� ������ ���� ������
            rb.velocity = Vector2.zero;
            directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, shootingRange);
            MoveAttack();
            //if (hit.collider.gameObject.tag == "Player")
            //{
            Debug.Log("Attack! Damage: " + attackDamage);
            //}
            lastAttackTime = Time.time;
        }
    }
    public void TakeDamage_enemy(float damage)
    {
        health_enemy -= damage; // ��������� ��������
        Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health_enemy);

        if (health_enemy <= 0)
        {
            Die(); // �������� ����� ������, ���� �������� ������ ��� ����� ����
        }
    }
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // ������ ������ ����� (��������, ������������ �����, �������� � �.�.)
        //Destroy(gameObject); // ������� ����� �� ����
    }
    void ReturnToStartingPosition()
    {
        // ������������ �� ��������� ���������
        distanceToStart = Vector2.Distance(transform.position, startingPosition);

        if (distanceToStart > 0.1f) // ��� �������������� ��������
        {
            Vector2 direction = (startingPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // ������������ � ��������� �����
        }
        else
        {
            rb.velocity = Vector2.zero; // ���������������, ��� ������ �������� ��������� �������
        }
        MoveMainEnemy();
    }
    void MoveMainEnemy()
    {
        Left = false;
        Right = false;
        Up = false;
        Down = false;
        Left_1 = false;
        Right_1 = false;
        Idle = false;
        Vector3 distance = player.position - transform.position;
        distanceToStart = Vector2.Distance(transform.position, startingPosition);
        if (distance.x < 0)
        {
            Left_1 = true;
        }
        if (distance.x > 0)
        {
            Right_1 = true;
        }
        if (distanceToStart < 0.1f)
        {
            Idle = true;
            Left_1 = false;
            Right_1 = false;
        }
        animator.SetBool("Left_1", Left_1);
        animator.SetBool("Right_1", Right_1);
        animator.SetBool("At_right", Right);
        animator.SetBool("At_left", Left);
        animator.SetBool("At_up", Up);
        animator.SetBool("At_down", Down);
        animator.SetBool("Idle", Idle);
    }
    void MoveAttack()
    {
        directionToPlayer = player.position - transform.position;
        if (Mathf.Abs(directionToPlayer.x) != 0)
            directionToPlayer.x /= Mathf.Abs(directionToPlayer.x);
        if (Mathf.Abs(directionToPlayer.y) != 0)
            directionToPlayer.y /= Mathf.Abs(directionToPlayer.y);
        distanceToStart = Vector2.Distance(transform.position, startingPosition);
        Left = false;
        Right = false;
        Up = false;
        Down = false;
        Left_1 = false;
        Right_1 = false;
        Idle = true;
        if (directionToPlayer == Vector2.up || directionToPlayer == Vector2.up + Vector2.right || directionToPlayer == Vector2.up - Vector2.right)
        {
            Up = true;
        }
        if (directionToPlayer == -Vector2.up || directionToPlayer == -Vector2.up + Vector2.right || directionToPlayer == -Vector2.up - Vector2.right)
        {
            Down = true;
        }
        if (directionToPlayer == Vector2.right)
        {
            Right = true;
        }
        if (directionToPlayer == -Vector2.right)
        {
            Left = true;
        }
        if (distanceToStart < 0.1f)
        {
            Idle = true;
            Left = false;
            Right = false;
            Up = false;
            Down = false;
        }
        animator.SetBool("Left_1", Left_1);
        animator.SetBool("Right_1", Right_1);
        animator.SetBool("At_right", Right);
        animator.SetBool("At_left", Left);
        animator.SetBool("At_up", Up);
        animator.SetBool("At_down", Down);
        animator.SetBool("Idle", Idle);
        Debug.Log("���������!");
    }
}

