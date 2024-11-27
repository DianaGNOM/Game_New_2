using System.Collections;
using UnityEngine;

public class Zombie : Enemy_1
{
    public float moveSpeed = 2f; // Скорость движения врага
    public float attackRange = 1.5f; // Дальность атаки
    public float attackCooldown = 1f; // Время между атаками
    public float chaseDistance = 5f; // Расстояние преследования
    public float stopDistance = 2f; // Расстояние отставания
    public float distanceToPlayer;

    private Transform player; // Ссылка на игрока
    private float lastAttackTime; // Время последней атаки
    private Rigidbody2D rb; // Rigidbody2D для движения
    private Vector3 startingPosition; // Начальная позиция врага
    private Animator anim;

    public bool IsAttacking;
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

        // Проверяем, находится ли игрок в пределах расстояния преследования
        if (distanceToPlayer < chaseDistance)
        {
            MoveTowardsPlayer();

            // Если игрок близок к врагу, атакуем
            if (distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            // Если игрок далеко, возвращаемся на начальную позицию
            ReturnToStartingPosition();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Двигаемся только если игрок не слишком близко
            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed; // Используем Rigidbody2D для движения
            }
            else
            {
                rb.velocity = Vector2.zero; // Останавливаемся, если слишком близко
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Останавливаемся, если игрока нет
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer == 0f)
            {
                rb.velocity = Vector3.zero;
                IsAttacking = true;
            }
            else
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }
            lastAttackTime = Time.time;
        }
    }
    void ReturnToStartingPosition()
    {
        // Возвращаемся на начальное положение
        float distanceToStart = Vector2.Distance(transform.position, startingPosition);

        if (distanceToStart > 0.1f) // Для предотвращения дрожания
        {
            Vector2 direction = (startingPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // Перемещаемся к начальной точке
        }
        else
        {
            rb.velocity = Vector2.zero; // Останавливаемся, как только достигли начальной позиции
        }
    }
    //функция смены анимаций в апдейт ещё не добавлена
    void Animation()
    {
        Up = false;
        Down = false;
        Vector2 distance = player.position - transform.position;
        float distanceToStart = Vector2.Distance(transform.position, startingPosition);
        IsAttackUp = false;
        IsAttackDown = false;
        IsAttackLeft = false;
        IsAttackRight = false;
        IsWalkUp = false;
        IsWalkDown = false;
        IsWalkLeft = false;
        IsWalkRight = false;
        IsDeathUp = false;
        IsDeathDown = false;
        IsDeathLeft = false;
        IsDeathRight = false;
        IsStop = false;
        if (distanceToStart < 0.1f)
        {
            IsAttackUp = IsAttackDown = IsAttackLeft = IsAttackRight = false;
            IsWalkUp = IsWalkDown = IsWalkLeft = IsWalkRight = false;
            IsDeathUp = IsDeathDown = IsDeathLeft = IsDeathRight = false;
            IsStop = true;
        }
        if (distance.y < 0)
        {
            Down = true;
            if (IsAttacking == true)
                IsAttackDown = true;
            else
                IsWalkDown = true;
        }
        if (distance.y > 0)
        {
            Up = true;
            if (IsAttacking == true)
                IsAttackUp = true;
            else
                IsWalkUp = true;
        }
        if (Up == false && Down == false)
        {
            if (distance.x < 0)
            { 
                if (IsAttacking == true)
                    IsAttackLeft = true;
                else
                    IsWalkLeft = true;
            }
            if (distance.x > 0)
            { 
                if (IsAttacking == true)
                    IsAttackRight = true;
                else
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
        //код для перехода к анимации смерти ещё не придуман
    }
}