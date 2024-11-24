using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_play : MonoBehaviour
{
    public class PlayerShooting : MonoBehaviour
    {
        public Camera playerCamera; // Ссылка на камеру игрока
        public float shootingRange = 100f; // Максимальная дистанция стрельбы
        public float damage = 20f; // Урон от первого выстрела
        public float fireRate = 1f; // Частота стрельбы
        private float nextFireTime = 0f;

        void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime) // Проверка нажатия клавиши
            {
                nextFireTime = Time.time + 1f / fireRate; // Настройка времени следующей стрельбы
                Shoot();
            }
        }

        private void Shoot()
        {
            RaycastHit hit;
            // Создаем луч из позиции камеры в направлении ее взгляда
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, shootingRange))
            {
                Debug.Log("Выстрел по объекту: " + hit.transform.name);
                // Проверка на попадание в врага
                Robot enemy = hit.transform.GetComponent<Robot>(); // Предположим, что у врага есть класс Enemy
                if (enemy != null)
                {
                    enemy.TakeDamage_enemy(damage); // Уменьшаем здоровье врага
                    Debug.Log("Игрок попал в врага! Урон: " + damage);
                }
            }
        }
    }
}