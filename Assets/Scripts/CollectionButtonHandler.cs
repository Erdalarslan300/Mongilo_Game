using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButtonHandler : MonoBehaviour
{
    public GameObject[] panels; // Tüm panelleri alır (GalataPanel, FebePanel, vs.)

    private void Start()
    {
        // Panellerdeki buton ve image durumlarını yükle
        LoadPanelStates();

        // Tüm panellerdeki butonlara event listener ekle
        foreach (GameObject panel in panels)
        {
            if (panel != null)
            {
                // "Çıkış" adlı Text objesini her zaman aktif yap
                Text exitText = panel.transform.Find("Çıkış")?.GetComponent<Text>();
                if (exitText != null)
                {
                    exitText.gameObject.SetActive(true); // "Çıkış" text'i her zaman aktif olsun
                }

                Button[] buttons = panel.GetComponentsInChildren<Button>(true); // Panel içindeki tüm butonları al
                foreach (Button button in buttons)
                {
                    button.onClick.AddListener(() =>
                    {
                        // Skor kontrolü
                        int currentScore = PlayerPrefs.GetInt("Score", 0);
                        if (currentScore > 0)
                        {
                            OnButtonClick(button);
                        }
                        else
                        {
                            Debug.Log("Skor 0 olduğu için buton tıklanamaz.");
                        }
                    });
                }
            }
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        // Tıklanan butonun bağlı olduğu paneldeki Image'ı bul
        Transform parent = clickedButton.transform.parent; // Butonun üst objesini al
        if (parent != null)
        {
            string buttonName = clickedButton.name; // Butonun adını al
            string imageName = buttonName.Replace("Button", "HediyeImage"); // Butona bağlı Image adını türet

            Transform relatedImage = parent.Find(imageName); // İlgili Image'ı bul
            if (relatedImage != null)
            {
                relatedImage.gameObject.SetActive(false); // Image'ı inaktif yap
                // Image durumunu kaydet
                PlayerPrefs.SetInt(relatedImage.name + "_Active", 0); // 0 = inaktif
            }
        }

        // Tıklanan butonu tamamen gizle
        clickedButton.gameObject.SetActive(false);
        // Buton durumunu kaydet
        PlayerPrefs.SetInt(clickedButton.name + "_Active", 0); // 0 = inaktif

        // Skoru azalt
        int currentScore = PlayerPrefs.GetInt("Score", 0); // Mevcut skoru al
        currentScore = Mathf.Max(0, currentScore - 1); // Skor sıfırın altına inmesin
        PlayerPrefs.SetInt("Score", currentScore); // Yeni skoru kaydet
        PlayerPrefs.Save();

        // Skor UI'sini güncelle
        ScoreDisplay scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (scoreDisplay != null)
        {
            scoreDisplay.UpdateScoreText(currentScore);
        }
    }

    private void LoadPanelStates()
    {
        foreach (GameObject panel in panels)
        {
            if (panel != null)
            {
                Button[] buttons = panel.GetComponentsInChildren<Button>(true);
                foreach (Button button in buttons)
                {
                    // Kaydedilen buton durumunu yükle
                    int buttonActive = PlayerPrefs.GetInt(button.name + "_Active", 1); // Varsayılan: 1 (aktif)
                    button.gameObject.SetActive(buttonActive == 1);
                }

                Image[] images = panel.GetComponentsInChildren<Image>(true);
                foreach (Image image in images)
                {
                    // Kaydedilen image durumunu yükle
                    int imageActive = PlayerPrefs.GetInt(image.name + "_Active", 1); // Varsayılan: 1 (aktif)
                    image.gameObject.SetActive(imageActive == 1);
                }
            }
        }
    }
}
