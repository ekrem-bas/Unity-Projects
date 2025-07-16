using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private GameObject closestEnemy; // En yakın düşman
        [SerializeField] private GameObject owner;
        public EnemySpawner enemySpawner; // EnemySpawner referansı

        void Start()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>(); // EnemySpawner'ı bul
        }
        // Update is called once per frame
        void Update()
        {
            closestEnemy = ClosestEnemy(); // En yakın düşmanı bul
                                           // Eğer en yakın düşman varsa ona bak
            if (closestEnemy != null)
            {
                owner.transform.LookAt(closestEnemy.transform); // Oyuncu en yakın düşmana bakar
            }
        }

        private GameObject ClosestEnemy()
        {
            // Eğer hiç düşman yoksa null döndür
            if (EnemySpawner.allEnemies.Count == 0)
                return null;

            GameObject closest = EnemySpawner.allEnemies[0];
            float closestDistance = Mathf.Infinity; // En yakın mesafe başlangıçta sonsuz olarak ayarlanır

            foreach (GameObject enemyObj in EnemySpawner.allEnemies)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy.health <= 0) continue;
                float distance = Vector3.Distance(transform.position, enemyObj.transform.position); // Düşman ile bu nesne arasındaki mesafeyi hesapla
                if (distance < closestDistance) // Eğer bu düşman daha yakınsa
                {
                    closestDistance = distance; // En yakın mesafeyi güncelle
                    closest = enemyObj; // En yakın düşmanı güncelle
                }
            }
            return closest;
        }

        public GameObject GetClosestEnemy()
        {
            return closestEnemy; // En yakın düşmanı döndür
        }
    }
}
