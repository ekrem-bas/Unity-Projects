using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;

namespace Scripts.Tower
{
    public class TowerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab; // Mermi prefab
        [SerializeField] private Transform spawnPoint; // Merminin spawn edileceği nokta
        private GameObject shootTarget; // Merminin hedefi
        [SerializeField] private TowerData towerData; // Kule verileri
        void Start()
        {
            InvokeRepeating("Shoot", 0f, towerData.shootTimer);
        }

        void Shoot()
        {
            if (GameManager.instance.isPlayerDead) // Eğer oyuncu ölmüşse
            {
                CancelInvoke("Shoot"); // Oyuncu öldüyse atışı durdur
                return;
            }

            EnemyDetector detector = GetComponent<EnemyDetector>();
            shootTarget = detector.GetClosestEnemy(); // En yakın düşmanı al

            // Eğer hedef yoksa atış yapma
            if (shootTarget == null)
            {
                return;
            }
            Projectile bullet = TowerBulletPoolManager.Instance.towerBulletPool.Get();
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
            bullet.Init(shootTarget.GetComponent<Collider>().bounds.center, towerData.damage, towerData.bulletSpeed, TowerBulletPoolManager.Instance.towerBulletPool);
        }
    }
}