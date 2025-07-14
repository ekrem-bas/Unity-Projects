using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Tower;
public class TowerPopupManager : MonoBehaviour
{
    public GameObject popupPanel; // Popup paneli
    private Vector3 spawnPosition; // Popup'ın açılacağı pozisyon
    public Tower towerPopUpManager; // Tower script referansı

    public void Show(Vector3 position)
    {
        if (GameManager.instance.isPlayerDead || GameManager.instance.isGameOverScreenActivated)  // Oyuncu ölmüşse popup'ı gösterme
        {
            return; // Oyuncu ölmüşse popup'ı gösterme
        }

        if (GameManager.instance.isSkillSelected)
        {
            return;
        }

        popupPanel.SetActive(true); // Popup panelini aktif et
        spawnPosition = position; // Popup'ın açılacağı pozisyonu ayarla

        // Paneli fare pozisyonuna taşı
        Vector2 mousePos = Input.mousePosition;
        popupPanel.GetComponent<Transform>().position = mousePos;
    }

    // Popup'ı gizle
    public void Hide()
    {
        popupPanel.SetActive(false);
    }

    // Butonlara tıklandığında çağrılacak fonksiyonlar
    public void OnTowerButton(int towerIndex)
    {
        towerPopUpManager.PlaceTower(spawnPosition, towerIndex);
        Hide();
    }

    public void OnCancelButton()
    {
        Hide();
    }
}
