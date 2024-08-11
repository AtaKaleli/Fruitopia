using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex >= sfx.Length)
            return;
        sfx[sfxIndex].Play();
    }

    public void StopSFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();
    }


}
