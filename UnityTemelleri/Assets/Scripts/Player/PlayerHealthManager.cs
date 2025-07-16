using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerData playerData;
    public Healthbar healthbar; // Sağlık çubuğu scripti
    public Animator animator;
    public GameObject deathEffect; // Ölüm efekti prefab'ı
    public GameObject bloodEffectPrefab;
    void Start()
    {
        GameManager.instance.SetPlayerDead(false); // Oyuncu başlangıçta ölü değil
        animator = GetComponent<Animator>(); // Animator bileşenini al
        healthbar = FindObjectOfType<Healthbar>(); // Sağlık çubuğu scriptini bul
        playerData.health = playerData.maxHealth; // Oyuncunun canını maksimum can olarak ayarla
        healthbar.UpdateHealthbar(playerData.maxHealth, playerData.health); // Sağlık çubuğunu güncelle
    }

    public void Death()
    {
        if (GameManager.instance.isPlayerDead) return;
        deathEffect.SetActive(true); // Ölüm efekti prefab'ını etkinleştir
        animator.SetBool("Death", true);
        GameManager.instance.SetPlayerDead(true); // Oyuncu ölü olarak işaretlendi
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magic"))
        {
            // Magic'in hasarını al
            float damage = other.GetComponent<Projectile>().damage;
            // Oyuncunun canını azalt
            playerData.health -= damage;
            // Sağlık çubuğunu güncelle
            healthbar.UpdateHealthbar(playerData.maxHealth, playerData.health);
            if (playerData.health <= 0)
            {
                this.Death();
            }
            // Mermiyi yok et
            MagicPoolManager.Instance.magicPool.Release(other.GetComponent<Projectile>());
        }
        else if (other.CompareTag("Sword"))
        {
            // kılıcın hasarı
            float damage = other.GetComponentInParent<Enemy>().swordDamage;
            // canı azalt
            playerData.health -= damage;
            // canı güncelle
            healthbar.UpdateHealthbar(playerData.maxHealth, playerData.health); // Sağlık çubuğunu güncelle
            // kan efekti
            Instantiate(bloodEffectPrefab, transform.position + (Vector3.up * 1.2f), Quaternion.identity);
            // can bitince end game
            if (playerData.health <= 0)
            {
                this.Death();
            }
        }
    }

    public void DestroySelf()
    {
        if (GameManager.instance.isGameOverScreenActivated) return;
        GameManager.instance.SetGameOverScreen(true); // Game Over ekranını etkinleştir
        EndGame();
        healthbar.gameObject.SetActive(false); // Sağlık çubuğunu gizle
    }

    public void EndGame()
    {
        GameOverScene gameOverScene = FindObjectOfType<GameOverScene>();
        gameOverScene.ShowGameOver();
    }
}
