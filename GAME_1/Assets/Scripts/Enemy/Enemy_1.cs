using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public float health_enemy = 100f; // Здоровье врага
    public float attackDamage = 10f; // Урон от атаки
    public void TakeDamage_enemy(float damage)
    {
        health_enemy -= damage;
        Debug.Log("Enemy takes damage: " + damage + ". Current health: " + health_enemy);

        if (health_enemy <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // Логика смерти врага (например, перезагрузка сцены, анимации и т.д.)
        //Destroy(gameObject); // Удаляем врага из игры
    }
}
