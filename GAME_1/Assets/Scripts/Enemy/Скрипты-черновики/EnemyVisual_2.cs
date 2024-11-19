using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual_2 : MonoBehaviour
{
    public float speed = 2.0f; // �������� �����
    public Animator animator;

    private Vector2 movement;

    private void Update()
    {
        // ��������� ����������� �������� �����.
        MoveEnemy();

        // ��������� �������� � ����������� �� ����������� ��������.
        UpdateAnimation();
    }

    private void MoveEnemy()
    {
        // ���������� ��������� ����������� ��� �������� � ��������� -1 �� 1
        float horizontal = Random.Range(-1f, 1f);
        float vertical = Random.Range(-1f, 1f);
        movement = new Vector2(horizontal, vertical).normalized; // �����������

        // ������� ����� � �������� �����������
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        // ���������� ������� �������� �� ������ ����������� ��������
        if (movement != Vector2.zero) // ���� ���� ��������
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // ��������� ������ �� ��� X
                if (movement.x > 0)
                {
                    animator.SetTrigger("MoveRight"); // ������
                }
                else
                {
                    animator.SetTrigger("MoveLeft"); // �����
                }
            }
            else
            {
                // ��������� ������ �� ��� Y
                if (movement.y > 0)
                {
                    animator.SetTrigger("MoveUp"); // �����
                }
                else
                {
                    animator.SetTrigger("MoveDown"); // ����
                }
            }
        }
        else
        {
            animator.SetTrigger("Idle"); // ���� ���� �� ��������
        }
    }
}
