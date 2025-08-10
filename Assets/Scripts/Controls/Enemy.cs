using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public EnemyData _enemyData;
    public float _speed = 2f;
    [Header("Jump Effect")]
    public Vector3 _originPosition;

    // 설정 된 거리가 되었을 때, 등장하는 시스템 구현
    private void Awake()
    {
        _originPosition = transform.position;
    }

    public void ResetSpeed()
    {
        _speed = GameManager.instance._InGame2Manager._BackGroundTiles[0]._speed;
    }
    public void ResetPosition()
    {
        transform.position = _originPosition;
    }
    public void Reset()
    {
        ResetSpeed();
        ResetPosition();
    }
    public void SetSpeed()
    {
        switch (_enemyData.movementType)
        {
            case Enum.MovementType.Close:
                _speed = GameManager.instance._InGame2Manager._BackGroundTiles[0]._speed;
                break;
            case Enum.MovementType.Mid:
                _speed = GameManager.instance._InGame2Manager._BackGroundTiles[2]._speed;
                break;
            case Enum.MovementType.Long:
                _speed = GameManager.instance._InGame2Manager._BackGroundTiles[4]._speed;
                break;
            default:
                Debug.LogWarning("Unknown movement type");
                break;
        }
        _speed = (_speed - _enemyData.diffSpeed) * _enemyData.scale; // Scale the speed based on enemy data
    }
    void FixedUpdate()
    {
        SetSpeed();
        // 이동
        transform.position = new Vector3(transform.position.x - _speed * Time.fixedDeltaTime, _originPosition.y - GameManager.instance._jumpHeight * _enemyData.jumpScale, _originPosition.z);

        if (transform.position.x < -GameManager.instance._TileSize)
        {
            ResetPosition();
            gameObject.SetActive(false);
        }
    }
}