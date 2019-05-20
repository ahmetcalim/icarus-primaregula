using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    public List<float> renderScales;
    public Text renderScaleTxt;
    private static int currentRenderScaleIndex;
    private static float currentRenderScale = 1;
    public static bool isShadowsEnable;
    public List<Button> buttons;
    public Animator shadowSwitchBtnAnimator;
    public Scrollbar scrollbarMusicVolume;
    public Scrollbar scrollbarSoundEffects;
    public static int CurrentRenderScaleIndex { get => currentRenderScaleIndex; set => currentRenderScaleIndex = value; }
    public static float CurrentRenderScale { get => currentRenderScale; set => currentRenderScale = value; }
    public static float MusicVolume { get; set; }
    public static float EffectVolume { get; set; }

    public static bool IsShadowsEnable()
    {
        return isShadowsEnable;
    }
    public void ChangeShadowsSetting()
    {
        isShadowsEnable = !isShadowsEnable;
        if (isShadowsEnable == true)
        {
            PlayerPrefs.SetInt("isShadowsEnabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isShadowsEnabled", 0);
        }
        shadowSwitchBtnAnimator.SetBool("isShadowsEnable", IsShadowsEnable());
    }
   
    private void Start()
    {
      
        scrollbarSoundEffects.value = PlayerPrefs.GetFloat("effectVolume");
        scrollbarMusicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        renderScaleTxt.text = PlayerPrefs.GetFloat("renderScale").ToString();
        CurrentRenderScale = PlayerPrefs.GetFloat("renderScale");
        if (PlayerPrefs.GetInt("renderscaleIndex") == 0)
        {
            PlayerPrefs.SetInt("renderscaleIndex", 0);
        }
        CurrentRenderScaleIndex = PlayerPrefs.GetInt("renderscaleIndex");
    }
    public void SetVolume()
    {
        MusicVolume = scrollbarMusicVolume.value;
        EffectVolume = scrollbarSoundEffects.value;
        if (EffectVolume == 0f)
        {
            EffectVolume = 0.01f;
        }
        if (MusicVolume == 0f)
        {
            MusicVolume = 0.01f;
        }
        PlayerPrefs.SetFloat("musicVolume", MusicVolume);
      
        PlayerPrefs.SetFloat("effectVolume", EffectVolume);
    }
    public void ChangeRenderQuality(int btnIndex)
    {
        CurrentRenderScaleIndex += btnIndex;
        if (CurrentRenderScaleIndex == -1)
        {
            CurrentRenderScaleIndex = 0;

        }
        if (CurrentRenderScaleIndex == 5)
        {
            CurrentRenderScaleIndex = 4;
        }
        PlayerPrefs.SetInt("renderscaleIndex", CurrentRenderScaleIndex);
        CurrentRenderScale = renderScales[CurrentRenderScaleIndex];
        renderScaleTxt.text = renderScales[CurrentRenderScaleIndex].ToString();
        PlayerPrefs.SetFloat("renderScale", CurrentRenderScale);
        if (CurrentRenderScaleIndex == 0)
        {
            buttons[0].enabled = false;
        }
        else
        {
            buttons[0].enabled = true;
        }
        if (CurrentRenderScaleIndex == renderScales.Count - 1)
        {
            buttons[1].enabled = false;
        }
        else
        {
            buttons[1].enabled = true;
        }
    }
}
