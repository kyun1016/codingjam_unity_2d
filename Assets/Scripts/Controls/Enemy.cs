using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private int _positionIndex = 0;
    [Header("Movement")]
    [SerializeField] private MovementData _movementData;
    [SerializeField] private float _speed = 2f;
    [Header("Jump Effect")]
    [SerializeField] private Vector3 _originPosition;

    // 설정 된 거리가 되었을 때, 등장하는 시스템 구현

    private void Start()
    {
        _originPosition = transform.position;
    }

    public float GetCurrentSpeed()
    {
        return _speed;
    }
    public void ResetSpeed()
    {
        _speed = _movementData.baseSpeed;
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
    void FixedUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
        if(GameManager.instance._distance + GameManager.instance._TileSize > _enemyData.spawnPositions[_positionIndex])
        {
            gameObject.SetActive(false);
        }
        // 속도 증가
        _speed += _movementData.acceleration * Time.fixedDeltaTime;
        _speed = Mathf.Clamp(_speed, 0, _movementData.maxSpeed);
        // 이동
        transform.position = new Vector3(transform.position.x - _speed * Time.fixedDeltaTime * _movementData.dashEffect, _originPosition.y - GameManager.instance._jumpHeight * _movementData.jumpEffect, _originPosition.z);

        if (transform.position.x < -GameManager.instance._TileSize)
        {
            gameObject.SetActive(false);
        }
    }
    void LateUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
    }
}