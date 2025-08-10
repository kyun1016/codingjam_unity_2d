using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI; // For SpriteAtlas
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Control")]
    public bool _IsGamePaused = false;
    public bool _IsLive = false;
    [Header("# Settings")]
    public SettingData _SettingData;
    public float _GameTime;
    [Header("# BGM")]
    public AudioClip _BGMClip;
    public AudioSource _BGMSource;
    [Header("# SFX")]
    public AudioSource _SFXSource;
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

    #region CoreLogic
    void Init()
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

        _InGame1Manager.Reset();
        _InGame2Manager.Reset();
        _HUDBuffer1Manager.Reset();
        _HUDBuffer2Manager.Reset();
        InitializeTitle();
        _HUDBuffer1Manager._gemini.SendChat();
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

    public void GameStart1()
    {
        ResetTitle();
        _InGame1Manager.gameObject.SetActive(true);
        _InGame1Manager.Initialize();
        _IsLive = true;
        Time.timeScale = 1;
        _HUDTitle.SetActive(false);
        _HUDPause.SetActive(true);
        _HUDBuffer1Manager._gemini.SendChat();
    }
    public void GameOver1()
    {
        _HUDBuffer1Manager._gemini.SendChat();
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
