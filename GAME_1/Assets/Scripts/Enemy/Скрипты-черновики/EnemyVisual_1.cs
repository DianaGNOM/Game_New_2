using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual_1 : MonoBehaviour
{
    //public float moveSpeed = 2f; // Скорость движения врага
    //public float changeDirectionTime = 2f; // Время между изменениями направления
    private Animator animator;

    private Vector3 movementDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        // Двигаем врага
        //transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // считываем в вектор направление движения врага
        // Если есть направление движения, меняем анимацию
        if (movementDirection.magnitude > 0.1f)
        {
            ChangeAnimationDirection(movementDirection);
        }
    }

    private void ChangeAnimationDirection(Vector3 direction)
    {
        // Определяем угол направления
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

        // Передаем значение в Animator для переключения анимаций
        if (angle >= -45 && angle < 45)
        {
            animator.SetInteger("Direction", 0); // Вперед
        }
        else if (angle >= 45 && angle < 135)
        {
            animator.SetInteger("Direction", 1); // Вправо
        }
        else if (angle >= 135 || angle < -135)
        {
            animator.SetInteger("Direction", 2); // Назад
        }
        else if (angle >= -135 && angle < -45)
        {
            animator.SetInteger("Direction", 3); // Влево
        }
    }
    /*
    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            // Генерируем случайное направление
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);
            movementDirection = new Vector3(horizontal, 0, vertical).normalized;

            yield return new WaitForSeconds(changeDirectionTime);
        }
    }
    */
}

