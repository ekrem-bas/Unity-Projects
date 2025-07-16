using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;
namespace Scripts.Player
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab; // Mermi prefab
        [SerializeField] private Transform spawnPoint; // Merminin spawn edileceği nokta
        [SerializeField] private float bulletSpeed = 40f; // Merminin hızı
        [SerializeField] private float playerBulletDamage = 50f; // Player mermi hasarı
        public GameObject shootTarget; // Merminin hedefi

        public void Shoot()
        {
            shootTarget = GetComponent<EnemyDetector>().GetClosestEnemy();
            if (shootTarget == null)
                return;

            Projectile bullet = BulletPoolManager.Instance.bulletPool.Get();
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
            Collider enemyCollider = shootTarget.GetComponent<Collider>();
            Vector3 targetPoint = enemyCollider.bounds.center;
            bullet.Init(targetPoint, playerBulletDamage, bulletSpeed, BulletPoolManager.Instance.bulletPool);
        }
    }
}