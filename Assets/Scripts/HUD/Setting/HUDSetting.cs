using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSetting : MonoBehaviour
{
    public TMP_Dropdown mDropdownLanguage;
    public TMP_Dropdown mDropdownWindow;
    public Toggle mToggleWindow;
    public Slider mMasterVolume;
    public Slider mBGMVolume;
    public Slider mSFXVolume;

    public TMP_Text mTextMasterVolume;
    public TMP_Text mTextBGMVolume;
    public TMP_Text mTextSFXVolume;

    public TMP_Text[] mText;

    public void UpdateText()
    {
        // 일괄적으로 텍스트 업데이트
        // for(int i=0; i<mText.Length; ++i)
        // {
        //     mText[i].text = GameManager.instance.mJsonTextData[(int)GameManager.instance.mSettingData.LanguageType].HUDSetting[i];
        // }
    }

    private FullScreenMode GetFullScreenMode(bool isFullscreen)
    {
        return isFullscreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
    }
    private float GetRefreshRateAsFloat(RefreshRate refreshRateRatio)
    {
        return (float)refreshRateRatio.numerator / refreshRateRatio.denominator;
    }
    private RefreshRate GetCurrentRefreshRateRatio()
    {
        int resolutionIndex = GameManager.instance.mSettingData.ResolutionNum;
        if (resolutionIndex >= 0 && resolutionIndex < Screen.resolutions.Length)
        {
            return Screen.resolutions[resolutionIndex].refreshRateRatio;
        }
        
        // 기본값 반환 (현재 화면의 refresh rate)
        return Screen.currentResolution.refreshRateRatio;
    }
    void InitLanguage()
    {
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = "English";
            mDropdownLanguage.options.Add(option);
        }
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = "Korean";
            mDropdownLanguage.options.Add(option);
        }
        mDropdownLanguage.RefreshShownValue();
        mDropdownLanguage.value = 2;
    }
    void InitWindow()
    {
        for (int i = 0; i < Screen.resolutions.Length; ++i)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            
            // ✅ 헬퍼 메서드 사용
            float refreshRate = GetRefreshRateAsFloat(Screen.resolutions[i].refreshRateRatio);
            option.text = $"{Screen.resolutions[i].width}x{Screen.resolutions[i].height} {refreshRate:F0}hz";
            
            mDropdownWindow.options.Add(option);
        }
        mDropdownWindow.RefreshShownValue();
        mDropdownWindow.value = GameManager.instance.mSettingData.ResolutionNum;
        mToggleWindow.isOn = GameManager.instance.mSettingData.Fullscreen;
    }
    void InitVolume()
    {
        mMasterVolume.value = GameManager.instance.mSettingData.MasterVolume;
        mBGMVolume.value = GameManager.instance.mSettingData.BGMVolume;
        mSFXVolume.value = GameManager.instance.mSettingData.SFXVolume;
    }

    public void Init()
    {
        InitLanguage();
        InitWindow();
        InitVolume();
        gameObject.SetActive(false);
    }

    public void ToTitle()
    {
        GameManager.instance.SaveSettingJson();
        GameManager.instance.ToTitle();
    }

    public void SetLanguage()
    {
        GameManager.instance.mSettingData.LanguageType = (Enum.Language)mDropdownLanguage.value;

        // GameManager.instance.UpdateText();
    }
    public void SetWindowScreen()
    {
        var selectedResolution = Screen.resolutions[mDropdownWindow.value];
        
        GameManager.instance.mSettingData.ResolutionNum = mDropdownWindow.value;
        GameManager.instance.mSettingData.Width = selectedResolution.width;
        GameManager.instance.mSettingData.Height = selectedResolution.height;
        GameManager.instance.mSettingData.RefreshRate = (int)GetRefreshRateAsFloat(selectedResolution.refreshRateRatio);

        Screen.SetResolution(
            selectedResolution.width,
            selectedResolution.height,
            GetFullScreenMode(GameManager.instance.mSettingData.Fullscreen),
            selectedResolution.refreshRateRatio
        );
    }
    public void SetFullscreen()
    {
        GameManager.instance.mSettingData.Fullscreen = mToggleWindow.isOn;

        var refreshRateRatio = Screen.resolutions[GameManager.instance.mSettingData.ResolutionNum].refreshRateRatio;

        Screen.SetResolution(
            GameManager.instance.mSettingData.Width,
            GameManager.instance.mSettingData.Height,
            GetFullScreenMode(GameManager.instance.mSettingData.Fullscreen),
            GetCurrentRefreshRateRatio()
        );
    }
    public void SetMasterVolume()
    {
        GameManager.instance.mSettingData.MasterVolume = mMasterVolume.value;
        mTextMasterVolume.text = ((int)(mMasterVolume.value * 100)).ToString();

        GameManager.instance.mBGMPlayer.volume = GameManager.instance.mSettingData.BGMVolume * GameManager.instance.mSettingData.MasterVolume;
        for (int i = 0; i < GameManager.instance.mSFXPlayer.Length; ++i)
        {
            GameManager.instance.mSFXPlayer[i].volume = GameManager.instance.mSettingData.SFXVolume * GameManager.instance.mSettingData.MasterVolume;
        }
    }
    public void SetBGMVolume()
    {
        GameManager.instance.mSettingData.BGMVolume = mBGMVolume.value;
        mTextBGMVolume.text = ((int)(mBGMVolume.value * 100)).ToString();

        GameManager.instance.mBGMPlayer.volume = GameManager.instance.mSettingData.BGMVolume * GameManager.instance.mSettingData.MasterVolume;
    }
    public void SetSFXVolume()
    {
        GameManager.instance.mSettingData.SFXVolume = mSFXVolume.value;
        mTextSFXVolume.text = ((int)(mSFXVolume.value * 100)).ToString();

        for (int i = 0; i < GameManager.instance.mSFXPlayer.Length; ++i)
        {
            GameManager.instance.mSFXPlayer[i].volume = GameManager.instance.mSettingData.SFXVolume * GameManager.instance.mSettingData.MasterVolume;
        }
    }

}
