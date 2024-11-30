using System.Collections;
using UnityEngine;

public class Zombie : Enemy_1
{
    public float moveSpeed = 2f; // —корость движени€ врага
    public float attackRange = 2.5f; // ƒальность атаки
    public float attackCooldown = 1f; // ¬рем€ между атаками
    public float chaseDistance = 5f; // –ассто€ние преследовани€
    public float stopDistance = 0.7f; 
    public float distanceToPlayer;
    public float distanceToStart;
    public Vector2 distance;

    private Transform player; // —сылка на игрока
    private Rigidbody2D rb; // Rigidbody2D дл€ движени€
    private Vector3 startingPosition; // Ќачальна€ позици€ врага
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
        distance = player.position - transform.position;

        // ѕровер€ем, находитс€ ли игрок в пределах рассто€ни€ преследовани€
        if (distanceToPlayer < chaseDistance)
        {
            MoveTowardsPlayer();
            Animation();

            // ≈сли игрок близок к врагу, атакуем
            if ((distanceToPlayer < attackRange))
            {
                AttackPlayer();
                Animation();
            }
        }
        else
        {
            // ≈сли игрок далеко, возвращаемс€ на начальную позицию
            ReturnToStartingPosition();
            Animation();
        }
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            IsWalking = true;
            IsAttacking = false;

            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // ƒвигаемс€ только если игрок не слишком близко
            if (distanceToPlayer > stopDistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed; // »спользуем Rigidbody2D дл€ движени€
            }
            else
            {
                rb.velocity = Vector2.zero; // ќстанавливаемс€, если слишком близко
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // ќстанавливаемс€, если игрока нет
        }
    }

    void AttackPlayer()
    {
        //distanceToPlayer = Vector2.Distance(transform.position, player.position);
        distance = player.position - transform.position;
        if (Player.Instance.IsAttacking_() == true)
        {
            rb.velocity = Vector3.zero;
            IsAttacking = true;
            IsWalking = false;
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            IsAttacking = false;
            IsWalking = true;
        }
    }
    void ReturnToStartingPosition()
    {
        // ¬озвращаемс€ на начальное положение
        distanceToStart = Vector2.Distance(transform.position, startingPosition);

        if (distanceToStart > 0.1f) // ƒл€ предотвращени€ дрожани€
        {
            Vector2 direction = (startingPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed; // ѕеремещаемс€ к начальной точке

            IsWalking = true;
            IsAttacking = false;

        }
        else
        {
            rb.velocity = Vector2.zero; // ќстанавливаемс€, как только достигли начальной позиции
            IsWalking = false;
            IsAttacking = false;
        }
    }
    //функци€ смены анимаций
    void Animation()
    {
        distance = player.position - transform.position;
        distanceToStart = Vector2.Distance(transform.position, startingPosition);
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Up = false;
        Down = false;
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
        if ((distance.y < 0) && (Mathf.Abs(distance.y) > 0.9f))
        {
            Down = true;
            if (IsAttacking == true)
            {
                IsAttackDown = true;
                IsWalkDown = false;
            }
            if (IsWalking == true)
            {
                IsWalkDown = true;
                IsAttackDown = false;
            }
        }
        if ((distance.y > 0) && (Mathf.Abs(distance.y) > 0.9f))
        {
            Up = true;
            if (IsAttacking == true)
            {
                IsAttackUp = true;
                IsWalkUp = false;
            }
            if (IsWalking == true)
            { 
                IsWalkUp = true;
                IsAttackUp = false;
            }
        }
        if ((Up == false) && (Down == false))
        {
            if (distance.x < 0)
            {
                if (IsAttacking == true)
                {
                    IsAttackLeft = true;
                    IsWalkLeft = false;

                }
                if (IsWalking == true)
                {
                    IsWalkLeft = true;
                    IsAttackLeft = false;
                }
            }
            if (distance.x > 0)
            { 
                if (IsAttacking == true)
                {
                    IsAttackRight = true;
                    IsWalkRight = false;
                }
                if (IsWalking == true)
                {
                    IsWalkRight = true;
                    IsAttackRight = false;
                }
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
        //код дл€ перехода к анимации смерти ещЄ не придуман
        //отдельно код дл€ движени€ к герою и возвращению к начальной точке
        //попробовать через координаты игрока и врага
    }
}