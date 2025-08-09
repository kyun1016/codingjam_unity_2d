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

    [Header("# Game Object - HUD")]
    public GameObject _HUDGameStart;
    public HUDSkillTree _HUDSkillTree;
    public GameObject _HUDPause;
    public HUDDistance _HUDDistance;
    public Slider _HUDDistanceSlider;
    public Slider _HUDHPSlider;

    #region HUD
    void InitHUDGameStart()
    {
        if (_HUDGameStart != null)
        {
            _HUDGameStart.SetActive(true);
            Debug.Log("HUD initialized");
        }
        else
        {
            Debug.LogError("HUD GameObject is not assigned in GameManager!");
        }
    }
    void InitHUDPause()
    {
        if (_HUDPause != null)
        {
            _HUDPause.SetActive(false);
            Debug.Log("HUD Pause initialized");
        }
        else
        {
            Debug.LogError("HUD Pause GameObject is not assigned in GameManager!");
        }
    }
    void InitHUD()
    {
        InitHUDGameStart();
        InitHUDPause();
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

        InitHUD();
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
    void LateUpdate()
    {
        if (!_IsLive)
            return;

        _spd = _BackGroundTiles[0].GetComponent<BackGround>().GetCurrentSpeed();
        _distance += _spd * Time.deltaTime;
        _hp -= Time.deltaTime * _tickDamage; // Example damage over time, can be adjusted

        UpdateHUDSlider();

        if (_distance >= _maxDistance) GameOver();
        if (_hp <= 0f) GameOver();
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
    public void GameOver()
    {
        _IsLive = false;
        Time.timeScale = 0;
        Debug.Log("Game stopped");
        InitHUDGameStart();
        _HUDPause.SetActive(false);
    }

    public void GameStart()
    {
        _hp = _maxHp;
        _distance = 0f;
        _IsLive = true;
        _isJumping = false;
        _jumpHeight = 0.0f;
        _jumpSpeed = 0.0f;
        for (int i = 0; i < _BackGroundTiles.Length; i++)
        {
            _BackGroundTiles[i].GetComponent<BackGround>().Reset();
        }
        Time.timeScale = 1;
        Debug.Log("Game started");
        _HUDGameStart.SetActive(false);
        _HUDDistance.Show();
        _HUDPause.SetActive(true);
        _GameTime = 0f;
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
