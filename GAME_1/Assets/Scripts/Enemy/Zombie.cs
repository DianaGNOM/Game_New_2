using System.Collections;
using UnityEngine;

public class Zombie : Enemy_1
{
    public float moveSpeed = 2f; // �������� �������� �����
    public float attackRange = 1.5f; // ��������� �����
    public float attackCooldown = 1f; // ����� ����� �������
    public float chaseDistance = 5f; // ���������� �������������
    public float stopDistance = 1f; // ���������� ����������
    public float distanceToPlayer;
    public float distanceToStart;

    private Transform player; // ������ �� ������
    private Rigidbody2D rb; // Rigidbody2D ��� ��������
    private Vector3 startingPosition; // ��������� ������� �����
    private Animator anim;

    public bool IsAttacking = false;
    public bool IsWalking = false;
    public bool IsAttackUp;
    public bool IsAttackDown;
    public bool IsAttackLeft;
    public bool IsAttackRight;
    public bool IsWalkUp;
    public bool IsWalkDown;
    public bool IsWalkLeft;
    public bool IsWalkRight;
    public bool IsDeathUp;
    public bool IsDeathDown;
    public bool IsDeathLeft;
    public bool IsDeathRight;
    public bool IsStop;

    private bool Up;
    private bool Down;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player_1").transform; 
        rb = GetComponent<Rigidbody2D>(); 
        startingPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

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
        Animation();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            IsWalking = true;
            IsAttacking = false;

            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
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
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer == 0f)
        {
            rb.velocity = Vector3.zero;
            IsAttacking = true;
            IsWalking = false;
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            IsWalking = true;
            IsAttacking = false;
        }
    }
    void ReturnToStartingPosition()
    {
        // ������������ �� ��������� ���������
        distanceToStart = Vector2.Distance(transform.position, startingPosition);

        if (distanceToStart > 0.1f) // ��� �������������� ��������
        {
            Vector2 direction = (startingPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // ������������ � ��������� �����

            IsWalking = true;
            IsAttacking = false;

        }
        else
        {
            rb.velocity = Vector2.zero; // ���������������, ��� ������ �������� ��������� �������
        }
    }
    //������� ����� �������� � ������ ��� �� ���������
    void Animation()
    {
        Up = false;
        Down = false;
        Vector2 distance = player.position - transform.position;
        distanceToStart = Vector2.Distance(transform.position, startingPosition);
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        IsAttackUp = false;
        IsAttackDown = false;
        IsAttackLeft = false;
        IsAttackRight = false;
        IsWalkUp = false;
        IsWalkDown = false;
        IsWalkLeft = false;
        IsWalkRight = false;
        IsStop = false;
        if (distanceToStart < 0.1f)
        {
            IsAttackUp = IsAttackDown = IsAttackLeft = IsAttackRight = false;
            IsWalkUp = IsWalkDown = IsWalkLeft = IsWalkRight = false;
            IsDeathUp = IsDeathDown = IsDeathLeft = IsDeathRight = false;
            IsAttacking = IsWalking = false;
            IsStop = true;
        }
        if (distance.y < 0)
        {
            Down = true;
            if (IsAttacking == true)
                IsAttackDown = true;
                IsWalkDown = false;
            if (IsWalking == true)
                IsWalkDown = true;
                IsAttackDown = false;
        }
        if (distance.y > 0)
        {
            Up = true;
            if (IsAttacking == true)
                IsAttackUp = true;
                IsWalkUp = false;
            if (IsWalking == true)
                IsWalkUp = true;
        }
        if ((Up == false) && (Down == false))
        {
            if (distance.x < 0)
            { 
                if (IsAttacking == true)
                    IsAttackLeft = true;
                if (IsWalking == true)
                    IsWalkLeft = true;
            }
            if (distance.x > 0)
            { 
                if (IsAttacking == true)
                    IsAttackRight = true;
                if (IsWalking == true)
                    IsWalkRight = true;
            }
        }
        anim.SetBool("Up_w", IsWalkUp);
        anim.SetBool("Down_w", IsWalkDown);
        anim.SetBool("Right_w", IsWalkRight);
        anim.SetBool("Left_w", IsWalkLeft);
        anim.SetBool("Idle_zom", IsStop);
        anim.SetBool("Up_a", IsAttackUp);
        anim.SetBool("Down_a", IsAttackDown);
        anim.SetBool("Left_a", IsAttackLeft);
        anim.SetBool("Right_a", IsAttackRight);
        //��� ��� �������� � �������� ������ ��� �� ��������
        //�������� ��� ��� �������� � ����� � ����������� � ��������� �����
        //����������� ����� ���������� ������ � �����
    }
}