using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning($"Scene to load is not set for {gameObject.name}");
        }
    }
}
