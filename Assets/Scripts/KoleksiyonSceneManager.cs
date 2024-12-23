using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoleksiyonSceneManager : MonoBehaviour
{
    public GameObject GalataPanel;
    public GameObject FebePanel;
    public GameObject BejekePanel;
    public GameObject TesePanel;
    public GameObject TarafPanel;

    private PanelManager panelManager;

    private void Start()
    {
        panelManager = FindObjectOfType<PanelManager>();
        if (panelManager != null)
        {
            // PanelManager'dan aktif paneli KoleksiyonScene'de aktif yap
            panelManager.ActivateCollectionPanel(GalataPanel, FebePanel, BejekePanel, TesePanel, TarafPanel);
        }
        else
        {
            Debug.LogError("PanelManager bulunamadı!");
        }

        // CollectionButtonHandler'a panelleri bildir
        CollectionButtonHandler buttonHandler = FindObjectOfType<CollectionButtonHandler>();
        if (buttonHandler != null)
        {
            buttonHandler.panels = new GameObject[] { GalataPanel, FebePanel, BejekePanel, TesePanel, TarafPanel };
        }
        else
        {
            Debug.LogError("CollectionButtonHandler bulunamadı!");
        }
    }
}
