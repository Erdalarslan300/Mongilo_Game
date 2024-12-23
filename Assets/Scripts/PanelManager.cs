using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    // 5 Panel
    public GameObject GalatasarayPanel;
    public GameObject FenerbahcePanel;
    public GameObject BesıktasPanel;
    public GameObject TrabzonPanel;
    public GameObject TarafsızPanel;

    // Static aktif panel referansı
    private static GameObject activePanel;

    // PlayerPrefs anahtarı
    private static string activePanelKey = "ActivePanel";

    private void Awake()
    {
        // Eğer bu sahnede ilk kez çalışıyorsa DontDestroyOnLoad'ı koru
        if (activePanel != null)
        {
            DontDestroyOnLoad(activePanel);
        }
    }

    private void Start()
    {
        // Sahne geçişinde PlayerPrefs'teki kaydedilmiş paneli yükle
        string savedPanelName = PlayerPrefs.GetString(activePanelKey, string.Empty);
        if (!string.IsNullOrEmpty(savedPanelName))
        {
            GameObject panelToActivate = FindPanelByName(savedPanelName);
            if (panelToActivate != null)
            {
                ActivatePanel(panelToActivate);
            }
        }
        else
        {
            DeactivateAllPanels();
        }
    }

    public void OnGalatasaraySpriteClick()
    {
        ActivatePanel(GalatasarayPanel);
    }

    public void OnFenerbahceSpriteClick()
    {
        ActivatePanel(FenerbahcePanel);
    }

    public void OnBesiktasSpriteClick()
    {
        ActivatePanel(BesıktasPanel);
    }

    public void OnTrabzonSpriteClick()
    {
        ActivatePanel(TrabzonPanel);
    }

    public void OnTarafsizSpriteClick()
    {
        ActivatePanel(TarafsızPanel);
    }

    private void ActivatePanel(GameObject panelToActivate)
    {
        // Önceki aktif paneli kapat
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }

        // Yeni paneli aktif yap
        panelToActivate.SetActive(true);
        activePanel = panelToActivate;

        // PlayerPrefs'te kaydet
        PlayerPrefs.SetString(activePanelKey, activePanel.name);
        PlayerPrefs.Save();

        // DontDestroyOnLoad ile sahne değişiminde tut
        DontDestroyOnLoad(activePanel);
    }

    private void DeactivateAllPanels()
    {
        GalatasarayPanel.SetActive(false);
        FenerbahcePanel.SetActive(false);
        BesıktasPanel.SetActive(false);
        TrabzonPanel.SetActive(false);
        TarafsızPanel.SetActive(false);
    }

    private GameObject FindPanelByName(string panelName)
    {
        switch (panelName)
        {
            case "GalatasarayPanel":
                return GalatasarayPanel;
            case "FenerbahcePanel":
                return FenerbahcePanel;
            case "BesıktasPanel":
                return BesıktasPanel;
            case "TrabzonPanel":
                return TrabzonPanel;
            case "TarafsızPanel":
                return TarafsızPanel;
            default:
                return null;
        }
    }

    // **Yeni Fonksiyon: KoleksiyonScene'de doğru paneli aktif et**
    public void ActivateCollectionPanel(GameObject galataPanel, GameObject febePanel, GameObject bejekePanel, GameObject tesePanel, GameObject tarafPanel)
    {
        // PlayerPrefs'ten aktif panel adını al
        string activePanelName = PlayerPrefs.GetString(activePanelKey, string.Empty);

        // Tüm KoleksiyonScene panellerini kapat
        galataPanel.SetActive(false);
        febePanel.SetActive(false);
        bejekePanel.SetActive(false);
        tesePanel.SetActive(false);
        tarafPanel.SetActive(false);

        // Aktif paneli KoleksiyonScene'de aç
        switch (activePanelName)
        {
            case "GalatasarayPanel":
                galataPanel.SetActive(true);
                break;
            case "FenerbahcePanel":
                febePanel.SetActive(true);
                break;
            case "BesıktasPanel":
                bejekePanel.SetActive(true);
                break;
            case "TrabzonPanel":
                tesePanel.SetActive(true);
                break;
            case "TarafsızPanel":
                tarafPanel.SetActive(true);
                break;
            default:
                Debug.LogWarning("KoleksiyonScene: Aktif panel bulunamadı veya eşleşmiyor!");
                break;
        }
    }
}
