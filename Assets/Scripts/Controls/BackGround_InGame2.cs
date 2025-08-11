using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackGround_InGame2 : MonoBehaviour
{
    [Header("Movement")]
    public MovementData _movementData;
    public float _speed = 2f;
    public float _jump = 0f;
    [Header("Jump Effect")]
    public Vector3 _originPosition;

    public void Initialization()
    {
        gameObject.SetActive(true);
        _speed = _movementData.baseSpeed;
        _originPosition = transform.position;
    }
    private void Awake()
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
        // 속도 증가
        _speed += _movementData.acceleration * Time.fixedDeltaTime;
        _speed = Mathf.Clamp(_speed, 0, _movementData.maxSpeed);
        // 이동
        transform.position = new Vector3(transform.position.x - _speed * Time.fixedDeltaTime * _movementData.dashEffect, _originPosition.y - GameManager.instance._jumpHeight * _movementData.jumpEffect, _originPosition.z);

        if (transform.position.x < -GameManager.instance._TileSize)
        {
            RepositionGroundTile();
        }
    }
    void LateUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
    }

    void RepositionGroundTile()
    {
        transform.Translate(Vector3.right * GameManager.instance._TileSize * 2); // 오른쪽 방향으로 꾸준히 옮기기만 해주면 된다.
    }
}