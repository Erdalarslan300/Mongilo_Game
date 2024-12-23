using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        // Text nesnesine Button bileşeni otomatik eklenir ve tıklama için hazırlanır
        Button button = gameObject.GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("TextSceneLoader script is attached, but no Button component found on " + gameObject.name);
        }
        else
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not specified for " + gameObject.name);
        }
    }
}
