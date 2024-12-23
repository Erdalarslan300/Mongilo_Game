using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    // Yönlendirilmek istenen URL
    [SerializeField] private string url = "https://www.instagram.com/erdalarslan300?igsh=MTZnMThxbW92MGs3MQ==";

    // Bu metoda tıklama olayında çağrılacak
    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
