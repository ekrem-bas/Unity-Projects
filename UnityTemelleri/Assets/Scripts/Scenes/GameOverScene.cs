using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    public Text gameCoinText;
    public Image bloodImage;
    public Text coinText;
    public Canvas gameOverCanvas;
    public GameObject skillPanel;
    public GameObject healthbarCanvas;

    public void ShowGameOver()
    {
        // healthbarCanvas.SetActive(false); // Sağlık çubuğu panelini gizle
        // skillPanel.SetActive(false); // Skill panelini gizle
        // gameCoinText.enabled = false;
        // bloodImage.enabled = false;
        int totalCoins = CoinManager.coinManagerInstance.coinCount;
        coinText.text = totalCoins.ToString();
        gameOverCanvas.gameObject.SetActive(true);
        // SkillManager.instance.ResetSkill(); // Skill'i resetle
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
