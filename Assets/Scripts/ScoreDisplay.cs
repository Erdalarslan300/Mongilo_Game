using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text skorText2; // SkorText2 prefab'ındaki Text referansı

    private static ScoreDisplay instance;

    private void Awake()
    {
        // Singleton yapısı: SkorText2 nesnesinin sahneler arasında yalnızca bir tane olmasını sağla
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // SkorText2 prefab'ını sahneler arasında kalıcı yap
        }
        else
        {
            Destroy(gameObject); // Aynı prefab'dan birden fazla varsa sil
        }
    }

    private void Start()
    {
        // Oyunun başında PlayerPrefs'ten skoru al ve UI'yı güncelle
        int skor = PlayerPrefs.GetInt("Score", 0);
        UpdateScoreText(skor);
    }

    // Skor bilgisini güncelle
    public void UpdateScoreText(int skor)
    {
        if (skorText2 != null)
        {
            skorText2.text = "Skor: " + skor; // Skoru UI'da göster
        }
        else
        {
            Debug.LogError("SkorText2 referansı eksik! Lütfen UI Text nesnesini atayın.");
        }
    }
}
