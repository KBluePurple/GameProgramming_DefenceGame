using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private float volume = .5f;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat("musicVolume", .5f);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    public void IncreaseVolume()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float GetVolume()
    {
        return volume;
    }
}
