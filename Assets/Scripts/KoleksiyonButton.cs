using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KoleksiyonButton : MonoBehaviour
{
    public void OnKoleksiyonButtonClick()
    {
        // KoleksiyonScene adlı sahneye geç
        SceneManager.LoadScene("KoleksiyonScene");
    }
}
