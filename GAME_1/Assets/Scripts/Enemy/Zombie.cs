using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 2f; // Скорость движения врага
    public float attackRange = 1.5f; // Дальность атаки
    public float attackDamage = 10f; // Урон от атаки
    public float attackCooldown = 1f; // Время между атаками
    public float chaseDistance = 5f; // Расстояние преследования
    public float stopDistance = 2f; // Расстояние отставания

    private Transform player; // Ссылка на игрока
    private float lastAttackTime; // Время последней атаки
    private Rigidbody2D rb; // Rigidbody2D для движения
    private Vector3 startingPosition; // Начальная позиция врага

    void Start()
    {
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
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

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
            // Тут можно добавить логику, чтобы задать урон игроку
            Debug.Log("Attack! Damage: " + attackDamage);
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
}