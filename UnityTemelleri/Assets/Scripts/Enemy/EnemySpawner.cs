using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject player; // oyuncu
        [SerializeField] private GameObject plane; // plane
        public static List<GameObject> allEnemies = new List<GameObject>();
        Vector3 planeSize; // plane'in boyutu
        [SerializeField] private GameObject[] enemyPrefabs; // düşman prefab
        // oyunucya olan minimum spawn mesafesi
        public float minSpawnDistance = 2f;
        // maksimum düşman sayısı
        public int maxEnemyCount = 100;

        void Awake()
        {
            // Tüm düşmanları temizle
            allEnemies.Clear();
        }
        // Start is called before the first frame update
        void Start()
        {
            // plane'in boyutunu al
            planeSize = plane.GetComponent<MeshRenderer>().bounds.size;
            InvokeRepeating("SpawnEnemy", 0f, 1f); // her saniyede bir SpawnEnemy fonksiyonunu çağır
        }

        void SpawnEnemy()
        {
            if (GameManager.instance.isPlayerDead || GameManager.instance.isGameOverScreenActivated)
            {
                return; // Oyuncu öldüyse veya game over ekranı aktifse spawn etme
            }

            int randomIndex = Random.Range(0, enemyPrefabs.Length); // düşman prefab'larından rastgele birini seç
            if (maxEnemyCount > 0)
            { // Random olarak spawn edilecek pozisyonu belirle
                Vector3 spawnPosition;
                do
                {
                    float x = Random.Range(-planeSize.x / 2, planeSize.x / 2);
                    float z = Random.Range(-planeSize.z / 2, planeSize.z / 2);
                    spawnPosition = new Vector3(
                        plane.transform.position.x + x,
                        1,
                        plane.transform.position.z + z
                    );
                } while (
                    Vector3.Distance(spawnPosition, player.transform.position) < minSpawnDistance
                    || Physics.OverlapSphere(spawnPosition, 2f, LayerMask.GetMask("Enemy")).Length > 0
                    );

                // Quaternion.identity = nesnein rotasyonu olmadan varsayılan olarak oluşturulması
                Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
                maxEnemyCount--;
            }
            else
            {
                CancelInvoke("SpawnEnemy"); // düşman sayısı maksimuma ulaştığında SpawnEnemy fonksiyonunu durdur

            }
        }
    }
}