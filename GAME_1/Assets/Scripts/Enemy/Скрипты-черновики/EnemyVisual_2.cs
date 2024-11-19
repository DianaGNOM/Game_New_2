using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual_2 : MonoBehaviour
{
    public float speed = 2.0f; // Скорость врага
    public Animator animator;

    private Vector2 movement;

    private void Update()
    {
        // Обновляем направление движения врага.
        MoveEnemy();

        // Обновляем анимацию в зависимости от направления движения.
        UpdateAnimation();
    }

    private void MoveEnemy()
    {
        // Генерируем случайное направление для движения в диапазоне -1 до 1
        float horizontal = Random.Range(-1f, 1f);
        float vertical = Random.Range(-1f, 1f);
        movement = new Vector2(horizontal, vertical).normalized; // Нормализуем

        // Двигаем врага в заданном направлении
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        // Определяем текущую анимацию на основе направления движения
        if (movement != Vector2.zero) // Если враг движется
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // Двигается больше по оси X
                if (movement.x > 0)
                {
                    animator.SetTrigger("MoveRight"); // вправо
                }
                else
                {
                    animator.SetTrigger("MoveLeft"); // влево
                }
            }
            else
            {
                // Двигается больше по оси Y
                if (movement.y > 0)
                {
                    animator.SetTrigger("MoveUp"); // вверх
                }
                else
                {
                    animator.SetTrigger("MoveDown"); // вниз
                }
            }
        }
        else
        {
            animator.SetTrigger("Idle"); // Если враг не движется
        }
    }
}
