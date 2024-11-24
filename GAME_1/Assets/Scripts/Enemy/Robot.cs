/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f; // Скорость движения врага
    public float attackRadius = 1.5f; // Радиус атаки
    public float returnRadius = 7f; // Радиус преследования
    public float attackDamage = 10f; // Урон от атаки
    public float attackCooldown = 1f; // Время между атаками

    public Player playerScript;
    private Vector3 shotpos;
    private Transform player; // Ссылка на игрока
    private float lastAttackTime; // Время последней атаки
    private Rigidbody2D rb; // Rigidbody2D для движения
    private Vector3 startposition;

    public float health_enemy = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        startposition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
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
            rb.MovePosition(startposition); //Идём к начальной точке
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
        health_enemy -= damage; // Уменьшаем здоровье
        Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health_enemy);

        if (health_enemy <= 0)
        {
            Die(); // Вызываем метод смерти, если здоровье ниже или равно нулю
        }
    }
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // Логика смерти врага (например, перезагрузка сцены, анимации и т.д.)
    }
}
*/






using System.Collections;
using UnityEngine;
public class Robot : MonoBehaviour
{
    public float moveSpeed = 2f; // Скорость движения врага
    public float attackRange = 1.5f; // Дальность атаки
    public float attackDamage = 10f; // Урон от атаки
    public float attackCooldown = 1f; // Время между атаками
    public float chaseDistance = 5f; // Дальность преследования
    public float stopDistance = 1f; // Дистанция, на которой враг останавливается перед игроком
    public float health_enemy = 100f; // Здоровье врага
    public float shootingRange = 10f; // Максимальная дистанция для стрельбы
    public Vector3 directionToPlayer;
    private bool Left_1;
    private bool Right_1;
    private bool Left;
    private bool Right;
    private bool Up;
    private bool Down;

    public Transform shootpoint;

    private Transform player; // Ссылка на игрока
    private float lastAttackTime; // Время последней атаки
    private Rigidbody2D rb; // Rigidbody2D для движения
    private Animator animator; //Animator для визуализации
    private Vector3 startingPosition; // Начальная позиция врага

    void Start()
    {
        animator = GetComponent<Animator>(); // Получаем компонент Animator
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        startingPosition = transform.position; // Запоминаем начальную позицию врага
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Проверяем, находится ли игрок в пределах расстояния преследования
        if (distanceToPlayer < chaseDistance)
        {
            MoveTowardsPlayer();

            // Если игрок близок к врагу, атакуем
            if (distanceToPlayer < attackRange)
            {
                rb.velocity = Vector2.zero;
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
        // Получаем направление к игроку
        Vector2 direction = (player.position - transform.position).normalized;

        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Двигаемся только если игрок не слишком близко (в пределах stopDistance)
        if (distanceToPlayer > stopDistance)
        {
            //rb.velocity = direction * moveSpeed; // Перемещаемся к игроку
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Перемещаем по оси X
                rb.velocity = new Vector3(Mathf.Sign(direction.x) * moveSpeed, 0, 0);
            }
            else
            {
                // Перемещаем по оси Y
                rb.velocity = new Vector3(0, Mathf.Sign(direction.y) * moveSpeed, 0);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Останавливаемся, если слишком близко
        }
        MoveMainEnemy();
    }
    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Тут можно добавить логику, чтобы задать урон игроку
            directionToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, shootingRange);
            Debug.Log("Attack! Damage: " + attackDamage);
            MoveAttack();
            lastAttackTime = Time.time;
        }
    }
    public void TakeDamage_enemy(float damage)
    {
        health_enemy -= damage; // Уменьшаем здоровье
        Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health_enemy);

        if (health_enemy <= 0)
        {
            Die(); // Вызываем метод смерти, если здоровье меньше или равно нулю
        }
    }
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // Логика смерти врага (например, перезагрузка сцены, анимации и т.д.)
        //Destroy(gameObject); // Удаляем врага из игры
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
        Vector3 distance = player.position - transform.position;
        float distanceToStart = Vector2.Distance(transform.position, startingPosition);
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
            Left_1 = false;
            Right_1 = false;
        }
        animator.SetBool("Left_1", Left_1);
        animator.SetBool("Right_1", Right_1);
    }
    void MoveAttack()
    {
        directionToPlayer = player.position - transform.position;
        if (Mathf.Abs(directionToPlayer.x) != 0)
            directionToPlayer.x /= Mathf.Abs(directionToPlayer.x);
        if (Mathf.Abs(directionToPlayer.y) != 0)
            directionToPlayer.y /= Mathf.Abs(directionToPlayer.y);
        Left = false;
        Right = false;
        Up = false;
        Down = false;
        Left_1 = false;
        Right_1 = false;
        if (directionToPlayer == Vector3.up)
        {
            Up = true;
        }
        if (directionToPlayer == -Vector3.up)
        {
            Down = true;
        }
        if (directionToPlayer == Vector3.right)
        {
            Right = true;
        }
        if (directionToPlayer == -Vector3.right)
        {
            Left = true;
        }
        animator.SetBool("At_right", Right);
        animator.SetBool("At_left", Left);
        animator.SetBool("At_up", Up);
        animator.SetBool("At_down", Down);
    }
}

