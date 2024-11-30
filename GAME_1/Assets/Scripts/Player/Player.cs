using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private Rigidbody2D rb;
    private Vector2 inputVector;

    private float lastAttackTime_1;
    private float attackCooldown_1 = 1f;

    [SerializeField] private float speed_player = 3f;
    public float health_hero = 100f;
    private Vector2 speed_to_axis;
    private bool togetherY;
    private bool togetherX;
    private bool isRunningUp;
    private bool isRunningDown;
    private bool isRunningLeftRight;
    private bool rev;
    private bool isAttacking_ = false;
    private bool isAttackingUp;
    private bool isAttackingDown;
    private bool isAttackingLeft;
    private bool isAttackingRight;
    private bool isUpDown;
    private bool isStandingUp;
    private bool isStandingDown;
    private bool isStandingRight;
    private bool isStandingLeft;
    public bool isShooting = false;
    private bool isShootingUp;
    private bool isShootingDown;
    private bool isShootingLeft;
    private bool isShootingRight;
    public GameObject _gun;
    public bool IsCollider = false;
    public bool IsAttacking_()
    {
        return isAttacking_;
    }
    public bool IsRunningUp()
    {
        return isRunningUp;
    }
    public bool IsRunningDown()
    {
        return isRunningDown;
    }
    public bool IsRunningLeftRight()
    {
        return isRunningLeftRight;
    }
    public bool Rev()
    {
        return rev;
    }
    public bool IsAttackingUp()
    {
        return isAttackingUp;
    }
    public bool IsAttackingDown()
    {
        return isAttackingDown;
    }
    public bool IsAttackingRight()
    {
        return isAttackingRight;
    }
    public bool IsAttackingLeft()
    {
        return isAttackingLeft;
    }
    public bool IsStandingUp()
    {
        return isStandingUp;
    }
    public bool IsStandingDown()
    {
        return isStandingDown;
    }
    public bool IsStandingRight()
    {
        return isStandingRight;
    }
    public bool IsStandingLeft()
    {
        return isStandingLeft;
    }
    public bool IsShootingUp()
    {
        return isShootingUp;
    }
    public bool IsShootingDown()
    {
        return isShootingDown;
    }
    public bool IsShootingRight()
    {
        return isShootingRight;
    }
    public bool IsShootingLeft()
    {
        return isShootingLeft;
    }
    public bool NowIsShooting()
    {
        return isShooting;
    }
    public bool ReturnToIdle()
    {
        return !isShooting;
    }

    private void HandleMovement()
    {
        speed_to_axis = inputVector * (speed_player * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + speed_to_axis);
        inputVector = inputVector.normalized;
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _gun.SetActive(false);
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void Update()
    {
        inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1f;
        }
        Animation(moving_mode());
    }
    public bool moving_mode()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            isShooting = true;
            _gun.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            isShooting = false;
            _gun.SetActive(false);
        }
        return isShooting;
    }
    private void Animation(bool isShooting)
    {
        isUpDown = false;

        isRunningUp = false;
        isRunningDown = false;
        isRunningLeftRight = false;
        rev = false;
        togetherY = false;
        togetherX = false;
        
        isAttackingUp = false;
        isAttackingDown = false;
        isAttackingLeft = false;
        isAttackingRight = false;

        isStandingUp = false;
        isStandingDown = false;
        isStandingRight = false;
        isStandingLeft = false;

        isShootingUp = false;
        isShootingDown = false;
        isShootingRight = false;
        isShootingLeft = false;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            togetherY = true;
        }
        if (Input.GetKey(KeyCode.W) && togetherY == false)
        {
            if(isShooting == false)
            {
                isRunningUp = true;
            }
            else
            {
                isAttackingUp = true;
                isShootingUp = true;
            }
            isUpDown = true;
        }
        if (Input.GetKeyUp(KeyCode.W) && isShooting)
        {
            isStandingUp = true;
            isShootingUp = true;
        }     
        if (Input.GetKey(KeyCode.S) && togetherY == false)
        {
            if (isShooting == false)
            {
                isRunningDown = true;
            }
            else
            {
                isAttackingDown = true;
                isShootingDown = true;
            }
            isUpDown = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && isShooting)
        {
            isStandingDown = true;
            isShootingDown = true;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            togetherX = true;
        }
        if (Input.GetKey(KeyCode.A) && togetherX == false)
        {
            if (isUpDown == false)
            {
                if (isShooting == false)
                {
                    isRunningLeftRight = true;
                    rev = true;
                }
                else 
                { 
                    isAttackingLeft = true;
                    isShootingLeft = true;
                }
            } 
        }
        if (Input.GetKeyUp(KeyCode.A) && isShooting)
        {
            isStandingLeft = true;
            isShootingLeft = true;
        }

        if (Input.GetKey(KeyCode.D) && togetherX == false)
        {
            if (isUpDown == false)
            {
                if (isShooting == false)
                {
                    isRunningLeftRight = true;
                    rev = false;
                }
                else 
                { 
                    isAttackingRight = true;
                    isShootingRight = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.D) && isShooting)
        {
            isStandingRight = true;
            isShootingRight = true;
        }
    }

    public void TakeDamage_hero(float damage)
    {
        health_hero -= damage; // Уменьшаем здоровье
        Debug.Log("Player takes damage: " + damage + ". Current health: " + health_hero);

        if (health_hero <= 0)
        {
            Die(); // Вызываем метод смерти, если здоровье ниже или равно нулю
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Логика смерти игрока (например, перезагрузка сцены, анимации и т.д.)
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isAttacking_ = true;
        if (Time.time >= lastAttackTime_1 + attackCooldown_1)
        {
            if ((collision.gameObject.tag == "Enemy") && (collision.gameObject.GetComponent<Zombie>() != null))
            {
                Enemy_1 en = collision.gameObject.GetComponent<Enemy_1>();
                if (en != null)
                {
                    Debug.Log("Attack! Damage: " + en.attackDamage);
                    TakeDamage_hero(en.attackDamage);
                }
            }
            lastAttackTime_1 = Time.time;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= lastAttackTime_1 + attackCooldown_1)
        {
            if ((collision.gameObject.tag == "Enemy") && (collision.gameObject.GetComponent<Zombie>() != null))
            {
                Enemy_1 en = collision.gameObject.GetComponent<Enemy_1>();
                if (en != null)
                {
                    Debug.Log("Attack! Damage: " + en.attackDamage);
                    TakeDamage_hero(en.attackDamage);
                }
            }
            lastAttackTime_1 = Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isAttacking_ = false;
    }
}
