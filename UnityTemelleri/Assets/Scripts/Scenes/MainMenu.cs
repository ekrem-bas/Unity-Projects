using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public PlayerData playerData;
    void Start()
    {
        if (!GameManager.instance.isCharacterSelected)
        {
            playerData.selectedCharacterPrefab = null;
        }
        GameManager.instance.SetPlayerDead(false);
        GameManager.instance.SetGameOverScreen(false);
        GameManager.instance.SetSkillSelected(false);
    }

    public void PlayGame()
    {
        if (playerData.selectedCharacterPrefab == null)
        {
            Debug.LogWarning("No character selected. Please choose a character first.");
            return;
        }
        else
        {
            GameManager.instance.SetGameStarted(true); // Oyunu başlat
            SceneManager.LoadScene("GameScene");
        }
    }

    public void ChooseCharacter()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
