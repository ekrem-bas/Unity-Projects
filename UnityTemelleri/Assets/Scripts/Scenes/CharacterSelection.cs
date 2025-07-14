using System.Collections;
using System.Collections.Generic;
using Scripts.Enemy;
using Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Karakter prefab'ları
    public int currentIndex = -1; // Seçili karakterin indeksi
    public GameObject currentCharacter; // Şu anki karakter objesi
    public GameObject characterSpawnPoint; // Karakterin spawn edileceği nokta
    public Rigidbody currentRigidbody; // Şu anki karakterin Rigidbody bileşeni
    public NavMeshAgent currentNavMeshAgent; // Şu anki karakterin NavMeshAgent bileşeni
    public PlayerData playerData; // PlayerData scripti

    public void ShowCharacter(int index)
    {
        if (currentCharacter != null)
            Destroy(currentCharacter);

        currentIndex = index;
        currentCharacter = Instantiate(characterPrefabs[currentIndex], characterSpawnPoint.transform.position, Quaternion.identity);
        currentCharacter.transform.rotation = Quaternion.Euler(0, 135, 0);

        currentRigidbody = currentCharacter.GetComponent<Rigidbody>();
        currentRigidbody.isKinematic = true;

        currentNavMeshAgent = currentCharacter.GetComponent<NavMeshAgent>();

        // Hatalı scriptleri devre dışı bırak
        var movement = currentCharacter.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        var healthManager = currentCharacter.GetComponent<PlayerHealthManager>();
        if (healthManager != null)
            healthManager.enabled = false;

        var spawner = currentCharacter.GetComponent<EnemySpawner>();
        if (spawner != null)
            spawner.enabled = false;

        var detector = currentCharacter.GetComponent<EnemyDetector>();
        if (detector != null)
            detector.enabled = false;

        if (currentNavMeshAgent != null)
        {
            Destroy(currentNavMeshAgent);
        }
    }

    public void SelectCharacter()
    {
        if (currentIndex < 0 && playerData.selectedCharacterPrefab == null)
        {
            Debug.LogWarning("No character selected.");
            SceneManager.LoadScene("MainMenuScene");
            return;
        }
        else if (currentIndex < 0 && playerData.selectedCharacterPrefab != null)
        {
            Debug.LogWarning("Character already selected. Returning to main menu.");
            SceneManager.LoadScene("MainMenuScene");
            return;
        }
        playerData.selectedCharacterPrefab = characterPrefabs[currentIndex]; // Seçilen karakteri PlayerData'ya ata
        GameManager.instance.SetCharacterSelected(true); // Karakter seçildi olarak işaretle
        SceneManager.LoadScene("MainMenuScene"); // Menu sahnesine geç
    }
}
