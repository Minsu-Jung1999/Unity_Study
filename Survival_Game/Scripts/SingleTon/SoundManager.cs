using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name; // ∞Ó¿« ¿Ã∏ß
    public AudioClip clip; // ∞Ó
}



public class SoundManager : MonoBehaviour
{
    public enum SoundOrder : int
    {
        COMBO_1 = 0,
        COMBO_2,
        COMBO_3,
        COMBO_4,
        CHARGING,
        FLASH,
        SHIELD,
        HIT
    }
    static public SoundManager instance;

    #region ΩÃ±€≈Ê
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public SoundOrder soundorder;
    public AudioSource[] audioSourcesEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    public void PlaySE(SoundOrder soundNum)
    {
        audioSourcesEffects[(int)soundNum].Play();
    }
    public void StopAllSE()
    {
        for (int i = 0; i < audioSourcesEffects.Length; i++)
        {
            audioSourcesEffects[i].Stop();
        }
    }
    public void StopSE(SoundOrder soundNum)
    {
        audioSourcesEffects[(int)soundNum].Stop();
    }
}
