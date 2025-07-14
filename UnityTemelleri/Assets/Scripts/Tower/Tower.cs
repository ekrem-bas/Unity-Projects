using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Tower
{
    public class Tower : MonoBehaviour
    {
        private Camera cam;
        [SerializeField] private GameObject[] towerPrefabs; // Yerleştirilecek kule prefab'ı
        [SerializeField] private TowerData[] towerDatas; // Kule verileri
        [SerializeField] private TowerPopupManager towerPopupManager; // Kule popup yöneticisi
        [SerializeField] CoinManager coinManager;
        [SerializeField] private LayerMask groundLayer; // Yere yerleştirme için kullanılacak layer
        void Start()
        {
            cam = Camera.main; // Ana kamerayı al
            groundLayer = LayerMask.GetMask("Ground"); // Ground layer'ını al
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1)) // Sağ fare tuşu
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Fare pozisyonundan bir ray oluştur
                RaycastHit hit; // Raycast sonucu için bir değişken
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                {
                    Vector3 spawnPosition = hit.point + Vector3.up * 1.50f; // 0.5 birim yukarı
                    towerPopupManager.Show(spawnPosition); // Popup'ı göster
                }
            }
        }

        public void PlaceTower(Vector3 position, int towerIndex)
        {
            GameObject prefab = towerPrefabs[towerIndex];
            TowerData towerData = towerDatas[towerIndex];

            Vector3 boxSize = new Vector3(4f, 2f, 4f) * 0.5f; // BoxCollider'ın yarısı kadar (center to edge)
            Collider[] colliders = Physics.OverlapBox(position, boxSize, Quaternion.identity, LayerMask.GetMask("Tower"));

            if (colliders.Length > 0)
            {
                Debug.Log("Burada zaten bir kule var!");
                return;
            }

            if (coinManager.coinCount >= towerData.price)
            {
                // Kule prefab'ını belirtilen pozisyonda oluştur
                Instantiate(prefab, position, Quaternion.identity);
                coinManager.coinCount -= towerData.price; // Coin sayısını güncelle
            }
            else
            {
                Debug.Log("Yeterli coin yok!"); // Yeterli coin yoksa uyarı ver
            }
        }
    }
}