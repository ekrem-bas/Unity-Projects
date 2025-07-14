using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPanelManager : MonoBehaviour
{
    void LateUpdate()
    {
        if (GameManager.instance.isGameOverScreenActivated)
        {
            gameObject.SetActive(false); // Game Over ekranı aktifse coin panelini gizle
            return; // Hiçbir şey yapma
        }
    }
}
