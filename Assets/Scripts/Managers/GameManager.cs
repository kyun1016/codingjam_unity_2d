using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI; // For SpriteAtlas
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Control")]
    public bool _IsGamePaused = false;
    public bool _IsLive = false;
    public float _GameTime;
    [Header("# Settings")]
    public SettingData mSettingData;
    [Header("# BGM")]
    public AudioClip mBGMClip;
    public AudioSource mBGMPlayer;
    public AudioHighPassFilter mBGMFffect;
    [Header("# SFX")]
    public AudioClip[] mSFXClip;
    public int mMaxSFXChannel;
    public AudioSource[] mSFXPlayer;
    [Header("# Unity Data")]
    public SpriteAtlas _SpriteAtlas;
    public Sprite[] _HUDIcons;

    [Header("# JSON")]
    public JsonTextData[] _JsonTextData;
    [Header("# Map Size")]
    public float _TileSize = 80f; // Example tile size, can be adjusted
    [Header("# Input System")]
    public PlayerInput _input;
    [Header("# Game Object")]
    public GameObject _Player;
    public GameObject[] _BackGroundTiles;
    public GameObject[] _PoolPrefabs;
    [Header("# Parameters")]
    public float _hp = 100f;
    public float _maxHp = 100f;
    public float _tickDamage = 0.1f;
    public float _spd = 2.0f;
    public float _distance = 0f;
    public float _maxDistance = 7000.0f;
    public bool _isJumping = false;
    public float _jumpHeight = 0.0f;
    public float _jumpSpeed = 0.0f;
    public float _maxJumpSpeed = 30.0f;
    public float _gravity = 9.0f;
    public float _distanceScale = 1.0f; // Scale factor for distance calculation
    [Header("# Skill Parameters")]
    public List<int> _skillCount = new List<int>();
    public List<float> _skillCooldown = new List<float>();
    public List<bool> _skillEffect = new List<bool>();
    public List<ItemData> _skillData = new List<ItemData>();
    public List<int> _permanentSkillCount = new List<int>();
    public List<float> _permanentSkillCooldown = new List<float>();
    public List<bool> _permanentSkillEffect = new List<bool>();
    public List<ItemData> _permanentSkillData = new List<ItemData>();

    [Header("# Game Object - HUD")]
    public GameObject _HUDTitle;
    public HUDSkillTree _HUDSkillTree;
    public GameObject _HUDPause;
    public HUDDistance _HUDDistance;
    public Slider _HUDDistanceSlider;
    public Slider _HUDHPSlider;
    [Header("# Manager")]
    public InGame1Manager _InGame1Manager;
    public InGame2Manager _InGame2Manager;
    public HUDBuffer1Manager _HUDBuffer1Manager;
    public HUDBuffer2Manager _HUDBuffer2Manager;
    public HUDSetting _HUDSettingManager;

    private string SettingJsonPath => Path.Combine(Application.persistentDataPath, "SettingData.json");
    private string DefaultSettingJsonPath => Path.Combine(Application.streamingAssetsPath, "SettingData.json");

    void InitBGM()
    {
        mBGMPlayer = new GameObject("BGMPlayer").AddComponent<AudioSource>();
        mBGMPlayer.transform.parent = GameManager.instance.transform;
        mBGMPlayer.playOnAwake = true;
        mBGMPlayer.loop = true;
        mBGMPlayer.volume = mSettingData.BGMVolume * mSettingData.MasterVolume;
        mBGMPlayer.clip = mBGMClip;

        mBGMFffect = Camera.main.GetComponent<AudioHighPassFilter>();
    }
    void InitSFX()
    {
        GameObject sfxPlayer = new GameObject("SFXPlayer");
        sfxPlayer.transform.parent = GameManager.instance.transform;
        mSFXPlayer = new AudioSource[mMaxSFXChannel];
            
        for(int i=0; i<mSFXPlayer.Length; ++i)
        {
            mSFXPlayer[i] = sfxPlayer.AddComponent<AudioSource>();
            mSFXPlayer[i].playOnAwake = false;
            mSFXPlayer[i].loop = false;
            mSFXPlayer[i].bypassListenerEffects = true;
            mSFXPlayer[i].volume = mSettingData.SFXVolume * mSettingData.MasterVolume;
            // mSFXPlayer[i].clip = mSFXClip[i];
        }       
    }
    #region HUD
    public void InitializeTitle()
    {
        _HUDTitle.SetActive(true);
    }
    public void ResetTitle()
    {
        _HUDTitle.SetActive(false);
    }

    void UpdateHUDSlider()
    {
        _HUDDistanceSlider.value = _distance / _maxDistance;
        _HUDHPSlider.value = _hp / _maxHp;
    }
    #endregion

    private int GetCurrentResolutionIndex()
    {
        Resolution currentRes = Screen.currentResolution;
        Resolution[] resolutions = Screen.resolutions;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentRes.width && 
                resolutions[i].height == currentRes.height)
            {
                return i;
            }
        }
        
        return resolutions.Length - 1; // 기본값으로 최고 해상도 반환
    }

    private void CreateDefaultSettings()
    {
        mSettingData = new SettingData
        {
            // 기본값들 설정
            MasterVolume = 1.0f,
            BGMVolume = 0.8f,
            SFXVolume = 0.8f,
            Fullscreen = Screen.fullScreen,
            Width = Screen.currentResolution.width,
            Height = Screen.currentResolution.height,
            RefreshRate = ((float)Screen.currentResolution.refreshRateRatio.numerator / Screen.currentResolution.refreshRateRatio.denominator),
            ResolutionNum = GetCurrentResolutionIndex(),
            LanguageType = Enum.Language.English // 기본 언어
        };

        // 기본 설정을 파일로 저장
        SaveSettingJson();
        DevLog.Log("Default settings created and saved");
    }

    public void SaveSettingJson()
    {
        // try
        // {
        //     string json = JsonConvert.SerializeObject(mSettingData, Formatting.Indented);
            
        //     // ✅ persistentDataPath 사용 (빌드 후에도 쓰기 가능)
        //     string directoryPath = Path.GetDirectoryName(SettingJsonPath);
        //     if (!Directory.Exists(directoryPath))
        //     {
        //         Directory.CreateDirectory(directoryPath);
        //     }
            
        //     File.WriteAllText(SettingJsonPath, json);
        //     DevLog.Log($"Settings saved successfully to: {SettingJsonPath}");
        // }
        // catch (System.Exception e)
        // {
        //     DevLog.LogError($"Failed to save settings: {e.Message}");
        // }
    }
    public void LoadSettingJson()
    {
        // try
        // {
        //     string jsonPath = SettingJsonPath;
            
        //     // ✅ 파일이 존재하지 않으면 기본 설정으로 생성
        //     if (!File.Exists(jsonPath))
        //     {
        //         DevLog.Log("Settings file not found, creating default settings...");
        //         CreateDefaultSettings();
        //         return;
        //     }

        //     // ✅ 파일 읽기 및 역직렬화
        //     string json = File.ReadAllText(jsonPath);
            
        //     if (string.IsNullOrEmpty(json))
        //     {
        //         DevLog.Log("Settings file is empty, using default settings...");
        //         CreateDefaultSettings();
        //         return;
        //     }

        //     mSettingData = JsonConvert.DeserializeObject<SettingData>(json);
            
        //     try
        //     {
        //         mSettingData = JsonConvert.DeserializeObject<SettingData>(json);
        //         DevLog.Log($"Settings loaded successfully from: {jsonPath}");
        //     }
        //     catch (JsonException jsonEx)
        //     {
        //         DevLog.Log($"Failed to deserialize JSON: {jsonEx.Message}, using default settings...");
                CreateDefaultSettings();
        //         return;
        //     }
        // }
        // catch (System.Exception e)
        // {
        //     DevLog.LogError($"Failed to load settings: {e.Message}");
        //     CreateDefaultSettings();
        // }
    }

    #region CoreLogic
    public void Init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager instance destroyed");
        }

        InitBGM();
        InitSFX();
        _InGame1Manager.Reset();
        _InGame2Manager.Reset();
        _HUDBuffer1Manager.Reset();
        _HUDBuffer2Manager.Reset();
        LoadSettingJson();
        _HUDSettingManager.Init();
        InitializeTitle();
        // _HUDBuffer1Manager._gemini.SendChat();
    }
    void Awake()
    {
        Init();
        Debug.Log("GameManager Awake called");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!_IsLive)
            return;

        _GameTime += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!_IsLive || _IsGamePaused)
            return;

        if (_InGame2Manager._isActive)
        {
            if (_isJumping)
            {
                _jumpHeight += _jumpSpeed * Time.fixedDeltaTime;
                _jumpSpeed -= _gravity * Time.fixedDeltaTime;

                if (_jumpHeight <= 0.0f)
                {
                    _jumpHeight = 0.0f;
                    _isJumping = false;
                }
            }
        }
    }
    void LateUpdate()
    {
        if (!_IsLive)
            return;

        if(_InGame1Manager._isActive)
        {
            if(_InGame1Manager._HUDTimer._currentTime > _InGame1Manager._HUDTimer._maxTime)
            {
                GameOver1();
            }
        }
        if(_HUDBuffer1Manager._isActive)
        {
            if (_HUDBuffer1Manager._currentTime > _HUDBuffer1Manager._maxTime)
            {
                EndHUDBuffer1();
            }
        }
        if (_InGame2Manager._isActive)
        {
            _spd = _BackGroundTiles[0].GetComponent<BackGround_InGame2>().GetCurrentSpeed();
            _distance += _spd * Time.deltaTime * _distanceScale;
            _hp -= Time.deltaTime * _tickDamage; // Example damage over time, can be adjusted
            UpdateHUDSlider();
            if (_distance >= _maxDistance) GameOver2();
            if (_hp <= 0f) GameOver2();

            _InGame2Manager._HUDDistance.UpdateDistanceDisplay();
            _InGame2Manager._HUDDistance2.UpdateDistanceDisplay();
            _InGame2Manager._HUDDistance3.UpdateDistanceDisplay();
        }
        if (_HUDBuffer2Manager._isActive)
        {
            if (_HUDBuffer2Manager._currentTime > _HUDBuffer2Manager._maxTime)
            {
                EndHUDBuffer2();
            }
        }
    }
    #endregion

    #region Game_Control
    public void PauseGame()
    {
        _IsGamePaused = true;
        Time.timeScale = 0;
        Debug.Log("Game paused");
    }
    public void ResumeGame()
    {
        SaveSettingJson();
        _IsGamePaused = false;
        Time.timeScale = 1;
        Debug.Log("Game resumed");
    }
    public void ToggleGame()
    {
        if (_IsGamePaused)
            ResumeGame();
        else
            PauseGame();
    }
    
    public void SettingGame()
    {
        if(_IsGamePaused)
        {
            _IsGamePaused = false;
            Time.timeScale = 1;
            _HUDSettingManager.gameObject.SetActive(false);
        }
        else
        {
            _IsGamePaused = true;
            Time.timeScale = 0;
            _HUDSettingManager.gameObject.SetActive(true);
        }
    }

    public void GameStart1()
    {
        ResetTitle();
        _InGame1Manager.gameObject.SetActive(true);
        _InGame1Manager.Initialize();
        _IsLive = true;
        Time.timeScale = 1;
        _HUDTitle.SetActive(false);
        _HUDPause.SetActive(true);
//         _HUDBuffer1Manager._gemini.SendChat();
    }
    public void GameOver1()
    {
        Time.timeScale = 0;
        _IsLive = false;
        _InGame1Manager.Reset();
        StartHUDBuffer1();
    }
    public void StartHUDBuffer1()
    {
        _IsLive = true;
        Time.timeScale = 1;
        _HUDBuffer1Manager.Initialize();
    }
    public void EndHUDBuffer1()
    {
        _IsLive = false;
        Time.timeScale = 0;
        _HUDBuffer1Manager.Reset();
        GameStart2();
    }

    public void GameStart2()
    {
        _InGame2Manager.Initialize();
        _hp = _maxHp;
        _distance = 0f;
        _isJumping = false;
        _jumpHeight = 0.0f;
        _jumpSpeed = 0.0f;
        for (int i = 0; i < _BackGroundTiles.Length; i++)
        {
            _BackGroundTiles[i].GetComponent<BackGround_InGame2>().Reset();
        }
        _IsLive = true;
        Time.timeScale = 1;
        Debug.Log("Game started");
        _GameTime = 0f;
    }
    public void GameOver2()
    {
        _InGame2Manager.Reset();
        StartHUDBuffer2();
    }
    public void StartHUDBuffer2()
    {
        _IsLive = true;
        Time.timeScale = 1;
        _HUDBuffer2Manager.Initialize();
    }
    public void EndHUDBuffer2()
    {
        _IsLive = false;
        Time.timeScale = 0;
        _HUDBuffer2Manager.Reset();
        InitializeTitle();
    }

    public void ToTitle()
    {
        _IsLive = false;
        Time.timeScale = 0;
        if(_InGame1Manager._isActive) _InGame1Manager.Reset();
        if(_HUDBuffer1Manager._isActive) _HUDBuffer1Manager.Reset();
        if(_InGame2Manager._isActive) _InGame2Manager.Reset();
        if(_HUDBuffer2Manager._isActive) _HUDBuffer2Manager.Reset();
        _HUDSettingManager.gameObject.SetActive(false);
        // _HUDPause.SetActive(false);
        _HUDTitle.SetActive(true);
    }

    #endregion

    #region Input_System
    void OnJump(InputValue value)
    {
        if (!_IsLive || _IsGamePaused || _isJumping)
            return;

        _isJumping = true;
        _jumpHeight = 0.0f;
        _jumpSpeed = _maxJumpSpeed;

        DevLog.Log($"Jump input received {_jumpSpeed}");
    }
    #endregion
}
