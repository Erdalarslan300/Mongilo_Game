using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Sahne geçişleri için

public class SettingsButton : MonoBehaviour
{
    public void OnSettingsButtonClick()
    {
        SceneManager.LoadScene("AyarlarScene");  // AyarlarScene adlı sahneye geçiş
    }

    public void OnGalatasarayButtonClick()
    {
        SceneManager.LoadScene("GalatasarayScene");  // GalatasarayScene adlı sahneye geçiş
    }

    public void OnFenerbahceButtonClick()
    {
        SceneManager.LoadScene("FenerbahceScene");  // FenerbahceScene adlı sahneye geçiş
    }

    public void OnBesıktasButtonClick()
    {
        SceneManager.LoadScene("BesıktasScene");  // BesıktasScene adlı sahneye geçiş
    }

    public void OnTrabzonButtonClick()
    {
        SceneManager.LoadScene("TrabzonScene");  // TrabzonScene adlı sahneye geçiş
    }

    public void OnStslButtonClick()
    {
        SceneManager.LoadScene("TurkıyeScene");  // TurkıyeScene adlı sahneye geçiş
    }

    public void OnWorldButtonClick()
    {
        SceneManager.LoadScene("DunyaScene");  // DunyaScene adlı sahneye geçiş
    }
}
