using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne geçişleri için

public class GameManager : MonoBehaviour
{
    public Text questionText; // Soru metni için UI
    public Text[] answerTexts; // Cevap şıkları için UI
    public static LevelData currentLevelData; // Dinamik olarak atanacak LevelData
    public Text levelNameText; // Hangi levelin yüklendiğini gösterecek UI

    public GameObject gameOverPanel; // Game Over paneli
    public GameObject levelCompletedPanel; // Level Completed paneli
    public Text scoreText; // Skor göstergesi
    public LevelData[] GsLevels; // Galatasaray levelleri
    public LevelData[] FbLevels; // Fenerbahçe levelleri
    public LevelData[] BjkLevels; // Beşiktaş levelleri
    public LevelData[] TsLevels; // Trabzonspor levelleri
    public LevelData[] StslLevels; // Süper Lig levelleri
    public LevelData[] WorldLevels; // Dünya levelleri

    public AudioClip nextLevelSound; // NextLevelSound ses efekti
    public AudioClip gameOverSound; // GameOverSound ses efekti
    private AudioSource audioSource; // Ses oynatma kaynağı

    private int score = 0; // Skor
    private int currentLevelIndex = 0; // Şu anki levelin indeks değeri
    private LevelData[] currentLevelArray; // Bulunduğumuz kategoriye ait level dizisi

    private int currentQuestionIndex = 0; // Hangi soruda olduğumuz

    private void Start()
    {
        if (currentLevelData == null)
        {
            Debug.LogError("LevelData atanmadı! Lütfen sahneye geçmeden önce LevelData'yı ayarlayın.");
            return;
        }

        // AudioSource'u ayarla
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Skoru yükle
        score = PlayerPrefs.GetInt("Score", 0);
        UpdateScoreUI();

        gameOverPanel.SetActive(false); // Başlangıçta GameOverPanel gizli
        levelCompletedPanel.SetActive(false); // Başlangıçta LevelCompletedPanel gizli

        AssignCurrentLevelArray(); // Hangi kategoride olduğumuzu belirle
        LoadQuestion();
    }

    private void AssignCurrentLevelArray()
    {
        // Bulunduğumuz kategoriyi ScriptableObject ismine göre belirle
        if (System.Array.Exists(GsLevels, level => level == currentLevelData)) currentLevelArray = GsLevels;
        else if (System.Array.Exists(FbLevels, level => level == currentLevelData)) currentLevelArray = FbLevels;
        else if (System.Array.Exists(BjkLevels, level => level == currentLevelData)) currentLevelArray = BjkLevels;
        else if (System.Array.Exists(TsLevels, level => level == currentLevelData)) currentLevelArray = TsLevels;
        else if (System.Array.Exists(StslLevels, level => level == currentLevelData)) currentLevelArray = StslLevels;
        else if (System.Array.Exists(WorldLevels, level => level == currentLevelData)) currentLevelArray = WorldLevels;

        // Mevcut levelin indeksini belirle
        currentLevelIndex = System.Array.IndexOf(currentLevelArray, currentLevelData);
    }

    private void LoadQuestion()
    {
        UpdateLevelName(); // Level başlığını güncelle

        if (currentQuestionIndex < currentLevelData.questions.Length)
        {
            QuestionData questionData = currentLevelData.questions[currentQuestionIndex];
            questionText.text = questionData.questionText;

            for (int i = 0; i < answerTexts.Length; i++)
            {
                if (i < questionData.options.Length)
                {
                    answerTexts[i].text = questionData.options[i];
                    int index = i; // Closure sorunu için
                    answerTexts[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    answerTexts[i].GetComponent<Button>().onClick.AddListener(() => CheckAnswer(index, questionData.correctOptionIndex));
                }
                else
                {
                    answerTexts[i].text = ""; // Eğer seçenek yoksa boş bırak
                    answerTexts[i].GetComponent<Button>().onClick.RemoveAllListeners();
                }
            }
        }
        else
        {
            LevelCompleted();
        }
    }

    private void CheckAnswer(int selectedIndex, int correctIndex)
    {
        if (selectedIndex == correctIndex)
        {
            Debug.Log("Doğru cevap!");
            currentQuestionIndex++;
            LoadQuestion();
        }
        else
        {
            Debug.Log("Yanlış cevap!");
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true); // GameOverPanel'i aktif et

        // GameOverSound çal
        if (gameOverSound != null)
        {
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }
    }

    public void RestartLevel()
    {
        currentQuestionIndex = 0; // İlk soruya dön
        gameOverPanel.SetActive(false); // GameOverPanel'i gizle
        LoadQuestion(); // Level'i yeniden başlat
    }

    private void LevelCompleted()
    {
        levelCompletedPanel.SetActive(true); // LevelCompletedPanel'i aktif et
        score++; // Skoru artır
        SaveScore(); // Skoru kaydet
        UpdateScoreUI(); // Skor UI güncelle

        // NextLevelSound çal
        if (nextLevelSound != null)
        {
            audioSource.clip = nextLevelSound;
            audioSource.Play();
        }

        // Skoru tüm sahnelerdeki SkorText2'ye gönder
        ScoreDisplay skorDisplay = FindObjectOfType<ScoreDisplay>();
        if (skorDisplay != null)
        {
            skorDisplay.UpdateScoreText(score);
        }
    }

    public void NextLevel()
    {
        if (currentLevelArray != null && currentLevelIndex + 1 <= currentLevelArray.Length)
        {
            if (currentLevelIndex == 10) // Level 11'e geldiysek (index 10 çünkü diziler 0'dan başlar)
            {
                SceneManager.LoadScene("TeamsScene"); // TeamsScene sahnesine geç
            }
            else
            {
                currentLevelIndex++; // Bir sonraki levele geç
                currentLevelData = currentLevelArray[currentLevelIndex]; // Yeni LevelData'yı ayarla
                currentQuestionIndex = 0; // Soruları sıfırla
                levelCompletedPanel.SetActive(false); // Paneli kapat
                LoadQuestion(); // Yeni levele soruları yükle
            }
        }
        else
        {
            Debug.Log("Tüm leveller tamamlandı!");
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + score;
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    private void UpdateLevelName()
    {
        if (levelNameText != null)
        {
            // ScriptableObject adı neyse başlık olarak ayarla
            levelNameText.text = currentLevelData.name;
        }
        else
        {
            Debug.LogWarning("Level Name Text atanmadı!");
        }
    }
}
