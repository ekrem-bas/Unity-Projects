using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game States")]
    public bool isPlayerDead = false;
    public bool isGameOverScreenActivated = false;
    public bool isSkillSelected = false;
    public bool isGameStarted = false;
    public bool isCharacterSelected = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCharacterSelected(bool isSelected)
    {
        isCharacterSelected = isSelected;
        Debug.Log($"Character Selected: {isSelected}");
    }

    public void SetPlayerDead(bool isDead)
    {
        isPlayerDead = isDead;
        Debug.Log($"Player Dead: {isDead}");
    }

    public void SetGameOverScreen(bool isActive)
    {
        isGameOverScreenActivated = isActive;
        isGameStarted = false;
        Debug.Log($"GameOver Screen: {isActive}");
    }

    public void SetSkillSelected(bool isSelected)
    {
        isSkillSelected = isSelected;
        Debug.Log($"Skill Selected: {isSelected}");
    }

    public void SetGameStarted(bool started)
    {
        isGameStarted = started;
        isGameOverScreenActivated = false; // Game started, reset game over screen
        isPlayerDead = false; // Reset player dead state
        isSkillSelected = false; // Reset skill selection state
        Debug.Log($"Game Started: {started}");
    }
}
