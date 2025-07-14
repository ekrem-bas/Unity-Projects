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
        [SerializeField] private GameObject currentTowerPlace;
        void Start()
        {
            cam = Camera.main; // Ana kamerayı al
            groundLayer = LayerMask.GetMask("TowerPlace"); // Ground layer'ını al
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
                    currentTowerPlace = hit.collider.gameObject; // TowerPlace objesini sakla
                    Vector3 spawnPosition = hit.point + Vector3.up * 1.50f; // 0.5 birim yukarı
                    towerPopupManager.Show(spawnPosition); // Popup'ı göster
                }
            }

            // Sol tık ile popup'ı kapat (UI elementine tıklanmadıysa)
            if (Input.GetMouseButtonDown(0))
            {
                // UI üzerinde mi kontrol et
                if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    towerPopupManager.Hide();
                }
            }
        }

        public void PlaceTower(Vector3 position, int towerIndex)
        {
            GameObject prefab = towerPrefabs[towerIndex];
            TowerData towerData = towerDatas[towerIndex];

            // Seçilen TowerPlace'in merkezini kullan
            Vector3 towerPlaceCenter = position;
            if (currentTowerPlace != null)
            {
                towerPlaceCenter = currentTowerPlace.transform.position;
                towerPlaceCenter.y += 1.50f;
            }
            // TowerPlace'in etrafında bir kutu oluştur ve collider'ları kontrol et
            Vector3 boxSize = new Vector3(4f, 2f, 4f) * 0.5f;
            // Collider'ları kontrol et
            Collider[] colliders = Physics.OverlapBox(towerPlaceCenter, boxSize, Quaternion.identity, LayerMask.GetMask("Tower"));
            // Eğer etrafta başka bir kule varsa, yerleştirme işlemini yapma
            if (colliders.Length > 0)
            {
                Debug.Log("Burada zaten bir kule var!");
                return;
            }
            // Eğer yeterli coin varsa kuleyi yerleştir
            if (coinManager.coinCount >= towerData.price)
            {
                Instantiate(prefab, towerPlaceCenter, Quaternion.identity);
                coinManager.coinCount -= towerData.price;
            }
            else
            {
                Debug.Log("Yeterli coin yok!");
            }
        }
    }
}