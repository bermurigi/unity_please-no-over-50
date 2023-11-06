using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] Sfx;
    public AudioSource[] Bgm;
    public float[] initialVolumes; // 각 SFX의 초기 볼륨 값을 저장하는 배열

    [SerializeField] private Text SFXOnText, SFXOffText, BGMOnText, BGMOffText;
    
    
    public static SoundManager soundManager;
    
    private bool isSFXMute;
    private bool isBGMMute;
    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        isSFXMute = PlayerPrefs.GetInt("IsSFXMute", 0) == 1;
        if (isSFXMute)
        {
            SFXOff();
        }
        else if (!isSFXMute)
        {
            SFXOn();
        }
        isBGMMute = PlayerPrefs.GetInt("IsBGMMute", 0) == 1;
        if (isBGMMute)
        {
            BGMOff();
        }
        else if (!isBGMMute)
        {
            BGMOn();
        }
       
    }

    private void Start()
    {
        // 초기 볼륨 값을 저장할 배열 초기화 (SFX 수와 동일한 길이로)
        initialVolumes = new float[Sfx.Length];

        // 각 SFX의 초기 볼륨 값을 저장
        for (int i = 0; i < Sfx.Length; i++)
        {
            initialVolumes[i] = Sfx[i].volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        for (int i = 0; i < Sfx.Length; i++)
        {
            // 초기 볼륨 값을 사용하여 현재 볼륨 값을 조절
            Sfx[i].volume = initialVolumes[i] * volume;
        }
    }

    public void SFXOn()
    {
        for (int i = 0; i < Sfx.Length; i++)
        {
            Sfx[i].mute = false;
            isSFXMute = false;
        }
        PlayerPrefs.SetInt("IsSFXMute", isSFXMute ? 1 : 0);
        SFXOnText.color = new Color(SFXOnText.color.r, SFXOnText.color.g, SFXOnText.color.b, 1f);
        SFXOffText.color = new Color(SFXOffText.color.r, SFXOffText.color.g, SFXOffText.color.b, 0.3f);
    }
    public void SFXOff()
    {
        for (int i = 0; i < Sfx.Length; i++)
        {
            Sfx[i].mute = true;
            isSFXMute = true;
        }
        PlayerPrefs.SetInt("IsSFXMute", isSFXMute ? 1 : 0);
        SFXOnText.color = new Color(SFXOnText.color.r, SFXOnText.color.g, SFXOnText.color.b, 0.3f);
        SFXOffText.color = new Color(SFXOffText.color.r, SFXOffText.color.g, SFXOffText.color.b, 1f);
    }
    public void BGMOn()
    {
        for (int i = 0; i < Bgm.Length; i++)
        {
            Bgm[i].mute = false;
        }
        isBGMMute = false;
        PlayerPrefs.SetInt("IsBGMMute", isBGMMute ? 1 : 0);
        BGMOnText.color = new Color(BGMOnText.color.r, BGMOnText.color.g, BGMOnText.color.b, 1f);
        BGMOffText.color = new Color(BGMOffText.color.r, BGMOffText.color.g, BGMOffText.color.b, 0.3f);
    }
    public void BGMOff()
    {
        for (int i = 0; i < Bgm.Length; i++)
        {
            Bgm[i].mute = true;
            
        }
        isBGMMute = true;
        PlayerPrefs.SetInt("IsBGMMute", isBGMMute ? 1 : 0);
        BGMOnText.color = new Color(BGMOnText.color.r, BGMOnText.color.g, BGMOnText.color.b, 0.3f);
        BGMOffText.color = new Color(BGMOffText.color.r, BGMOffText.color.g, BGMOffText.color.b, 1f);
    }

    // public void Kr()
    // {
    //     KrBtn.color = new Color(KrBtn.color.r, KrBtn.color.g, KrBtn.color.b, 1f);
    //     EnBtn.color = new Color(EnBtn.color.r, EnBtn.color.g, EnBtn.color.b, 0.3f);
    //     
    // }
    //
    // public void En()
    // {
    //     KrBtn.color = new Color(KrBtn.color.r, KrBtn.color.g, KrBtn.color.b, 0.3f);
    //     EnBtn.color = new Color(EnBtn.color.r, EnBtn.color.g, EnBtn.color.b, 1f);
    //     
    // }
    
}
