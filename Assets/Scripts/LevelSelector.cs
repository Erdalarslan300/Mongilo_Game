using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public LevelData levelData; // Bu levelin LevelData'sı

    public void LoadLevel()
    {
        GameManager.currentLevelData = levelData; // LevelData'yı GameManager'e ata
        SceneManager.LoadScene("OyunScene"); // Oyun sahnesine geçiş yap
    }
}
