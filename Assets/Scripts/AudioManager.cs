using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // Arka plan müziği
    private AudioSource audioSource; // AudioSource bileşeni
    private static AudioManager instance; // Singleton örneği

    private void Awake()
    {
        // Eğer başka bir AudioManager varsa, yok et
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tek bir AudioManager olsun ve sahneler arasında yok olmasın
        instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource'u ayarla
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Döngüde çalsın
        audioSource.playOnAwake = false; // Sahne başlarken hemen çalmaya başlama
        audioSource.volume = 0.1f; // Ses seviyesini ayarla
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
