using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private int currentBGMIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        /*
        if (bgm.Length <= 0)
            return;*/

        //InvokeRepeating(nameof(PlayMusicIfNeeded), 0, 2);
    }

  


    /*
    public void PlayRandomBGM()
    {
        currentBGMIndex = Random.Range(0, bgm.Length);
        PlayBGM(currentBGMIndex);
    }

    public void PlayMusicIfNeeded()
    {
        if (bgm[currentBGMIndex].isPlaying == false)
            PlayRandomBGM();
    }

    public void PlayBGM(int bgmIndex)
    {

        if (bgm.Length <= 0)
            return;

        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        currentBGMIndex = bgmIndex;
        bgm[bgmIndex].Play();
    }*/

    public void PlaySFX(int sfxIndex, bool randomPitch = true)
    {
        if (sfxIndex >= sfx.Length)
            return;

        if(randomPitch)
            sfx[sfxIndex].pitch = Random.Range(.9f, 1.1f);
        
        sfx[sfxIndex].Play();
    }

    public void StopSFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();
    }



}
