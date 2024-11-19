using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual_1 : MonoBehaviour
{
    //public float moveSpeed = 2f; // �������� �������� �����
    //public float changeDirectionTime = 2f; // ����� ����� ����������� �����������
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
        // ������� �����
        //transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

        // ��������� � ������ ����������� �������� �����
        // ���� ���� ����������� ��������, ������ ��������
        if (movementDirection.magnitude > 0.1f)
        {
            ChangeAnimationDirection(movementDirection);
        }
    }

    private void ChangeAnimationDirection(Vector3 direction)
    {
        // ���������� ���� �����������
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

        // �������� �������� � Animator ��� ������������ ��������
        if (angle >= -45 && angle < 45)
        {
            animator.SetInteger("Direction", 0); // ������
        }
        else if (angle >= 45 && angle < 135)
        {
            animator.SetInteger("Direction", 1); // ������
        }
        else if (angle >= 135 || angle < -135)
        {
            animator.SetInteger("Direction", 2); // �����
        }
        else if (angle >= -135 && angle < -45)
        {
            animator.SetInteger("Direction", 3); // �����
        }
    }
    /*
    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            // ���������� ��������� �����������
            float horizontal = Random.Range(-1f, 1f);
            float vertical = Random.Range(-1f, 1f);
            movementDirection = new Vector3(horizontal, 0, vertical).normalized;

            yield return new WaitForSeconds(changeDirectionTime);
        }
    }
    */
}

